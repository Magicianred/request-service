using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Enums;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class PostNewRequestForHelpHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<IUserService> _userService;
        private Mock<ICommunicationService> _communicationService;
        private Mock<IAddressService> _adddressService;
        private PostNewRequestForHelpHandler _classUnderTest;
        private PostNewRequestForHelpRequest _request;
        private Mock<IOptionsSnapshot<ApplicationConfig>> _applicationConfig;
        private int requestId;
        private bool _validPostcode;
        private int _championCount;
        private bool _emailSent;
        private GetVolunteersByPostcodeAndActivityResponse _getVolunteersByPostcodeAndActivityResponse;
        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupAddressService();
            SetupCommunicationService();
            SetupApplicationConfig();
            SetupUserService();
            _classUnderTest = new PostNewRequestForHelpHandler(_repository.Object, _userService.Object, _adddressService.Object, _communicationService.Object, _applicationConfig.Object);
        }
        private void SetupCommunicationService()
        {
            _communicationService = new Mock<ICommunicationService>();
            _communicationService.Setup(x => x.SendEmail(It.IsAny<SendEmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _emailSent);

        }
        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.NewHelpRequestAsync(
                It.IsAny<PostNewRequestForHelpRequest>(),
                It.IsAny<Fulfillable>()))
                .ReturnsAsync(() => requestId);
            _repository.Setup(x => x.UpdateCommunicationSentAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()));

        }
        private void SetupApplicationConfig()
        {
            _applicationConfig = new Mock<IOptionsSnapshot<ApplicationConfig>>();
            _applicationConfig.SetupGet(x => x.Value).Returns(new ApplicationConfig
            {
                ManualReferName = "test",
                ManualReferEmail = "manual@test.com",
                EmailBaseUrl = "helpmystreettest"
            });
        }

        private void SetupUserService()
        {
            _userService = new Mock<IUserService>();
            _userService.Setup(x => x.GetChampionCountByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _championCount);
            _userService.Setup(x => x.GetHelpersByPostcodeAndTaskType(It.IsAny<string>(), It.IsAny<List<SupportActivities>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _getVolunteersByPostcodeAndActivityResponse);
        }

        private void SetupAddressService()
        {
            _adddressService = new Mock<IAddressService>();
            _adddressService.Setup(x => x.IsValidPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _validPostcode);
        }

        [Test]
        public async Task WhenIPostValidNewRequest_AllFunctionGetCalledCorrectly()
        {
           requestId = 1;
          _validPostcode = true;
          _championCount = 1;
          _emailSent = true;
           _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
           {
                Volunteers = new List<VolunteerSummary>
                {
                    new VolunteerSummary
                    {
                        UserID = 1,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    }
                }
            };
           var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };
           await _classUnderTest.Handle(request, new CancellationToken());

            _adddressService.Verify(x => x.IsValidPostcode("TEST", It.IsAny<CancellationToken>()), Times.Once);
            _userService.Verify(x => x.GetChampionCountByPostcode("TEST", It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.NewHelpRequestAsync(request, Fulfillable.Accepted_PassToStreetChampion), Times.Once);
            _userService.Verify(x => x.GetHelpersByPostcodeAndTaskType("TEST", new List<SupportActivities> { SupportActivities.Shopping }, It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.IsAny<SendEmailToUsersRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateCommunicationSentAsync(1, true, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenIPostRequest_WithInvalidPostcode_IGetRejected()
        {            
            _validPostcode = false;

            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());
            Assert.AreEqual(Fulfillable.Rejected_InvalidPostcode, response.Fulfillable);     
        }

        [Test]
        public async Task WhenIPostRequest_WithNoChampions_IGetManualReer()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 0;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>
                {
                    new VolunteerSummary
                    {
                        UserID = 1,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    }
                }
            };
            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());
            Assert.AreEqual(Fulfillable.Accepted_ManualReferral, response.Fulfillable);
        }

        [Test]
        public async Task WhenIPostRequest_WithChampions_IGetAccepted()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 1;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>
                {
                    new VolunteerSummary
                    {
                        UserID = 99,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    }
                }
            };

            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());
            Assert.AreEqual(Fulfillable.Accepted_PassToStreetChampion, response.Fulfillable);
        }


        [Test]
        public async Task WhenISendEmail_WithNoHelperFound_ISendManualRefer()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 1;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>()                
            };

            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());

            _communicationService.Verify(x => x.SendEmail(It.Is<SendEmailRequest>(ser => ser.ToAddress == "manual@test.com"), It.IsAny<CancellationToken>()), Times.Once);
        }



        [Test]
        public async Task WhenISendEmail_WithHelperFound_ISendToUser()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 1;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>
                {
                    new VolunteerSummary
                    {
                        UserID = 99,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    }
                }
            };


            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());

            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(ser => ser.Recipients.ToUserIDs.Contains(99)), It.IsAny<CancellationToken>()), Times.Once);
        }



        [Test]
        public async Task WhenISendEmail_WithHelperFound_ISendEmailConfirmation()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 1;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>
                {
                    new VolunteerSummary
                    {
                        UserID = 99,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    }
                }
            };


            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        EmailAddress = "requestorEmailAdddress",
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                            
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());

            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(ser => ser.Recipients.ToUserIDs.Contains(99)), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        public async Task WhenISendEmail_WithNoHelperFound_ISendEmailConfirmation()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 1;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>()
            };

            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        EmailAddress = "requestorEmailAdddress",
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",

                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());

            _communicationService.Verify(x => x.SendEmail(It.Is<SendEmailRequest>(ser => ser.ToAddress == "requestorEmailAdddress"), It.IsAny<CancellationToken>()), Times.Once);
        }


        public async Task WhenISendEmail_WithHelperFound_ISendEmailToEachHelper()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 1;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>
                {
                    new VolunteerSummary
                    {
                        UserID = 99,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    },
                    new VolunteerSummary
                    {
                        UserID = 98,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    }
                }
            };

            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        EmailAddress = "requestorEmailAdddress",
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",

                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());

            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(ser => ser.Recipients.ToUserIDs.Contains(99)), It.IsAny<CancellationToken>()), Times.Once);
             _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(ser => ser.Recipients.ToUserIDs.Contains(98)), It.IsAny<CancellationToken>()), Times.Once);
            
        }
    }
}