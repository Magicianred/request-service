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

namespace RequestService.Handlers
{
    public class PutUpdateJobStatusToCancelledHandler : IRequestHandler<PutUpdateJobStatusToCancelledRequest, PutUpdateJobStatusToCancelledResponse>
    {
        private readonly IRepository _repository;
        private readonly IJobService _jobService;
        public PutUpdateJobStatusToCancelledHandler(IRepository repository, IJobService jobService)
        {
            _repository = repository;
            _jobService = jobService;
        }

        public async Task<PutUpdateJobStatusToCancelledResponse> Handle(PutUpdateJobStatusToCancelledRequest request, CancellationToken cancellationToken)
        {
            PutUpdateJobStatusToCancelledResponse response = new PutUpdateJobStatusToCancelledResponse()
            {
                Outcome = UpdateJobStatusOutcome.Unauthorized
            };

            bool hasPermission = await _jobService.HasPermissionToChangeStatusAsync(request.JobID, request.CreatedByUserID,cancellationToken);

            if(hasPermission)
            {
                var result = await _repository.UpdateJobStatusCancelledAsync(request.JobID, request.CreatedByUserID, cancellationToken);

                if (result)
                {
                    response.Outcome = UpdateJobStatusOutcome.Success;
                }
                else
                {
                    response.Outcome = UpdateJobStatusOutcome.BadRequest;
                }
            }
            else
            {
                response.Outcome = UpdateJobStatusOutcome.Unauthorized;
            }
            return response;
        }
    }
}
