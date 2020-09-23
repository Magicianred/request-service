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
        Task<List<JobSummary>> AttachedDistanceToJobSummaries(string volunteerPostCode, List<JobSummary> jobHeaders, CancellationToken cancellationToken);
        Task<List<JobHeader>> AttachedDistanceToJobHeaders(string volunteerPostCode, List<JobHeader> jobHeaders, CancellationToken cancellationToken);
        Task<bool> HasPermissionToChangeStatusAsync(int jobID, int createdByUserID, CancellationToken cancellationToken);
    }
}
