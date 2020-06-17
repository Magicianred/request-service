using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HelpMyStreet.Utils.Enums;

namespace RequestService.UnitTests
{
    public class GetJobsByFilterHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<IJobService> _jobService;
        private Mock<IAddressService> _addressService;
        private GetJobsByFilterHandler _classUnderTest;
        private GetJobsByFilterRequest _request;
        private GetJobsByFilterResponse _response;
        private List<JobSummary> _jobSummaries;
        private MockRepository _mockRepository;

       [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            SetupRepository();
            SetUpJobService();
            SetupAddressService();

            _jobSummaries = new List<JobSummary>();
            _jobSummaries.Add(new JobSummary
            {
                JobID = 1,
                DistanceInMiles = 25d,
                SupportActivity = SupportActivities.CheckingIn
            });
            _jobSummaries.Add(new JobSummary
            {
                JobID = 1,
                DistanceInMiles = 15d,
                SupportActivity = SupportActivities.CollectingPrescriptions
            });
            _jobSummaries.Add(new JobSummary
            {
                JobID = 1,
                DistanceInMiles = 20d,
                SupportActivity = SupportActivities.FaceMask
            });
            _jobSummaries.Add(new JobSummary
            {
                JobID = 1,
                DistanceInMiles = 0d,
                SupportActivity = SupportActivities.Errands
            });
            _jobSummaries.Add(new JobSummary
            {
                JobID = 1,
                DistanceInMiles = 30d,
                SupportActivity = SupportActivities.Errands
            });

            _response = new GetJobsByFilterResponse()
            {
                JobSummaries = _jobSummaries
            };

            _classUnderTest = new GetJobsByFilterHandler(_repository.Object, _jobService.Object,_addressService.Object);
        }

        private void SetUpJobService()
        {
            _jobService = _mockRepository.Create<IJobService>();
            _jobService.Setup(x => x.AttachedDistanceToJobSummaries(It.IsAny<string>(), It.IsAny<List<JobSummary>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=>_jobSummaries);
        }

        private void SetupRepository()
        {
            _repository = _mockRepository.Create<IRepository>();
            _repository.Setup(x => x.GetOpenJobsSummaries())
                .Returns(()=>_jobSummaries);
        }

        private void SetupAddressService()
        {
            _addressService = _mockRepository.Create<IAddressService>();
            _addressService.Setup(x => x.IsValidPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }

        [TearDown]
        public void VerifyAndTearDown()
        {
            _mockRepository.VerifyAll();
        }

        [Test]
        public async Task WhenPassesInGoodRequest_ReturnsNoJobsDueToDistance()
        {
            _request = new GetJobsByFilterRequest
            {
                Postcode = "NG1 6DQ",
                DistanceInMiles = 20d
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(w => w.DistanceInMiles <= _request.DistanceInMiles), response.JobSummaries.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequest_ReturnsJobs()
        {
            _request = new GetJobsByFilterRequest
            {
                Postcode = "NG1 6DQ",
                DistanceInMiles = 50d
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(w => w.DistanceInMiles <= _request.DistanceInMiles), response.JobSummaries.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithNullDistance_ReturnsJobs()
        {
            _request = new GetJobsByFilterRequest
            {
                Postcode = "NG1 6DQ",
                DistanceInMiles = null
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(), response.JobSummaries.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithActivitySpecificSupportDistance_ReturnsJobs()
        {
            _request = new GetJobsByFilterRequest
            {
                Postcode = "NG1 6DQ",
                DistanceInMiles = null,
                ActivitySpecificSupportDistancesInMiles = new Dictionary<SupportActivities, double?>() { { SupportActivities.Errands, 10d } }
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Where(w => w.SupportActivity != SupportActivities.Errands || w.DistanceInMiles < 10d).Count(), response.JobSummaries.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithNullActivitySpecificSupportDistance_ReturnsJobs()
        {
            _request = new GetJobsByFilterRequest
            {
                Postcode = "NG1 6DQ",
                DistanceInMiles = 0d,
                ActivitySpecificSupportDistancesInMiles = new Dictionary<SupportActivities, double?>() { { SupportActivities.Errands, null } }
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Where(w => w.SupportActivity == SupportActivities.Errands || w.DistanceInMiles == 0d).Count(), response.JobSummaries.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithSupportActivityFilter_ReturnsJobs()
        {
            _request = new GetJobsByFilterRequest
            {
                Postcode = "NG1 6DQ",
                DistanceInMiles = null,
                SupportActivities = new List<SupportActivities>() { SupportActivities.Errands, SupportActivities.DogWalking }
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(w => w.SupportActivity == SupportActivities.Errands || w.SupportActivity == SupportActivities.Errands), response.JobSummaries.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithEmptySupportActivityFilter_ReturnsJobs()
        {
            _request = new GetJobsByFilterRequest
            {
                Postcode = "NG1 6DQ",
                DistanceInMiles = null,
                SupportActivities = new List<SupportActivities>()
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(0, response.JobSummaries.Count);
        }
    }
}