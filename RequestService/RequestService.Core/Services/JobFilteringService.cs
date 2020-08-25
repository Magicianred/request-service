using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using RequestService.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<List<JobSummary>> FilterJobSummaries(
            List<JobSummary> jobs, 
            int? UserID,
            List<SupportActivities> supportActivities, 
            string postcode, 
            double? distanceInMiles, 
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles,
            int? referringGroupID,
            List<int> groups,
            List<JobStatuses> statuses,
            CancellationToken cancellationToken)
        {
            bool applyDistanceFilter = false;
            //if postcode has been pased calculate distance between volunteer postcode and jobs
            if (!string.IsNullOrEmpty(postcode))
            {
                jobs = await _jobService.AttachedDistanceToJobSummaries(postcode, jobs, cancellationToken);
                applyDistanceFilter = true;
            }

            if (jobs == null)
            {
                // For now, return no jobs to avoid breaking things downstream
                return new List<JobSummary>();
            }

            if(UserID.HasValue)
            {
                jobs = jobs.Where(w => w.VolunteerUserID == UserID.Value)
                    .ToList();
            }

            jobs = jobs.Where(w => supportActivities == null || supportActivities.Contains(w.SupportActivity))
                       .ToList();

            if(applyDistanceFilter)
            {
                jobs = jobs.Where(w => w.DistanceInMiles <= GetSupportDistanceForActivity(w.SupportActivity, distanceInMiles, activitySpecificSupportDistancesInMiles))
                        .ToList();
            }

            if(referringGroupID.HasValue)
            {
                jobs = jobs.Where(w => w.ReferringGroupID == referringGroupID.Value).ToList();
            }

            if (groups!=null)
            {
                jobs = jobs.Where(t2 => groups.Any(t1 => t2.Groups.Contains(t1))).ToList();
            }

            if (statuses != null)
            {
                jobs = jobs.Where(t2 => statuses.Contains(t2.JobStatus)).ToList();
            }

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
