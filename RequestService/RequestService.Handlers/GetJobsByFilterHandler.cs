using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using UserService.Core.Utils;

namespace RequestService.Handlers
{
    public class GetJobsByFilterHandler : IRequestHandler<GetJobsByFilterRequest, GetJobsByFilterResponse>
    {
        private readonly IRepository _repository;
        private readonly IJobService _jobService;
        public GetJobsByFilterHandler(
            IRepository repository,
            IJobService jobService)
        {
            _repository = repository;
            _jobService = jobService;
        }

        public async Task<GetJobsByFilterResponse> Handle(GetJobsByFilterRequest request, CancellationToken cancellationToken)
        {
            GetJobsByFilterResponse result = new GetJobsByFilterResponse();
            List<JobSummary> jobSummaries = _repository.GetOpenJobsSummaries();

            await _jobService.GetJobSummaries(request.Postcode, jobSummaries, cancellationToken);

            if(jobSummaries.Count==0)
            {
                return result;
            }

            result = new GetJobsByFilterResponse()
            {
                JobSummaries = jobSummaries.Where(w => w.DistanceInMiles<=request.DistanceInMiles).ToList()
            };
            return result;
        }
    }
}
