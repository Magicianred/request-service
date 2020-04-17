using RequestService.Core.Domains.Entities;
using RequestService.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Handlers
{
    public class LogRequestHandler : IRequestHandler<LogRequestRequest, LogRequestResponse>
    {
        private readonly IRepository _repository;

        public LogRequestHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Task<LogRequestResponse> Handle(LogRequestRequest request, CancellationToken cancellationToken)
        {
            LogRequestResponse response = null;
            switch (request.Postcode)
            {
                case "NG16DQ":
                    response = new LogRequestResponse()
                    {
                        RequestID = 1,
                        Fulfillable = true
                    };
                    break;
                default:
                    response = new LogRequestResponse()
                    {
                        RequestID = 2,
                        Fulfillable = false
                    };
                    break;
            }
            return Task.FromResult(response);
        }
    }
}
