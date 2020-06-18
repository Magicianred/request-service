using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public class JobFilteringService : IJobFilteringService
    {
        private readonly IJobService _jobService;

        public JobFilteringService(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<List<JobSummary>> FilterJobSummaries(List<JobSummary> jobs, List<SupportActivities> supportActivities, string postcode, double? distanceInMiles, Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles, CancellationToken cancellationToken)
        {
            jobs = await _jobService.AttachedDistanceToJobSummaries(postcode, jobs, cancellationToken);

            jobs = jobs.Where(w => supportActivities == null || supportActivities.Contains(w.SupportActivity))
                       .Where(w => w.DistanceInMiles <= GetSupportDistanceForActivity(w.SupportActivity, distanceInMiles, activitySpecificSupportDistancesInMiles))
                       .ToList();

            return jobs;
        }

        private double GetSupportDistanceForActivity(SupportActivities supportActivity, double? distanceInMiles, Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles)
        {
            if (activitySpecificSupportDistancesInMiles != null && activitySpecificSupportDistancesInMiles.ContainsKey(supportActivity))
            {
                return activitySpecificSupportDistancesInMiles[supportActivity] ?? int.MaxValue;
            }
            else
            {
                return distanceInMiles ?? int.MaxValue;
            }
        }
    }
}
