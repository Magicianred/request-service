using HelpMyStreet.Contracts.ReportService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using RequestService.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Interfaces.Repositories
{
    public interface IRepository
    {
        Task AddJobAvailableToGroupAsync(int jobID, int groupID, CancellationToken cancellationToken);
        GetJobDetailsResponse GetJobDetails(int jobID);
        List<StatusHistory> GetJobStatusHistory(int jobID);
        List<JobSummary> GetJobSummaries();
        List<JobSummary> GetOpenJobsSummaries();
        List<JobSummary> GetJobsInProgressSummaries();
        List<JobSummary> GetJobsAllocatedToUser(int volunteerUserID);
        Task<bool> UpdateJobStatusCancelledAsync(int jobID, int createdByUserID, CancellationToken cancellationToken);
        Task<bool> UpdateJobStatusOpenAsync(int jobID, int createdByUserID, CancellationToken cancellationToken);
        Task<bool> UpdateJobStatusInProgressAsync(int jobID, int createdByUserID, int volunteerUserID, CancellationToken cancellationToken);
        Task<bool> UpdateJobStatusDoneAsync(int jobID, int createdByUserID, CancellationToken cancellationToken);
        Task<int> NewHelpRequestAsync(PostNewRequestForHelpRequest postNewRequestForHelpRequest, Fulfillable fulfillable);
        List<ReportItem> GetDailyReport();
        Task<int> CreateRequestAsync(string postCode, CancellationToken cancellationToken);
        Task UpdateFulfillmentAsync(int requestId, Fulfillable fulfillable, CancellationToken cancellationToken);
        Task AddSupportActivityAsync(SupportActivityDTO dto, CancellationToken cancellationToken);
        Task UpdatePersonalDetailsAsync(PersonalDetailsDto dto, CancellationToken cancellationToken);
        Task<string> GetRequestPostCodeAsync(int requestId, CancellationToken cancellationToken);
        Task UpdateCommunicationSentAsync(int requestId, bool communicationSent, CancellationToken cancellationToken);
        Task<List<LatitudeAndLongitudeDTO>> GetLatitudeAndLongitudes(List<string> postCodes, CancellationToken cancellationToken);
        Task<List<ActivityQuestionDTO>> GetActivityQuestions(List<SupportActivities> activity, RequestHelpFormVariant requestHelpFormVariant, RequestHelpFormStage requestHelpFormStage, CancellationToken cancellationToken);
        List<JobSummary> GetJobsByStatusesSummaries(List<JobStatuses> jobStatuses);
    }
}
