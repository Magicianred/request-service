using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    class JobFilteringServiceTests
    {
        private Mock<IJobService> _jobService;
        private List<JobHeader> _jobHeaders;
        private MockRepository _mockRepository;
        private JobFilteringService _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            SetUpJobService();

            _jobHeaders = new List<JobHeader>();
            _jobHeaders.Add(new JobHeader()
            {
                DistanceInMiles = 2d,
                SupportActivity = SupportActivities.CheckingIn,
                JobStatus = JobStatuses.Open,
                ReferringGroupID = 3
            });
            _jobHeaders.Add(new JobHeader()
            {
                DistanceInMiles = 4d,
                SupportActivity = SupportActivities.Shopping,
                JobStatus = JobStatuses.InProgress
            });
            _jobHeaders.Add(new JobHeader()
            {
                DistanceInMiles = 6d,
                SupportActivity = SupportActivities.HomeworkSupport,
            });
            _jobHeaders.Add(new JobHeader()
            {
                DistanceInMiles = 8d,
                SupportActivity = SupportActivities.MealPreparation,
            });
            _jobHeaders.Add(new JobHeader()
            {
                DistanceInMiles = 10d,
                SupportActivity = SupportActivities.FaceMask,
            });
            _jobHeaders.Add(new JobHeader()
            {
                DistanceInMiles = 100d,
                SupportActivity = SupportActivities.FaceMask,
            });
            _jobHeaders.Add(new JobHeader()
            {
                DistanceInMiles = 100d,
                SupportActivity = SupportActivities.MealPreparation,
            });

            _classUnderTest = new JobFilteringService(_jobService.Object);
        }

        private void SetUpJobService()
        {
            _jobService = _mockRepository.Create<IJobService>();
            _jobService.Setup(x => x.AttachedDistanceToJobSummaries(It.IsAny<string>(), It.IsAny<List<JobHeader>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string volunteerPostCode, List<JobHeader> jobSummaries, CancellationToken cancellationToken) => _jobHeaders);
        }

        [Test]
        public async Task WhenPassesInGoodRequest_ReturnsNoJobsDueToDistance()
        {
            string postcode = "POSTCODE";
            double? distanceInMiles = 0d;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobHeaders, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None); ;
            Assert.AreEqual(0, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequest_ReturnsJobs()
        {
            string postcode = "POSTCODE";
            double? distanceInMiles = 20d;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobHeaders, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobHeaders.Count(w => w.DistanceInMiles <= distanceInMiles), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithNullDistance_ReturnsJobs()
        {
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobHeaders, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobHeaders.Count(), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithActivitySpecificSupportDistance_ReturnsJobs()
        {
            string postcode = "POSTCODE";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = new Dictionary<SupportActivities, double?>() { { SupportActivities.Errands, 10d } };

            _jobHeaders.Add(new JobHeader() { DistanceInMiles = 8d, SupportActivity = SupportActivities.Errands });
            _jobHeaders.Add(new JobHeader() { DistanceInMiles = 12d, SupportActivity = SupportActivities.Errands });

            var response = await _classUnderTest.FilterJobSummaries(_jobHeaders, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobHeaders.Where(w => w.SupportActivity != SupportActivities.Errands || w.DistanceInMiles < 10d).Count(), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithNullActivitySpecificSupportDistance_ReturnsJobs()
        {
            string postcode = "POSTCODE";
            double? distanceInMiles = 0d;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = new Dictionary<SupportActivities, double?>() { { SupportActivities.Errands, 100d } };

            _jobHeaders.Add(new JobHeader() { DistanceInMiles = 8d, SupportActivity = SupportActivities.Errands });
            _jobHeaders.Add(new JobHeader() { DistanceInMiles = 12d, SupportActivity = SupportActivities.Errands });

            var response = await _classUnderTest.FilterJobSummaries(_jobHeaders, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobHeaders.Where(w => w.SupportActivity == SupportActivities.Errands || w.DistanceInMiles == 0d).Count(), response.Count);
        }
    }
}
