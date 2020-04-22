
using RequestService.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Services;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;

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
            request.Postcode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.Postcode);
            
            response = new LogRequestResponse
            {
                RequestID = await _repository.CreateRequestAsync(request.Postcode, cancellationToken)
            };

           int championCount = await _userService.GetChampionCountByPostcode(request.Postcode, cancellationToken);
           if (championCount > 0) {
                response.Fulfillable = true;                 
                await _repository.UpdateFulfillmentAsync(response.RequestID, response.Fulfillable, cancellationToken);
             }
           
            return response;
        }
    }
}
