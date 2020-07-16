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

namespace RequestService.AzureFunction
{
    public class GetJobsInProgress
    {
        private readonly IMediator _mediator;

        public GetJobsInProgress(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetJobsInProgress")]        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetJobsInProgressResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            [RequestBodyType(typeof(GetJobsInProgressRequest), "jobs in progress request")] GetJobsInProgressRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                GetJobsInProgressResponse response = await _mediator.Send(req); 
                return new OkObjectResult(ResponseWrapper<GetJobsInProgressResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError("Exception occured in jobs in progress", exc);
                return new ObjectResult(ResponseWrapper<GetJobsInProgressResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
