﻿using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Extensions;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public class DailyDigestService : IDailyDigestService
    {
        private readonly IUserService _userService;
        private readonly IJobService _jobService;
        private readonly IRepository _repository;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        private readonly ICommunicationService _communicationService;
        private readonly ILogger<DailyDigestService> _logger;

        public DailyDigestService(IUserService userService, IJobService jobService, IOptionsSnapshot<ApplicationConfig> applicationConfig,
            ICommunicationService communicationService,
            IRepository repository,
            ILogger<DailyDigestService> logger)
        {
            _userService = userService;
            _jobService = jobService;
            _repository = repository;
            _communicationService = communicationService;
            _applicationConfig = applicationConfig;
            _logger = logger;
        }


        public async Task SendDailyDigestEmailAsync( CancellationToken cancellationToken)
        {

            var openRequests = _repository.GetOpenJobsSummaries();
            
            if (openRequests.Count == 0)
            {
                _logger.LogWarning($"No Open Job Summaries Found when generating daily digest");
                return;
            }

            var users = await _userService.GetUsers(cancellationToken);
                        
            if (users == null || users.UserDetails == null ||  users.UserDetails.Count() == 0)
            {
                _logger.LogWarning($"No Users found when generating daily digest");
                return;
            }

            users.UserDetails = users.UserDetails.Where(x => x.SupportRadiusMiles.HasValue);

            foreach (var user in users.UserDetails)
            {

                if (!user.SupportActivities.Contains(SupportActivities.CommunityConnector))
                {
                    openRequests = openRequests.Where(x => x.SupportActivity != SupportActivities.CommunityConnector).ToList();
                };

                var attachedDistances = await _jobService.AttachedDistanceToJobSummaries(user.PostCode, openRequests, cancellationToken);
                if (attachedDistances == null || !attachedDistances.Any())
                {
                    _logger.LogWarning($"Could not find Latitude and Longtitude for postcode {user.PostCode}, UserID: {user.UserID}");
                    continue;
                }

                attachedDistances = attachedDistances.Where(w => w.DistanceInMiles <= _applicationConfig.Value.DistanceInMilesForDailyDigest).ToList();

                //if they dont have the community connector support activity, let remove any open requests in there.
            

                var (criteriaJobs, otherJobs) = attachedDistances.Split(x => user.SupportActivities.Contains(x.SupportActivity) && x.DistanceInMiles < user.SupportRadiusMiles);

                var criteraJobSummaries = criteriaJobs.OrderOpenJobsForDisplay().Select(x => new OpenJobRequestDTO
                {
                    Distance = x.DistanceInMiles,
                    DueDate = x.DueDate,
                    IsCritical = x.IsHealthCritical,
                    Postcode = x.PostCode,
                    SupportActivity = x.SupportActivity,
                    EncodedJobID = HelpMyStreet.Utils.Utils.Base64Utils.Base64Encode(x.JobID.ToString()),
                }).ToList();

                var otherJobSummaries = otherJobs.OrderOpenJobsForDisplay().Select(x => new OpenJobRequestDTO
                {
                    Distance = x.DistanceInMiles,
                    DueDate = x.DueDate,
                    IsCritical = x.IsHealthCritical,
                    Postcode = x.PostCode,
                    SupportActivity = x.SupportActivity,
                    EncodedJobID = HelpMyStreet.Utils.Utils.Base64Utils.Base64Encode(x.JobID.ToString()),
                }).ToList();

                if (criteraJobSummaries.Count() > 0 || otherJobSummaries.Count() > 0)
                {
                    try
                    {
                        SendEmailToUserRequest request = new SendEmailToUserRequest
                        {
                            ToUserID = user.UserID,
                            Subject = $"Help needed in your area - {DateTime.Now.ToString("dd/MM/yy")}",
                            BodyHTML = EmailBuilder.BuildDailyDigestEmail(criteraJobSummaries, otherJobSummaries, _applicationConfig.Value.EmailBaseUrl, (user.IsVerified.HasValue && user.IsVerified.Value)),
                        };

                        bool emailSent = await _communicationService.SendEmailToUserAsync(request, cancellationToken);

                        if (!emailSent) throw new ApplicationException($"Daily Digest email not sent to UserID: {user.UserID}");

                        _logger.LogInformation($"Daily Digest Email Sent to UserID: {user.UserID}");
                        
                    }
                    catch (WebException ex)
                    {
                        _logger.LogError($"Could not send email to userID: {user.UserID}", ex);
                        var response = (HttpWebResponse)ex.Response;
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.NotFound:
                            case HttpStatusCode.InternalServerError:
                            case HttpStatusCode.ServiceUnavailable:
                            return;                                                                
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }            
        }
    }
}
