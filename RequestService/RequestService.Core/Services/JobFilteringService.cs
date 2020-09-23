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

        public async Task<List<JobHeader>> FilterJobHeaders(
            List<JobHeader> jobs,            
            string postcode,
            double? distanceInMiles, 
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles,            
            CancellationToken cancellationToken)
        {
            bool applyDistanceFilter = false;
            //if postcode has been pased calculate distance between volunteer postcode and jobs
            if (!string.IsNullOrEmpty(postcode))
            {
                jobs = await _jobService.AttachedDistanceToJobHeaders(postcode, jobs, cancellationToken);
                applyDistanceFilter = true;
            }

            if (jobs == null)
            {
                // For now, return no jobs to avoid breaking things downstream
                return new List<JobHeader>();
            }

            if (applyDistanceFilter)
            {
                jobs = jobs.Where(w => w.DistanceInMiles <= GetSupportDistanceForActivity(w.SupportActivity, distanceInMiles, activitySpecificSupportDistancesInMiles))
                        .ToList();
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
