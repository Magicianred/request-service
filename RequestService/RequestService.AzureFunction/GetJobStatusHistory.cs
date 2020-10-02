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
    public class GetJobStatusHistory
    {
        private readonly IMediator _mediator;

        public GetJobStatusHistory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetJobStatusHistory")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetJobStatusHistoryResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            [RequestBodyType(typeof(GetJobStatusHistoryRequest), "get job status history")] GetJobStatusHistoryRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                GetJobStatusHistoryResponse response = await _mediator.Send(req); 
                return new OkObjectResult(ResponseWrapper<GetJobStatusHistoryResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError("Exception occured in get job status history", exc);
                return new ObjectResult(ResponseWrapper<GetJobStatusHistoryResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
