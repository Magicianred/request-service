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
    public class GetJobsAllocatedToUser
    {
        private readonly IMediator _mediator;

        public GetJobsAllocatedToUser(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetJobsAllocatedToUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            GetJobsAllocatedToUserRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                GetJobsAllocatedToUserResponse response = await _mediator.Send(req); 
                return new OkObjectResult(ResponseWrapper<GetJobsAllocatedToUserResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError("Exception occured in Log Request", exc);
                return new ObjectResult(ResponseWrapper<GetJobsAllocatedToUserResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
