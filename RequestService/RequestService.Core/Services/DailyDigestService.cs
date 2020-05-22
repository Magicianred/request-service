using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using MediatR;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public class DailyDigestService : IDailyDigestService
    {
        private readonly IUserService _userService;
        private readonly ICommunicationService _communicationService;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        private readonly IRepository _repository;
        private readonly IJobService _jobService;
        private readonly IAddressService _addressService;
        private List<JobSummary> _currentOpenJobs;

        public DailyDigestService(
            IUserService userService, 
            ICommunicationService communicationService,
            IOptionsSnapshot<ApplicationConfig> applicationConfig,
            IRepository repository,
            IJobService jobService,
            IAddressService addressService)
        {
            _userService = userService;
            _communicationService = communicationService;
            _applicationConfig = applicationConfig;
            _repository = repository;
            _jobService = jobService;
            _addressService = addressService;
            _currentOpenJobs = _repository.GetOpenJobsSummaries();
        }

        public async Task GenerateEmailsAsync()
        {
            try
            {
                if(_currentOpenJobs == null || _currentOpenJobs.Count==0)
                {
                    return; 
                }

                CancellationToken cancellationToken = CancellationToken.None;
                var users = await _userService.GetUsersAsync(cancellationToken);

                if (users == null || users.UserDetails == null)
                {
                    return;
                }

                foreach (HelpMyStreet.Contracts.UserService.Response.UserDetails ud in users.UserDetails.Where(w => w.PostCode != null && w.PostCode.Length > 0))
                {
                    List<JobSummary> openJobs = GetOpenJobsForPostCode(ud.PostCode);
                    
                    if(openJobs.Count>0)
                    {
                        List<OpenJobRequestDTO> emailJobs = GetOpenJobsRequestDTOFromSummaries(openJobs);

                        SendEmailRequest emailRequest = new SendEmailRequest()
                        {
                            Subject = $"Help needed in your area - {DateTime.Now:mm/DD/yyyy}",
                            ToAddress = ud.EmailAddress,
                            ToName = $"{ud.FirstName} {ud.LastName}",
                            BodyHTML = EmailBuilder.BuildDailyDigestEmail(emailJobs)
                        };
                        await _communicationService.SendEmail(emailRequest, cancellationToken);

                    }
                }
            }
            catch(Exception exc)
            {
                string s = exc.ToString();
                int i = 1;
            }
        }

        private List<JobSummary> GetOpenJobsForPostCode(string postCode)
        {
            JobSummary[] jobs = new JobSummary[_currentOpenJobs.Count];
            _currentOpenJobs.CopyTo(jobs);
            List<JobSummary> openJobs = jobs.ToList();
            openJobs = _jobService.AttachedDistanceToJobSummaries(postCode, openJobs, CancellationToken.None).Result;

            if (openJobs.Count > 0)
            {
                openJobs = openJobs
                            .Where(w => w.DistanceInMiles <= _applicationConfig.Value.DistanceInMilesForDailyDigest)
                            .OrderBy(a => a.DistanceInMiles).ThenBy(a => a.DueDate).ThenByDescending(a => a.IsHealthCritical)
                            .ToList();
            }
            return openJobs;
        }

        private List<OpenJobRequestDTO> GetOpenJobsRequestDTOFromSummaries(List<JobSummary> jobSummaries)
        {
            return jobSummaries.Select(s => new OpenJobRequestDTO
            {
                DistanceInMiles = s.DistanceInMiles,
                DueDate = s.DueDate,
                IsCritical = s.IsHealthCritical,
                Postcode = s.PostCode,
                SupportActivity = s.SupportActivity
            }).ToList();
        }
    }
}
