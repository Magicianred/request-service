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
using System.Net;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using HelpMyStreet.Utils.Utils;
using System.Threading;

namespace RequestService.AzureFunction
{
    public class GetJobDetails
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<GetJobDetailsRequest> _logger;

        public GetJobDetails(IMediator mediator, ILoggerWrapper<GetJobDetailsRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("GetJobDetails")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetJobDetailsResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            [RequestBodyType(typeof(GetJobDetailsRequest), "get job details request")] GetJobDetailsRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetJobDetails started");
                GetJobDetailsResponse response = await _mediator.Send(req,cancellationToken); 
                return new OkObjectResult(ResponseWrapper<GetJobDetailsResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in GetJobDetails", exc);
                return new ObjectResult(ResponseWrapper<GetJobDetailsResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
