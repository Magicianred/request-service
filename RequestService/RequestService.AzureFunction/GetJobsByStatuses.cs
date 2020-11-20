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
    public class GetJobsByStatuses
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<GetJobsByStatusesRequest> _logger;

        public GetJobsByStatuses(IMediator mediator, ILoggerWrapper<GetJobsByStatusesRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("GetJobsByStatuses")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetJobsByStatusesResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            [RequestBodyType(typeof(GetJobsByStatusesRequest), "jobs by statuses request")] GetJobsByStatusesRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetJobsByStatuses started");
                GetJobsByStatusesResponse response = await _mediator.Send(req,cancellationToken); 
                return new OkObjectResult(ResponseWrapper<GetJobsByStatusesResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in GetJobsByStatuses", exc);
                return new ObjectResult(ResponseWrapper<GetJobsByStatusesResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
