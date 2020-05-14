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
    public class PutUpdateJobStatusToDoneHandler : IRequestHandler<PutUpdateJobStatusToDoneRequest, PutUpdateJobStatusToDoneResponse>
    {
        private readonly IRepository _repository;
        public PutUpdateJobStatusToDoneHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<PutUpdateJobStatusToDoneResponse> Handle(PutUpdateJobStatusToDoneRequest request, CancellationToken cancellationToken)
        {
            var result = await  _repository.UpdateJobStatusDoneAsync(request.JobID, request.CreatedByUserID, cancellationToken);
            PutUpdateJobStatusToDoneResponse response = new PutUpdateJobStatusToDoneResponse()
            {
                Success = result
            };
            return response;
        }
    }
}
