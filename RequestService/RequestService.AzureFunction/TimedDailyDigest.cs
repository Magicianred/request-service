using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
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