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
    public class PutUpdateJobStatusToOpen
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<PutUpdateJobStatusToOpenRequest> _logger;

        public PutUpdateJobStatusToOpen(IMediator mediator, ILoggerWrapper<PutUpdateJobStatusToOpenRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("PutUpdateJobStatusToOpen")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PutUpdateJobStatusToOpenResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)]
            [RequestBodyType(typeof(PutUpdateJobStatusToOpenRequest), "put update job status to open request")] PutUpdateJobStatusToOpenRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("PutUpdateJobStatusToOpen started");
                PutUpdateJobStatusToOpenResponse response = await _mediator.Send(req, cancellationToken); 
                return new OkObjectResult(ResponseWrapper<PutUpdateJobStatusToOpenResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in PutUpdateJobStatusToOpen", exc);
                return new ObjectResult(ResponseWrapper<PutUpdateJobStatusToOpenResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
