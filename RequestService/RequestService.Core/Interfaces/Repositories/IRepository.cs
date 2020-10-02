using HelpMyStreet.Contracts.ReportService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using RequestService.Core.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Interfaces.Repositories
{
    public interface IRepository
    {
        Task<int> GetReferringGroupIDForJobAsync(int jobID, CancellationToken cancellationToken);
        Task<List<int>> GetGroupsForJobAsync(int jobID, CancellationToken cancellationToken);
        Task AddJobAvailableToGroupAsync(int jobID, int groupID, CancellationToken cancellationToken);
        GetJobDetailsResponse GetJobDetails(int jobID);
        GetJobSummaryResponse GetJobSummary(int jobID);
        List<StatusHistory> GetJobStatusHistory(int jobID);
        List<JobSummary> GetOpenJobsSummaries();
        List<JobSummary> GetJobsInProgressSummaries();
        List<JobSummary> GetJobsAllocatedToUser(int volunteerUserID);
        Task<UpdateJobStatusOutcome> UpdateJobStatusCancelledAsync(int jobID, int createdByUserID, CancellationToken cancellationToken);
        Task<UpdateJobStatusOutcome> UpdateJobStatusOpenAsync(int jobID, int createdByUserID, CancellationToken cancellationToken);
        Task<UpdateJobStatusOutcome> UpdateJobStatusInProgressAsync(int jobID, int createdByUserID, int volunteerUserID, CancellationToken cancellationToken);
        Task<UpdateJobStatusOutcome> UpdateJobStatusDoneAsync(int jobID, int createdByUserID, CancellationToken cancellationToken);
        Task<int> NewHelpRequestAsync(PostNewRequestForHelpRequest postNewRequestForHelpRequest, Fulfillable fulfillable);
        List<ReportItem> GetDailyReport();
        Task<int> CreateRequestAsync(string postCode, CancellationToken cancellationToken);
        Task UpdateFulfillmentAsync(int requestId, Fulfillable fulfillable, CancellationToken cancellationToken);
        Task AddSupportActivityAsync(SupportActivityDTO dto, CancellationToken cancellationToken);
        Task UpdatePersonalDetailsAsync(PersonalDetailsDto dto, CancellationToken cancellationToken);
        Task<string> GetRequestPostCodeAsync(int requestId, CancellationToken cancellationToken);
        Task UpdateCommunicationSentAsync(int requestId, bool communicationSent, CancellationToken cancellationToken);
        Task<List<LatitudeAndLongitudeDTO>> GetLatitudeAndLongitudes(List<string> postCodes, CancellationToken cancellationToken);
        List<JobSummary> GetJobsByStatusesSummaries(List<JobStatuses> jobStatuses);
        List<JobHeader> GetJobHeaders(GetJobsByFilterRequest request);
        void ArchiveOldRequests(int daysSinceJobRequested, int daysSinceJobStatusChanged);        
        bool JobHasSameStatusAsProposedStatus(int jobID, JobStatuses newJobStatus);
        Task<List<Question>> GetQuestionsForActivity(SupportActivities activity, RequestHelpFormVariant requestHelpFormVariant, RequestHelpFormStage requestHelpFormStage, CancellationToken cancellationToken);
    }
}
