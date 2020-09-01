using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.Shared;
using Microsoft.AspNetCore.Http;
using HelpMyStreet.Contracts.ReportService.Response;
using RequestService.Core.Domains.Entities;

namespace RequestService.AzureFunction
{
    public class GetReport
    {
        private readonly IMediator _mediator;

        public GetReport(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("GetReport")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            GetReportRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                GetReportResponse response = await _mediator.Send(req);                
                return new OkObjectResult(ResponseWrapper<GetReportResponse, RequestServiceErrorCode>.CreateSuccessfulResponse(response));
            }
            catch (Exception exc)
            {
                log.LogError("Exception occured in Log Request", exc);
                return new ObjectResult(ResponseWrapper<GetReportResponse, RequestServiceErrorCode>.CreateUnsuccessfulResponse(RequestServiceErrorCode.InternalServerError, "Internal Error")) { StatusCode = StatusCodes.Status500InternalServerError };                
            }
        }
    }
}
