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
using HelpMyStreet.Contracts.ReportService.Response;
using RequestService.Core.Domains.Entities;
using RequestService.Core.Services;
using System.Threading;

namespace RequestService.AzureFunction
{
    public class TimedDailyDigest
    {

        private readonly IDailyDigestService _dailyDigestService;
        public TimedDailyDigest(IDailyDigestService dailyDigestService)
        {
            _dailyDigestService = dailyDigestService;
        }

        [FunctionName("TimedDailyDigest")]
        public async Task Run([TimerTrigger("%TimedDailyDigestCronExpression%")] TimerInfo timerInfo, ILogger log, CancellationToken cancellationToken)
        {
            try
            {
                log.LogInformation($"GetDailyDigest started at: {DateTime.Now}");
                await _dailyDigestService.SendDailyDigestEmailAsync(cancellationToken);
                log.LogInformation($"GetDailyDigest completed at: {DateTime.Now}");

            }
            catch (Exception ex)
            {
                log.LogError($"Unhandled error in GetDailyDigest {ex}");
            }

        }
    }
}