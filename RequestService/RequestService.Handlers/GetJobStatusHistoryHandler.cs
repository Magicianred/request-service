using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using HelpMyStreet.Contracts.RequestService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using HelpMyStreet.Contracts.RequestService.Response;

namespace RequestService.Handlers
{
    public class GetJobStatusHistoryHandler : IRequestHandler<GetJobStatusHistoryRequest, GetJobStatusHistoryResponse>
    {
        private readonly IRepository _repository;
        public GetJobStatusHistoryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetJobStatusHistoryResponse> Handle(GetJobStatusHistoryRequest request, CancellationToken cancellationToken)
        {
            var history = _repository.GetJobStatusHistory(request.JobID);

            return new GetJobStatusHistoryResponse()
            {
                History = history
            };
        }
    }
}
