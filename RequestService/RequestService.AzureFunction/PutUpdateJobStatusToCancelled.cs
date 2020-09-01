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

namespace RequestService.AzureFunction
{
    public class PutUpdateJobStatusToCancelled
    {
        private readonly IMediator _mediator;

        public PutUpdateJobStatusToCancelled(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("PutUpdateJobStatusToCancelled")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)]
            PutUpdateJobStatusToCancelledRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                PutUpdateJobStatusToCancelledResponse response = await _mediator.Send(req); 
                return new OkObjectResult(ResponseWrapper<PutUpdateJobStatusToCancelledResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError("Exception occured in Log Request", exc);
                return new ObjectResult(ResponseWrapper<PutUpdateJobStatusToCancelledResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
