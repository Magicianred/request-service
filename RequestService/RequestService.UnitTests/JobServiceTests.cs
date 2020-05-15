using HelpMyStreet.Contracts.AddressService.Response;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Utils;

namespace RequestService.UnitTests
{
    public class JobServiceTests
    {
        private MockRepository _mockRepository;
        Mock<IAddressService> _mockAddressService;
        Mock<IDistanceCalculator> _mockDistanceCalculator;
        private JobService _classUnderTest;
        private double _distance;
        private GetPostcodeCoordinatesResponse _getPostcodeCoordinatesResponse;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            SetupAddressService();
            SetupDistanceCalculator();
            _classUnderTest = new JobService(_mockAddressService.Object,_mockDistanceCalculator.Object);
        }

        private void SetupAddressService()
        {
            _mockAddressService = _mockRepository.Create<IAddressService>();
            _mockAddressService.Setup(x => x.GetPostcodeCoordinatesAsync(It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(()=> _getPostcodeCoordinatesResponse);
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
            List<PostcodeCoordinate> postcodeCoordinates = new List<PostcodeCoordinate>();
            postcodeCoordinates.Add(new PostcodeCoordinate()
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode"
            });
            postcodeCoordinates.Add(new PostcodeCoordinate()
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode2"
            });
            _getPostcodeCoordinatesResponse = new GetPostcodeCoordinatesResponse()
            {
                PostcodeCoordinates = postcodeCoordinates
            };
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
            List<PostcodeCoordinate> postcodeCoordinates = new List<PostcodeCoordinate>();
            postcodeCoordinates.Add(new PostcodeCoordinate()
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode"
            });
            _getPostcodeCoordinatesResponse = new GetPostcodeCoordinatesResponse()
            {
                PostcodeCoordinates = postcodeCoordinates
            };
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

    }
}