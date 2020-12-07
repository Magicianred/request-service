using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using HelpMyStreet.Contracts.RequestService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using HelpMyStreet.Contracts.RequestService.Response;
using RequestService.Core.Services;

namespace RequestService.Handlers
{
    public class GetJobDetailsHandler : IRequestHandler<GetJobDetailsRequest, GetJobDetailsResponse>
    {
        private readonly IRepository _repository;
        private readonly IJobService _jobService;
        private const int ADMIN_USERID = -1;
        public GetJobDetailsHandler(IRepository repository, IJobService jobService)
        {
            _repository = repository;
            _jobService = jobService;
        }

        public async Task<GetJobDetailsResponse> Handle(GetJobDetailsRequest request, CancellationToken cancellationToken)
        {
            bool hasPermission = true;
            GetJobDetailsResponse response = null;

            if (request.UserID != ADMIN_USERID)
            {
                hasPermission = await _jobService.HasPermissionToChangeStatusAsync(request.JobID, request.UserID, true, cancellationToken);
            }

            if (hasPermission)
            {
                response = _repository.GetJobDetails(request.JobID);
            }
            return response;
        }
    }
}
