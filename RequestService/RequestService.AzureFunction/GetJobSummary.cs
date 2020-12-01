using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Net;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.Shared;
using Microsoft.AspNetCore.Http;
using HelpMyStreet.Utils.Utils;
using System.Threading;

namespace RequestService.AzureFunction
{
    public class GetJobSummary
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<GetJobSummaryRequest> _logger;

        public GetJobSummary(IMediator mediator, ILoggerWrapper<GetJobSummaryRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("GetJobSummary")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetJobSummaryResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            [RequestBodyType(typeof(GetJobSummaryRequest), "get job summary request")] GetJobSummaryRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetJobSummary started");
                GetJobSummaryResponse response = await _mediator.Send(req, cancellationToken); 
                return new OkObjectResult(ResponseWrapper<GetJobSummaryResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in GetJobSummary", exc);
                return new ObjectResult(ResponseWrapper<GetJobSummaryResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
