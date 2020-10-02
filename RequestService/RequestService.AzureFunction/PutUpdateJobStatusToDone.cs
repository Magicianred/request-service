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

namespace RequestService.AzureFunction
{
    public class PutUpdateJobStatusToDone
    {
        private readonly IMediator _mediator;

        public PutUpdateJobStatusToDone(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("PutUpdateJobStatusToDone")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PutUpdateJobStatusToDoneResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)]
            [RequestBodyType(typeof(PutUpdateJobStatusToDoneRequest), "put update job status to done request")] PutUpdateJobStatusToDoneRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                PutUpdateJobStatusToDoneResponse response = await _mediator.Send(req); 
                return new OkObjectResult(ResponseWrapper<PutUpdateJobStatusToDoneResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError("Exception occured in Log Request", exc);
                return new ObjectResult(ResponseWrapper<PutUpdateJobStatusToDoneResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
