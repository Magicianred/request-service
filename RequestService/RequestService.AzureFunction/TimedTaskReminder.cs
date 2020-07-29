using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using RequestService.Core.Services;
using System.Threading;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Response;

namespace RequestService.AzureFunction
{
    public class TimedTaskReminder
    {
        private readonly ICommunicationService _commmunicationService;

        public TimedTaskReminder(ICommunicationService commmunicationService)
        {
            _commmunicationService = commmunicationService;
        }

        [FunctionName("TimedTaskReminder")]
        public async Task Run([TimerTrigger("%TimedTaskReminderCronExpression%")] TimerInfo timerInfo, ILogger log, CancellationToken cancellationToken)
        {
            try
            {
                log.LogInformation($"TaskReminder started at: {DateTime.Now}");
                await _commmunicationService.RequestCommunication(new RequestCommunicationRequest()
                {
                    CommunicationJob = new CommunicationJob() { CommunicationJobType = CommunicationJobTypes.SendTaskReminder}
                }, cancellationToken);
                log.LogInformation($"TaskReminder completed at: {DateTime.Now}");

            }
            catch (Exception ex)
            {
                log.LogError($"Unhandled error in TaskReminder {ex}");
            }

        }
    }
}