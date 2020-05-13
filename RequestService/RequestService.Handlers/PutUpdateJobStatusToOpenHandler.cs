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
    public class PutUpdateJobStatusToOpenHandler : IRequestHandler<PutUpdateJobStatusToOpenRequest, PutUpdateJobStatusToOpenResponse>
    {
        private readonly IRepository _repository;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public PutUpdateJobStatusToOpenHandler(IRepository repository, IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _applicationConfig = applicationConfig;
        }

        public async Task<PutUpdateJobStatusToOpenResponse> Handle(PutUpdateJobStatusToOpenRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdateJobStatusOpenAsync(request.JobID, request.CreatedByUserID, cancellationToken);
            PutUpdateJobStatusToOpenResponse response = new PutUpdateJobStatusToOpenResponse()
            {
                Success = result
            };
            return response;
        }
    }
}
