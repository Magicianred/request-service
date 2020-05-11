using AzureFunctions.Extensions.Swashbuckle.Attribute;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using RequestService.Core.Contracts;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RequestService.AzureFunction
{
    public class GetRequestSummaryCoordinates
    {
        private readonly IMediator _mediator;

        public GetRequestSummaryCoordinates(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Transaction(Web = true)]
        [FunctionName(nameof(GetRequestSummaryCoordinates))]        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetRequestSummaryCoordinatesResponse))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            [RequestBodyType(typeof(GetRequestSummaryCoordinatesRequest), "log request")] GetRequestSummaryCoordinatesRequest req,
            ILogger log)
        {
            try
            {
                NewRelic.Api.Agent.NewRelic.SetTransactionName("RequestService", nameof(GetRequestSummaryCoordinates));
                log.LogInformation("C# HTTP trigger function processed a request.");
                GetRequestSummaryCoordinatesResponse response = await _mediator.Send(req);                
                return new OkObjectResult(ResponseWrapper<GetRequestSummaryCoordinatesResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                LogError.Log(log, exc, req);
                return new ObjectResult(ResponseWrapper<GetRequestSummaryCoordinatesResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
