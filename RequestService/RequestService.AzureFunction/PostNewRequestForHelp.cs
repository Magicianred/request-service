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
    public class PostNewRequestForHelp
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<PostNewRequestForHelpRequest> _logger;

        public PostNewRequestForHelp(IMediator mediator, ILoggerWrapper<PostNewRequestForHelpRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("PostNewRequestForHelp")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PostNewRequestForHelpResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            [RequestBodyType(typeof(PostNewRequestForHelpRequest), "post new request for help request")] PostNewRequestForHelpRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("PostNewRequestForHelp started");
                PostNewRequestForHelpResponse response = await _mediator.Send(req, cancellationToken);                
                return new OkObjectResult(ResponseWrapper<PostNewRequestForHelpResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in PostNewRequestForHelp", exc);
                return new ObjectResult(ResponseWrapper<PostNewRequestForHelpResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
