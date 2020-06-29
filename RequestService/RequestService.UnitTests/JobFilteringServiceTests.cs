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
                SupportActivity = SupportActivities.CheckingIn,
                Groups = new List<int>()
                { 1,2},
                JobStatus = JobStatuses.Open,
                ReferringGroupID = 3
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 4d,
                SupportActivity = SupportActivities.Shopping,
                Groups = new List<int>()
                { 2,3},
                JobStatus = JobStatuses.InProgress
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 6d,
                SupportActivity = SupportActivities.HomeworkSupport,
                Groups = new List<int>()
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 8d,
                SupportActivity = SupportActivities.MealPreparation,
                Groups = new List<int>()
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 10d,
                SupportActivity = SupportActivities.FaceMask,
                Groups = new List<int>()
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 100d,
                SupportActivity = SupportActivities.FaceMask,
                Groups = new List<int>()
            });
            _jobSummaries.Add(new JobSummary()
            {
                DistanceInMiles = 100d,
                SupportActivity = SupportActivities.MealPreparation,
                Groups = new List<int>()
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

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles,null,null,null, CancellationToken.None);;
            Assert.AreEqual(0, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequest_ReturnsJobs()
        {
            List<SupportActivities> supportActivities = null;
            string postcode = "";
            double? distanceInMiles = 20d;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles,null,null,null, CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(w => w.DistanceInMiles <= distanceInMiles), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithNullDistance_ReturnsJobs()
        {
            List<SupportActivities> supportActivities = null;
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles,null,null,null, CancellationToken.None);
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

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles,null,null,null, CancellationToken.None);
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

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, null,null,null, CancellationToken.None);
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

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles,null,null,null,CancellationToken.None);
            Assert.AreEqual(_jobSummaries.Count(w => w.SupportActivity == SupportActivities.Errands || w.SupportActivity == SupportActivities.DogWalking), response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithEmptySupportActivityFilter_ReturnsNoJobs()
        {
            List<SupportActivities> supportActivities = new List<SupportActivities>();
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, supportActivities, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, null,null, null, CancellationToken.None);
            Assert.AreEqual(0, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithEmptyJobStatusFilter_ReturnsNoJobs()
        {
            List<JobStatuses> statuses = new List<JobStatuses>();
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, null, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, null, null, statuses, CancellationToken.None);
            Assert.AreEqual(0, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithEmptyGroupFilter_ReturnsNoJobs()
        {
            List<int> groups = new List<int>();
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, null, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, null, groups, null, CancellationToken.None);
            Assert.AreEqual(0, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithGroupFilter_ReturnsJobs()
        {
            List<int> groups = new List<int>() { 1 };
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            int count = _jobSummaries.Count(t2 => groups.Any(t1 => t2.Groups.Contains(t1)));

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, null, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, null, groups, null, CancellationToken.None);

            

            Assert.AreEqual(count, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithJobStatusFilter_ReturnsJobs()
        {
            List<JobStatuses> statuses = new List<JobStatuses>()
            { JobStatuses.InProgress,JobStatuses.Open};
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            int count = _jobSummaries.Count(t2 => statuses.Contains(t2.JobStatus));

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, null, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, null,null,statuses, CancellationToken.None);

            Assert.AreEqual(count, response.Count);
        }

        [Test]
        public async Task WhenPassesInGoodRequestWithReferringGroup_ReturnsJobs()
        {
            int referringGroupId = 3;
            string postcode = "";
            double? distanceInMiles = null;
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles = null;

            int count = _jobSummaries.Count(t2 => t2.ReferringGroupID == referringGroupId);

            var response = await _classUnderTest.FilterJobSummaries(_jobSummaries, null, postcode, distanceInMiles, activitySpecificSupportDistancesInMiles, referringGroupId, null, null, CancellationToken.None);

            Assert.AreEqual(count, response.Count);
        }
    }
}
