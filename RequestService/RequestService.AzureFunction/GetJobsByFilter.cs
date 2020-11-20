using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.Shared;
using Microsoft.AspNetCore.Http;
using RequestService.Core.Exceptions;
using System.Net;
using HelpMyStreet.Utils.Utils;
using System.Threading;

namespace RequestService.AzureFunction
{
    public class GetJobsByFilter
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<GetJobsByFilterRequest> _logger;

        public GetJobsByFilter(IMediator mediator, ILoggerWrapper<GetJobsByFilterRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("GetJobsByFilter")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetJobsByFilterResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            GetJobsByFilterRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetJobsByFilter started");
                GetJobsByFilterResponse response = await _mediator.Send(req, cancellationToken); 
                return new OkObjectResult(ResponseWrapper<GetJobsByFilterResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch(PostCodeException exc)
            {
                _logger.LogErrorAndNotifyNewRelic($"{req.Postcode} is an invalid postcode", exc);
                return new ObjectResult(ResponseWrapper<GetJobsByFilterResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.ValidationError, "Invalid Postcode")) { StatusCode = StatusCodes.Status400BadRequest };
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in GetJobsByFilter", exc);
                return new ObjectResult(ResponseWrapper<GetJobsByFilterResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
