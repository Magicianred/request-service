using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Net;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.Shared;
using HelpMyStreet.Contracts.RequestService.Response;
using Microsoft.AspNetCore.Http;

namespace RequestService.AzureFunction
{
    public class UpdateRequest
    {
        private readonly IMediator _mediator;

        public UpdateRequest(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("UpdateRequest")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,"post", Route = null)]
            [RequestBodyType(typeof(UpdateRequestRequest), "update request")] UpdateRequestRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                await _mediator.Send(req);                
                return new OkObjectResult(ResponseWrapper<NoContentResult, RequestServiceErrorCode>.CreateSuccessfulResponse(new NoContentResult()));                
            }
            catch (Exception exc)
            {

                log.LogError("Exception occured in Update Request", exc);
                return new ObjectResult(ResponseWrapper<LogRequestResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
