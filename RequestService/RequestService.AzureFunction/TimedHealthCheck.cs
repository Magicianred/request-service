using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace RequestService.AzureFunction
{
    public class TimedHealthCheck
    {
        [FunctionName("TimedHealthCheck")]
        public void Run([TimerTrigger("%TimedHealthCheckCronExpression%")] TimerInfo timerInfo, ILogger log)
        {
            log.LogInformation($"Health check CRON trigger executed at : {DateTime.Now}");
        }
    }
}
