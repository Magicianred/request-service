using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using System.Collections.Generic;
using HelpMyStreet.Utils.Models;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;

namespace RequestService.Handlers
{
    public class GetJobsByStatusesHandler : IRequestHandler<GetJobsByStatusesRequest, GetJobsByStatusesResponse>
    {
        private readonly IRepository _repository;
        public GetJobsByStatusesHandler(
            IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetJobsByStatusesResponse> Handle(GetJobsByStatusesRequest request, CancellationToken cancellationToken)
        {
            GetJobsByStatusesResponse result = new GetJobsByStatusesResponse() { JobSummaries = new List<JobSummary>() };
            List<JobSummary> jobSummaries = _repository.GetJobsByStatusesSummaries(request.JobStatuses.JobStatuses);

            result = new GetJobsByStatusesResponse()
            {
                JobSummaries = jobSummaries
            };
            return result;
        }

    }
}
