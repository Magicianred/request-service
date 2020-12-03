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
    public class PutUpdateJobDueDate
    {
        private readonly IMediator _mediator;

        public PutUpdateJobDueDate(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("PutUpdateJobDueDate")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PutUpdateJobDueDateResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)]
            [RequestBodyType(typeof(PutUpdateJobDueDateRequest), "put update job due date request")] PutUpdateJobDueDateRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                PutUpdateJobDueDateResponse response = await _mediator.Send(req); 
                return new OkObjectResult(ResponseWrapper<PutUpdateJobDueDateResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError("Exception occured in PutUpdateJobDueDateResponse", exc);
                return new ObjectResult(ResponseWrapper<PutUpdateJobDueDateResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
