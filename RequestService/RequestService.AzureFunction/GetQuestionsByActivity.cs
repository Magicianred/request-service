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
using RequestService.Core.Exceptions;

namespace RequestService.AzureFunction
{
    public class GetQuestionsByActivity
    {
        private readonly IMediator _mediator;

        public GetQuestionsByActivity(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetQuestionsByActivity")]        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetQuestionsByActivtiesResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            [RequestBodyType(typeof(GetQuestionsByActivitiesRequest), "log request")] GetQuestionsByActivitiesRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                GetQuestionsByActivtiesResponse response = await _mediator.Send(req); 
                return new OkObjectResult(ResponseWrapper<GetQuestionsByActivtiesResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }        
            catch (Exception exc)
            {
                log.LogError("Exception occured in Log Request", exc);
                return new ObjectResult(ResponseWrapper<GetQuestionsByActivtiesResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
