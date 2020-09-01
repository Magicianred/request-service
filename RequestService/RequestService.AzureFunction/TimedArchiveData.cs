using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using RequestService.Core.Services;
using System.Threading;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;

namespace RequestService.AzureFunction
{
    public class TimedArchiveData
    {
        private readonly IArchiveService _archiveService;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;

        public TimedArchiveData(IArchiveService archiveService, IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _archiveService = archiveService;
            _applicationConfig = applicationConfig;
        }

        [FunctionName("TimedArchiveData")]
        public async Task Run([TimerTrigger("%TimedArchiveDataCronExpression%")] TimerInfo timerInfo, ILogger log, CancellationToken cancellationToken)
        {
            try
            {
                log.LogInformation($"TimedArchiveData started at: {DateTime.Now}");
                int daysSinceJobRequested = _applicationConfig.Value.DaysSinceJobRequested;
                int daysSinceJobStatusChanged = _applicationConfig.Value.DaysSinceJobStatusChanged;

                _archiveService.ArchiveOldRequests(daysSinceJobRequested, daysSinceJobStatusChanged);
                log.LogInformation($"TimedArchiveData completed at: {DateTime.Now}");

            }
            catch (Exception ex)
            {
                log.LogError($"Unhandled error in TimedArchiveData {ex}");
            }

        }
    }
}