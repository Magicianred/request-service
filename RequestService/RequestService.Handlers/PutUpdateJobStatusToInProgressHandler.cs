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
    public class PutUpdateJobStatusToInProgressHandler : IRequestHandler<PutUpdateJobStatusToInProgressRequest, PutUpdateJobStatusToInProgressResponse>
    {
        private readonly IRepository _repository;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public PutUpdateJobStatusToInProgressHandler(IRepository repository, IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _applicationConfig = applicationConfig;
        }

        public async Task<PutUpdateJobStatusToInProgressResponse> Handle(PutUpdateJobStatusToInProgressRequest request, CancellationToken cancellationToken)
        {
            PutUpdateJobStatusToInProgressResponse result = new PutUpdateJobStatusToInProgressResponse()
            {
                Success = true
            };
            return result;
        }
    }
}
