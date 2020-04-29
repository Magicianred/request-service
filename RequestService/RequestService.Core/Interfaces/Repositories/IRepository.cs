using HelpMyStreet.Contracts.ReportService.Response;
using HelpMyStreet.Contracts.RequestService.Response;
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
        List<ReportItem> GetDailyReport();
        Task<int> CreateRequestAsync(string postCode, CancellationToken cancellationToken);
        Task UpdateFulfillmentAsync(int requestId, Fulfillable fulfillable, CancellationToken cancellationToken);
        Task AddSupportActivityAsync(SupportActivityDTO dto, CancellationToken cancellationToken);
        Task UpdatePersonalDetailsAsync(PersonalDetailsDto dto, CancellationToken cancellationToken);
        Task<string> GetRequestPostCodeAsync(int requestId, CancellationToken cancellationToken);
        Task UpdateCommunicationSentAsync(int requestId, bool communicationSent, CancellationToken cancellationToken);
    }
}
