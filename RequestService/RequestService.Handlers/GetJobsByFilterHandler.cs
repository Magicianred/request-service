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

namespace RequestService.Handlers
{
    public class GetJobsByFilterHandler : IRequestHandler<GetJobsByFilterRequest, GetJobsByFilterResponse>
    {
        private readonly IRepository _repository;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public GetJobsByFilterHandler(IRepository repository, IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _applicationConfig = applicationConfig;
        }

        public async Task<GetJobsByFilterResponse> Handle(GetJobsByFilterRequest request, CancellationToken cancellationToken)
        {
            List<HelpMyStreet.Utils.Models.JobSummary> jobSummaries = new List<HelpMyStreet.Utils.Models.JobSummary>();
            jobSummaries.Add(new HelpMyStreet.Utils.Models.JobSummary()
            {
                UniqueIdentifier = Guid.NewGuid(),
                IsHealthCritical = true,
                Details = "Job Details",
                DueDate = DateTime.Now.AddDays(5),
                SupportActivity = SupportActivities.DogWalking,
                JobStatus = JobStatuses.InProgress,
                PostCode = "NG1 6DQ"
            });

            GetJobsByFilterResponse result = new GetJobsByFilterResponse()
            {
                JobSummaries = jobSummaries
            };
            return result;
        }
    }
}
