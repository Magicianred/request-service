using RequestService.Core.Domains.Entities;
using RequestService.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Services;

namespace RequestService.Handlers
{
    public class LogRequestHandler : IRequestHandler<LogRequestRequest, LogRequestResponse>
    {
        private readonly IRepository _repository;
        private readonly IUserService _userService;

        public LogRequestHandler(IRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<LogRequestResponse> Handle(LogRequestRequest request, CancellationToken cancellationToken)
        {
            LogRequestResponse response = null;
            int championCount = await _userService.GetChampionCountByPostcode(request.Postcode, cancellationToken);
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
            return response;
        }
    }
}
