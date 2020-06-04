using HelpMyStreet.Contracts.AddressService.Response;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Utils;

namespace RequestService.UnitTests
{
    public class JobServiceTests
    {
        private MockRepository _mockRepository;                
        Mock<IDistanceCalculator> _mockDistanceCalculator;
        Mock<IRepository> _repository;
        Mock<ICommunicationService> _communicationService;
        private JobService _classUnderTest;
        private double _distance;
        private bool _emailSent;
        private List<LatitudeAndLongitudeDTO> _getPostcodeCoordinatesResponse;
        private GetJobDetailsResponse _jobDetails;

        [SetUp]
        public void Setup()
        {            
            _mockRepository = new MockRepository(MockBehavior.Loose);
            SetupRepository();
            SetupDistanceCalculator();
            SetupCommunicationService();
            _classUnderTest = new JobService(_mockDistanceCalculator.Object, _communicationService.Object, _repository.Object );
        }

        private void SetupCommunicationService()
        {
            _communicationService = _mockRepository.Create<ICommunicationService>();
            _communicationService.Setup(x => x.SendEmail(It.IsAny<SendEmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _emailSent);
        }

        private void SetupRepository()
        {
            _repository = _mockRepository.Create<IRepository>();
            _repository.Setup(x => x.GetLatitudeAndLongitudes(It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(()=> _getPostcodeCoordinatesResponse);
            _repository.Setup(x => x.GetJobDetails(It.IsAny<int>())).Returns(() => _jobDetails);
        }

        private void SetupDistanceCalculator()
        {
            _mockDistanceCalculator = _mockRepository.Create<IDistanceCalculator>();
            _mockDistanceCalculator.Setup(x => x.GetDistanceInMiles(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .Returns(() => _distance);
        }


        [TearDown]
        public void VerifyAndTearDown()
        {
            //_mockRepository.VerifyAll();
        }

        [Test]
        public async Task WhenPasscodesAreKnownToAddressService_ReturnJobSummaryWithDistance()
        {
            _distance = 50d;
            List<LatitudeAndLongitudeDTO> postcodeCoordinates = new List<LatitudeAndLongitudeDTO>();
            postcodeCoordinates.Add(new LatitudeAndLongitudeDTO
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode"
            });
            postcodeCoordinates.Add(new LatitudeAndLongitudeDTO
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode2"
            });
            _getPostcodeCoordinatesResponse = postcodeCoordinates;


            string postCode = "PostCode2";
            List<JobSummary> jobSummaries = new List<JobSummary>();
            jobSummaries.Add(new JobSummary()
            {
                JobID = 1,
                PostCode = "PostCode"
            });
            var response = await _classUnderTest.AttachedDistanceToJobSummaries(postCode,jobSummaries, CancellationToken.None);
            
        }

        [Test]
        public async Task WhenVolunteerPasscodesIsNotKnownToAddressService_ReturnNull()
        {
            _distance = 50d;
            List<LatitudeAndLongitudeDTO> postcodeCoordinates = new List<LatitudeAndLongitudeDTO>();
            postcodeCoordinates.Add(new LatitudeAndLongitudeDTO()
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode"
            });
            _getPostcodeCoordinatesResponse = postcodeCoordinates;
       
            string postCode = "PostCode2";
            List<JobSummary> jobSummaries = new List<JobSummary>();
            jobSummaries.Add(new JobSummary()
            {
                JobID = 1,
                PostCode = "PostCode"
            });
            var response = await _classUnderTest.AttachedDistanceToJobSummaries(postCode, jobSummaries, CancellationToken.None);
            _mockDistanceCalculator.Verify(v => v.GetDistanceInMiles(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Never);
        }


        [Test]
        public async Task WhenISendUpdateEmail_ForRequestor_IsendEmailToRequestor()
        {
            _jobDetails = new GetJobDetailsResponse
            {
                ForRequestor = true,
                Requestor = new RequestPersonalDetails
                {
                    EmailAddress = "requestor@email.com",
                    FirstName = "Requestor",
                    LastName = "Req",
                },
                DueDate = DateTime.Now,
                SupportActivity = HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn,
                

                
            };
            await _classUnderTest.SendUpdateStatusEmail(1, HelpMyStreet.Utils.Enums.JobStatuses.Done, new CancellationToken());

            _communicationService.Verify(x => x.SendEmail(It.Is<SendEmailRequest>(e => e.ToAddress == "requestor@email.com"), It.IsAny<CancellationToken>()));
        }


        [Test]
        public async Task WhenISendUpdateEmail_OnBehalf_WithRequestorEmailAddress_IsendEmailToRequestor()
        {
            _jobDetails = new GetJobDetailsResponse
            {
                ForRequestor = false,
                Requestor = new RequestPersonalDetails
                {
                    EmailAddress = "requestor@email.com",
                    FirstName = "Requestor",
                    LastName = "Req",
                },
                Recipient = new RequestPersonalDetails
                {
                    EmailAddress = "recipient@email.com",
                    FirstName = "Requestor",
                    LastName = "Req",
                },
                DueDate = DateTime.Now,
                SupportActivity = HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn,
            };
                await _classUnderTest.SendUpdateStatusEmail(1, HelpMyStreet.Utils.Enums.JobStatuses.Done, new CancellationToken());
               _communicationService.Verify(x => x.SendEmail(It.Is<SendEmailRequest>(e => e.ToAddress == "requestor@email.com"), It.IsAny<CancellationToken>()));

                   
        }

        [Test]
        public async Task WhenISendUpdateEmail_OnBehalf_WithNoRequestorEmailAddress_IsendEmailToRecipient()
        {
            _jobDetails = new GetJobDetailsResponse
            {
                ForRequestor = false,
                Requestor = new RequestPersonalDetails
                {
                    EmailAddress = "",
                    FirstName = "Requestor",
                    LastName = "Req",
                },
                Recipient = new RequestPersonalDetails
                {
                    EmailAddress = "recipient@email.com",
                    FirstName = "Requestor",
                    LastName = "Req",
                },
                DueDate = DateTime.Now,
                SupportActivity = HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn,
            };
            await _classUnderTest.SendUpdateStatusEmail(1, HelpMyStreet.Utils.Enums.JobStatuses.Done, new CancellationToken());
            _communicationService.Verify(x => x.SendEmail(It.Is<SendEmailRequest>(e => e.ToAddress == "recipient@email.com"), It.IsAny<CancellationToken>()));

        }
    }
}