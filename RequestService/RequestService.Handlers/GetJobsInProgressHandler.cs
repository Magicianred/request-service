using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using System.Collections.Generic;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;

namespace RequestService.Handlers
{
    public class GetJobsInProgressHandler : IRequestHandler<GetJobsInProgressRequest, GetJobsInProgressResponse>
    {
        private readonly IRepository _repository;
        public GetJobsInProgressHandler(
            IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetJobsInProgressResponse> Handle(GetJobsInProgressRequest request, CancellationToken cancellationToken)
        {
            GetJobsInProgressResponse result = new GetJobsInProgressResponse() { JobSummaries = new List<JobSummary>() };
            List<JobSummary> jobSummaries = _repository.GetJobsInProgressSummaries();

            result = new GetJobsInProgressResponse()
            {
                JobSummaries = jobSummaries
            };
            return result;
        }

    }
}
