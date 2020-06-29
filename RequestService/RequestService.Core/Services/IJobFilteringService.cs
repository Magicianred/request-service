using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface IJobFilteringService
    {
        Task<List<JobSummary>> FilterJobSummaries(
            List<JobSummary> jobs, 
            List<SupportActivities> supportActivities, 
            string postcode, 
            double? distanceInMiles, 
            Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles,
            int? referringGroupID,
            List<int> groups,
            List<JobStatuses> statuses,
            CancellationToken cancellationToken);
    }
}
