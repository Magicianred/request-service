
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
using System.Text.RegularExpressions;

namespace RequestService.Handlers
{
    public class LogRequestHandler : IRequestHandler<LogRequestRequest, LogRequestResponse>
    {
        private readonly IRepository _repository;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        public LogRequestHandler(IRepository repository, IUserService userService, IAddressService addressService)
        {
            _repository = repository;
            _userService = userService;
            _addressService = addressService;
        }

        public async Task<LogRequestResponse> Handle(LogRequestRequest request, CancellationToken cancellationToken)
        {
            LogRequestResponse response = null;
            request.Postcode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.Postcode);
            var postcodeValid =  await _addressService.IsValidPostcode(request.Postcode, cancellationToken);
                       
            if (!postcodeValid || request.Postcode.Length > 10)
            {
               return  new LogRequestResponse
                {
                    RequestID = -1,
                    Fulfillable = Fulfillable.Rejected_InvalidPostcode                 
                };
            }

            response = new LogRequestResponse
            {
                RequestID = await _repository.CreateRequestAsync(request.Postcode, cancellationToken)
            };

           int championCount = await _userService.GetChampionCountByPostcode(request.Postcode, cancellationToken);
            if (championCount > 0) {
                response.Fulfillable = Fulfillable.Accepted_PassToStreetChampion;
            }
            else
            {
                response.Fulfillable = Fulfillable.Accepted_ManualReferral;
            }
            await _repository.UpdateFulfillmentAsync(response.RequestID, response.Fulfillable, cancellationToken);

            return response;
        }
    }
}
