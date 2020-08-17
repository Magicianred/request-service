using HelpMyStreet.Contracts.RequestService.Request;
using MediatR;

namespace RequestService.Core.Domains.Entities
{
    public class GetJobsByStatusesRequest : IRequest<GetJobsByStatusesResponse>
    {
        public JobStatusRequest JobStatuses { get; set; }
    }
}
