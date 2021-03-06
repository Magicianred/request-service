﻿using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using RequestService.Core.Services;
using System.Threading;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Response;

namespace RequestService.AzureFunction
{
    public class TimedDailyDigest
    {
        private readonly ICommunicationService _commmunicationService;

        public TimedDailyDigest(ICommunicationService commmunicationService)
        {
            _commmunicationService = commmunicationService;
        }

        [FunctionName("TimedDailyDigest")]
        public async Task Run([TimerTrigger("%TimedDailyDigestCronExpression%")] TimerInfo timerInfo, ILogger log, CancellationToken cancellationToken)
        {
            try
            {
                log.LogInformation($"GetDailyDigest started at: {DateTime.Now}");
                await _commmunicationService.RequestCommunication(new RequestCommunicationRequest()
                {
                    CommunicationJob = new CommunicationJob() { CommunicationJobType = CommunicationJobTypes.SendOpenTaskDigest}
                }, cancellationToken);
                log.LogInformation($"GetDailyDigest completed at: {DateTime.Now}");

            }
            catch (Exception ex)
            {
                log.LogError($"Unhandled error in GetDailyDigest {ex}");
            }

        }
    }
}