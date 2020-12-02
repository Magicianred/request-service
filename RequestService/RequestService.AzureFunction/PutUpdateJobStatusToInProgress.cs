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
    public class PutUpdateJobStatusToInProgress
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<PutUpdateJobStatusToInProgressRequest> _logger;

        public PutUpdateJobStatusToInProgress(IMediator mediator, ILoggerWrapper<PutUpdateJobStatusToInProgressRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("PutUpdateJobStatusToInProgress")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PutUpdateJobStatusToInProgressResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)]
            [RequestBodyType(typeof(PutUpdateJobStatusToInProgressRequest), "put update job status to in progress request")] PutUpdateJobStatusToInProgressRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("PutUpdateJobStatusToInProgress started");
                PutUpdateJobStatusToInProgressResponse response = await _mediator.Send(req,cancellationToken); 
                return new OkObjectResult(ResponseWrapper<PutUpdateJobStatusToInProgressResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in PutUpdateJobStatusToInProgress", exc);
                return new ObjectResult(ResponseWrapper<PutUpdateJobStatusToInProgressResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
