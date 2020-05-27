using HelpMyStreet.Contracts.CommunicationService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using System;
using System.Linq;
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

        public DailyDigestService(IUserService userService, IJobService jobService, IOptionsSnapshot<ApplicationConfig> applicationConfig,
            ICommunicationService communicationService,
            IRepository repository)
        {
            _userService = userService;
            _jobService = jobService;
            _repository = repository;
            _communicationService = communicationService;
            _applicationConfig = applicationConfig;
        }


        public async Task SendDailyDigestEmailAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsers(cancellationToken);
            if (users == null || users.UserDetails == null ||  users.UserDetails.Count() == 0) 
                return;

            var openRequests = _repository.GetOpenJobsSummaries();
            if (openRequests.Count == 0) 
                return;

            foreach (var user in users.UserDetails)
            {
                var attachedDistances = await _jobService.AttachedDistanceToJobSummaries(user.PostCode, openRequests, cancellationToken);
                if (!attachedDistances.Any()) continue;

                var jobSummaries = attachedDistances
                            .Where(w => w.DistanceInMiles <= _applicationConfig.Value.MaxDistanceDailyDigest)
                            .OrderBy(a => a.DistanceInMiles).ThenBy(a => a.DueDate).ThenByDescending(a => a.IsHealthCritical)
                            .Select(x => new OpenJobRequestDTO
                            {
                                Distance = x.DistanceInMiles,
                                DueDate = x.DueDate,
                                IsCritical = x.IsHealthCritical,
                                Postcode = x.PostCode,
                                SupportActivity = x.SupportActivity,
                                EncodedJobID = HelpMyStreet.Utils.Utils.Base64Utils.Base64Encode(x.JobID.ToString()),                                
                            }).ToList();

                if (jobSummaries.Count > 0)
                {
                    SendEmailToUserRequest request = new SendEmailToUserRequest
                    {
                        ToUserID = user.UserID,
                        Subject = $"Help needed in your area - {DateTime.Now.ToString("dd/MM/yy")}",
                        BodyHTML = EmailBuilder.BuildDailyDigestEmail(jobSummaries, _applicationConfig.Value.EmailBaseUrl),
                    };
                    await _communicationService.SendEmailToUserAsync(request, cancellationToken);
                }
            }
        }
    }
}
