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
    public class PostNewRequestForHelp
    {
        private readonly IMediator _mediator;

        public PostNewRequestForHelp(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("PostNewRequestForHelp")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PostNewRequestForHelpResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            [RequestBodyType(typeof(PostNewRequestForHelpRequest), "post new request for help request")] PostNewRequestForHelpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("PostNewRequestForHelp started.");
                PostNewRequestForHelpResponse response = await _mediator.Send(req);                
                return new OkObjectResult(ResponseWrapper<PostNewRequestForHelpResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError($"Exception occured in PostNewRequestForHelp. Error {exc.ToString()}", exc);
                return new ObjectResult(ResponseWrapper<PostNewRequestForHelpResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
