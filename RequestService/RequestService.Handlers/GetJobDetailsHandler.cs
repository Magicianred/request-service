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
    public class GetJobDetailsHandler : IRequestHandler<GetJobDetailsRequest, GetJobDetailsResponse>
    {
        private readonly IRepository _repository;
        public GetJobDetailsHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetJobDetailsResponse> Handle(GetJobDetailsRequest request, CancellationToken cancellationToken)
        {
            GetJobDetailsResponse response = _repository.GetJobDetails(request.JobID);
            return response;
        }
    }
}
