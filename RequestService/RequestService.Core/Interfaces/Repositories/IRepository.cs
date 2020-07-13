using HelpMyStreet.Contracts.ReportService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
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
        List<JobSummary> GetJobSummaries();
        List<JobSummary> GetOpenJobsSummaries();
        List<JobSummary> GetJobsAllocatedToUser(int volunteerUserID);
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
        Task<List<ActivityQuestionDTO>> GetActivityQuestions(List<HelpMyStreet.Utils.Enums.SupportActivities> activity, CancellationToken cancellationToken);

    }
}
