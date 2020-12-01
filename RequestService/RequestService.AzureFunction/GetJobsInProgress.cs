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
    public class GetJobsInProgress
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<GetJobsInProgressRequest> _logger;

        public GetJobsInProgress(IMediator mediator, ILoggerWrapper<GetJobsInProgressRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("GetJobsInProgress")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetJobsInProgressResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            [RequestBodyType(typeof(GetJobsInProgressRequest), "jobs in progress request")] GetJobsInProgressRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetJobsInProgress started");
                GetJobsInProgressResponse response = await _mediator.Send(req, cancellationToken); 
                return new OkObjectResult(ResponseWrapper<GetJobsInProgressResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in GetJobsInProgress", exc);
                return new ObjectResult(ResponseWrapper<GetJobsInProgressResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
