using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;

namespace RequestService.Handlers
{
    public class GetJobSummaryHandler : IRequestHandler<GetJobSummaryRequest, GetJobSummaryResponse>
    {
        private readonly IRepository _repository;
        public GetJobSummaryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetJobSummaryResponse> Handle(GetJobSummaryRequest request, CancellationToken cancellationToken)
        {
            return _repository.GetJobSummary(request.JobID);
        }
    }
}
