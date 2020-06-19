using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    class JobFilteringServiceTests
    {
        private Mock<IJobService> _jobService;
        private List<JobSummary> _jobSummaries;
        private MockRepository _mockRepository;
        private JobFilteringService _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            SetUpJobService();

            _jobSummaries = new List<JobSummary>();
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 2d,
                SupportActivity = SupportActivities.CheckingIn
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 4d,
                SupportActivity = SupportActivities.Shopping
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 6d,
                SupportActivity = SupportActivities.HomeworkSupport
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 8d,
                SupportActivity = SupportActivities.MealPreparation
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 10d,
                SupportActivity = SupportActivities.FaceMask
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 100d,
                SupportActivity = SupportActivities.FaceMask
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 100d,
                SupportActivity = SupportActivities.MealPreparation
            });

            _classUnderTest = new JobFilteringService(_jobService.Object);
        }

        private void SetUpJobService()
        {
            _jobService = _mockRepository.Create<IJobService>();
            _jobService.Setup(x => x.AttachedDistanceToJobSummaries(It.IsAny<string>(), It.IsAny<List<JobSummary>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string volunteerPostCode, List<JobSummary> jobSummaries, CancellationToken cancellationToken) => jobSummaries);
        }

        [Test]
        public async Task WhenPassesInGoodRequest_ReturnsNoJobsDueToDistance()
        {
            List<SupportActivities> supportActivities = null;
            string postcode = "";
            double? distanceInMiles = 0d;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(0, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequest_ReturnsJobs()
        {
            List<SupportActivities> supportActivities = null;
            string postcode = "";
            double? distanceInMiles = 20d;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(w => w.DistanceInMiles <= distanceInMiles), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithNullDistance_ReturnsJobs()
        {
            List<SupportActivities> supportActivities = null;
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithActivitySpecificSupportDistance_ReturnsJobs()
        {
            List<SupportActivities> supportActivities = null;
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = new Dictionary<SupportActivities, double?>() { { SupportActivities.Errands, 10d } };

            _jobSummaries.Add(new JobSummary() { DistanceInMiles = 8d, SupportActivity = SupportActivities.Errands });
            _jobSummaries.Add(new JobSummary() { DistanceInMiles = 12d, SupportActivity = SupportActivities.Errands });

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Where(w => w.SupportActivity != SupportActivities.Errands || w.DistanceInMiles < 10d).Count(), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithNullActivitySpecificSupportDistance_ReturnsJobs()
        {
            List<SupportActivities> supportActivities = null;
            string postcode = "";
            double? distanceInMiles = 0d;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = new Dictionary<SupportActivities, double?>() { { SupportActivities.Errands, 100d } };

            _jobSummaries.Add(new JobSummary() { DistanceInMiles = 8d, SupportActivity = SupportActivities.Errands });
            _jobSummaries.Add(new JobSummary() { DistanceInMiles = 12d, SupportActivity = SupportActivities.Errands });

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Where(w => w.SupportActivity == SupportActivities.Errands || w.DistanceInMiles == 0d).Count(), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithSupportActivityFilter_ReturnsJobs()
        {
            List<SupportActivities> supportActivities = new List<SupportActivities>() { SupportActivities.Errands, SupportActivities.DogWalking };
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            _jobSummaries.Add(new JobSummary() { DistanceInMiles = 8d, SupportActivity = SupportActivities.Errands });
            _jobSummaries.Add(new JobSummary() { DistanceInMiles = 12d, SupportActivity = SupportActivities.Errands });
            _jobSummaries.Add(new JobSummary() { DistanceInMiles = 12d, SupportActivity = SupportActivities.DogWalking });

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(w => w.SupportActivity == SupportActivities.Errands || w.SupportActivity == SupportActivities.DogWalking), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithEmptySupportActivityFilter_ReturnsNoJobs()
        {
            List<SupportActivities> supportActivities = new List<SupportActivities>();
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, CancellationToken.None);
            Assert.AreEqual(0, response.Count);
        }
    }
}
