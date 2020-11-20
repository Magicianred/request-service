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
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using HelpMyStreet.Utils.Utils;
using System.Threading;

namespace RequestService.AzureFunction
{
    public class GetQuestionsByActivity
    {
        private readonly IMediator _mediator;
        private readonly ILoggerWrapper<GetQuestionsByActivitiesRequest> _logger;

        public GetQuestionsByActivity(IMediator mediator, ILoggerWrapper<GetQuestionsByActivitiesRequest> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName("GetQuestionsByActivity")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            [RequestBodyType(typeof(GetQuestionsByActivitiesRequest), "get questions by activities request")] GetQuestionsByActivitiesRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetQuestionsByActivity started");
                GetQuestionsByActivtiesResponse response = await _mediator.Send(req, cancellationToken); 
                return new OkObjectResult(ResponseWrapper<GetQuestionsByActivtiesResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }        
            catch (Exception exc)
            {
                _logger.LogErrorAndNotifyNewRelic("Exception occured in GetQuestionsByActivity", exc);
                return new ObjectResult(ResponseWrapper<GetQuestionsByActivtiesResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
