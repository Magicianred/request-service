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
            string postCode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.Postcode);
            
            response = new LogRequestResponse
            {
                RequestID = await _repository.CreateRequest(request.Postcode)
            };

            int championCount = await _userService.GetChampionCountByPostcode(request.Postcode, cancellationToken);
           if (championCount > 0) {
                response.Fulfillable = true;                 
                await _repository.UpdateFulfillment(response.RequestID, true);
             }
           
            return response;
        }
    }
}
