using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RequestService.Core.Services;

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
        public void Run([TimerTrigger("%TimedDailyDigestCronExpression%", RunOnStartup = true)] TimerInfo timerInfo, ILogger log)
        {
            _dailyDigestService.GenerateEmailsAsync();
        }
    }
}
