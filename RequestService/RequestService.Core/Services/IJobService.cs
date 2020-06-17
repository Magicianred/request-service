using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface IJobService
    {
        Task<List<JobSummary>> AttachedDistanceToJobSummaries(string volunteerPostCode, List<JobSummary> jobSummaries, CancellationToken cancellationToken);

        Task<bool> SendUpdateStatusEmail(int jobId, JobStatuses status, CancellationToken cancellationToken);

        Task<List<JobSummary>> FilterJobSummaries(List<JobSummary> jobs, List<SupportActivities> supportActivities, string postcode, double? distanceInMiles, Dictionary<SupportActivities, double?> activitySpecificSupportDistancesInMiles, CancellationToken cancellationToken);
    }
}
