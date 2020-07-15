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
using RequestService.Core.Exceptions;
using System.Net.Http;

namespace RequestService.Handlers
{
    public class GetJobsInProgressHandler : IRequestHandler<GetJobsInProgressRequest, GetJobsInProgressResponse>
    {
        private readonly IRepository _repository;
        private readonly IAddressService _addressService;
        private readonly IJobFilteringService _jobFilteringService;
        public GetJobsInProgressHandler(
            IRepository repository,
            IAddressService addressService,
            IJobFilteringService jobFilteringService)
        {
            _repository = repository;
            _addressService = addressService;
            _jobFilteringService = jobFilteringService;
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
