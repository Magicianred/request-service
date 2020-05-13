using AutoMapper;
using HelpMyStreet.Contracts.ReportService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using Microsoft.EntityFrameworkCore;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Repo
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> GetRequestPostCodeAsync(int requestId, CancellationToken cancellationToken)
        {
            var request = await _context.Request.FirstAsync(x => x.Id == requestId, cancellationToken);
            if (request != null)
            {
                return request.PostCode;
            }
            return null;
        }

        public async Task<int> CreateRequestAsync(string postCode, CancellationToken cancellationToken)
        {            
            Request request = new Request
            {
                PostCode = postCode,
                DateRequested = DateTime.Now,
                IsFulfillable = false,
                CommunicationSent = false,
            };

           _context.Request.Add(request);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Id;
        }


        public async Task UpdateFulfillmentAsync(int requestId, Fulfillable fulfillable, CancellationToken cancellationToken)
        {
            var request = await _context.Request.FirstAsync(x => x.Id == requestId, cancellationToken);
            if (request != null)
            {
                request.FulfillableStatus = (byte)fulfillable;
                await _context.SaveChangesAsync(cancellationToken);
            }        
        }

        public async Task UpdateCommunicationSentAsync(int requestId, bool communicationSent, CancellationToken cancellationToken)
        {
            var request = await _context.Request.FirstAsync(x => x.Id == requestId, cancellationToken);
            if (request != null)
            {
                request.CommunicationSent = communicationSent;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdatePersonalDetailsAsync(PersonalDetailsDto dto, CancellationToken cancellationToken)
        {
            var personalDetails = new PersonalDetails
            {
                RequestId = dto.RequestID,
                FurtherDetails = dto.FurtherDetails,
                OnBehalfOfAnother = dto.OnBehalfOfAnother,
                RequestorEmailAddress = dto.RequestorEmailAddress,
                RequestorFirstName = dto.RequestorFirstName,
                RequestorLastName = dto.RequestorLastName,
                RequestorPhoneNumber = dto.RequestorPhoneNumber,
            };
            _context.PersonalDetails.Add(personalDetails);
            await _context.SaveChangesAsync(cancellationToken);               
        }

        public async Task AddSupportActivityAsync(SupportActivityDTO dto, CancellationToken cancellationToken)
        {
            List<SupportActivities> activties = new List<SupportActivities>();
            foreach(var activtity in dto.SupportActivities)
            {
                activties.Add(new SupportActivities
                {
                    RequestId = dto.RequestID,
                    ActivityId = (int)activtity
                });
            }

            _context.SupportActivities.AddRange(activties);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public List<ReportItem> GetDailyReport()
        {
            List<ReportItem> response = new List<ReportItem>();
            List<DailyReport> result = _context.DailyReport.ToList();

            if (result != null)
            {
                foreach (DailyReport dailyReport in result)
                {
                    response.Add(new ReportItem()
                    {
                        Section = dailyReport.Section,
                        Last2Hours = dailyReport.Last2Hours,
                        Today = dailyReport.Today,
                        SinceLaunch = dailyReport.SinceLaunch
                    });
                }
            }

            return response;
        }

        private Person GetPersonFromPersonalDetails(RequestPersonalDetails requestPersonalDetails)
        {
            return new Person()
            {
                FirstName = requestPersonalDetails.FirstName,
                LastName = requestPersonalDetails.LastName,
                EmailAddress = requestPersonalDetails.EmailAddress,
                AddressLine1 = requestPersonalDetails.Address.AddressLine1,
                AddressLine2 = requestPersonalDetails.Address.AddressLine2,
                AddressLine3 = requestPersonalDetails.Address.AddressLine3,
                Locality = requestPersonalDetails.Address.Locality,
                Postcode = requestPersonalDetails.Address.Postcode,
                MobilePhone = requestPersonalDetails.MobileNumber,
                OtherPhone = requestPersonalDetails.OtherNumber,
            };
        }

        public async Task<int> NewHelpRequestAsync(PostNewRequestForHelpRequest postNewRequestForHelpRequest, Fulfillable fulfillable)
        {
            Person requester = GetPersonFromPersonalDetails(postNewRequestForHelpRequest.HelpRequest.Requestor);
            Person recipient;

            if (postNewRequestForHelpRequest.HelpRequest.ForRequestor)
            {
                recipient = requester;
            }
            else
            {
                recipient = GetPersonFromPersonalDetails(postNewRequestForHelpRequest.HelpRequest.Recipient);
            }

            _context.Person.Add(requester);
            _context.Person.Add(recipient);

            Request request = new Request()
            {
                ReadPrivacyNotice = postNewRequestForHelpRequest.HelpRequest.ReadPrivacyNotice,
                SpecialCommunicationNeeds = postNewRequestForHelpRequest.HelpRequest.SpecialCommunicationNeeds,
                AcceptedTerms = postNewRequestForHelpRequest.HelpRequest.AcceptedTerms,
                OtherDetails = postNewRequestForHelpRequest.HelpRequest.OtherDetails,
                PostCode = postNewRequestForHelpRequest.HelpRequest.Recipient.Address.Postcode,
                ForRequestor = postNewRequestForHelpRequest.HelpRequest.ForRequestor,
                PersonIdRecipientNavigation = recipient,
                PersonIdRequesterNavigation = requester,
                FulfillableStatus = (byte) fulfillable,
                CreatedByUserId = postNewRequestForHelpRequest.HelpRequest.CreatedByUserId
            };

            _context.Request.Add(request);
            foreach(HelpMyStreet.Utils.Models.Job job in postNewRequestForHelpRequest.NewJobsRequest.Jobs)
            {
                EntityFramework.Entities.Job EFcoreJob = new EntityFramework.Entities.Job()
                {
                    NewRequest = request,
                    Details = job.Details,
                    IsHealthCritical = job.HealthCritical,
                    SupportActivityId = (byte)job.SupportActivity,
                    DueDate = DateTime.Now.AddDays(job.DueDays),
                    JobStatusId = (byte)HelpMyStreet.Utils.Enums.JobStatuses.Open
                };
                _context.Job.Add(EFcoreJob);
                _context.RequestJobStatus.Add(new RequestJobStatus()
                {
                    DateCreated = DateTime.Now,
                    JobStatusId = (byte) HelpMyStreet.Utils.Enums.JobStatuses.Open,
                    Job = EFcoreJob
                });
            }
            await _context.SaveChangesAsync();
            return request.Id;

        }

        private void AddJobStatus(int jobID, int? createdByUserID, int? volunteerUserID, HelpMyStreet.Utils.Enums.JobStatuses jobStatus)
        {
            _context.RequestJobStatus.Add(new RequestJobStatus()
            {
                CreatedByUserId = createdByUserID,
                VolunteerUserId = volunteerUserID,
                JobId = jobID,
                JobStatusId = (byte)jobStatus
            });
        }

        public async Task<bool> UpdateJobStatusOpenAsync(int jobID, int createdByUserID, CancellationToken cancellationToken)
        {
            bool response = false;
            var job = _context.Job.Where(w => w.Id == jobID).FirstOrDefault();
            if (job != null)
            {
                job.JobStatusId = (byte)HelpMyStreet.Utils.Enums.JobStatuses.Open;
                job.VolunteerUserId = null;
                AddJobStatus(jobID, createdByUserID, null, HelpMyStreet.Utils.Enums.JobStatuses.Open);
                int result = await _context.SaveChangesAsync(cancellationToken);
                if (result == 2)
                {
                    response = true;
                }
            }
            return response;
        }

        public async Task<bool> UpdateJobStatusInProgressAsync(int jobID, int createdByUserID, int volunteerUserID, CancellationToken cancellationToken)
        {
            bool response = false;
            var job = _context.Job.Where(w => w.Id == jobID).FirstOrDefault();
            if (job != null)
            {
                job.JobStatusId = (byte)HelpMyStreet.Utils.Enums.JobStatuses.InProgress;
                job.VolunteerUserId = volunteerUserID;
                AddJobStatus(jobID, createdByUserID, volunteerUserID, HelpMyStreet.Utils.Enums.JobStatuses.InProgress);
                int result = await _context.SaveChangesAsync(cancellationToken);
                if (result == 2)
                {
                    response = true;
                }
            }
            return response;
        }

        public async Task<bool> UpdateJobStatusDoneAsync(int jobID, int createdByUserID, CancellationToken cancellationToken)
        {
            bool response = false;
            var job = _context.Job.Where(w => w.Id == jobID).FirstOrDefault();
            if (job != null)
            {
                job.JobStatusId = (byte)HelpMyStreet.Utils.Enums.JobStatuses.Done;
                AddJobStatus(jobID, createdByUserID, null, HelpMyStreet.Utils.Enums.JobStatuses.InProgress);
                int result = await _context.SaveChangesAsync(cancellationToken);
                if (result == 2)
                {
                    response = true;
                }
            }
            return response;
        }

        public List<JobSummary> GetJobsAllocatedToUser(int volunteerUserID)
        {
            List<EntityFramework.Entities.Job> jobSummaries = _context.Job
                                    .Include(i => i.NewRequest)
                                    .Where(w => w.VolunteerUserId == volunteerUserID 
                                                && w.JobStatus.Value == HelpMyStreet.Utils.Enums.JobStatuses.InProgress.ToString()
                                            ).ToList();

            List<JobSummary> response = new List<JobSummary>();
            foreach (EntityFramework.Entities.Job j in jobSummaries)
            {
                response.Add(new JobSummary()
                {
                    IsHealthCritical = j.IsHealthCritical,
                    DueDate = j.DueDate,
                    Details = j.Details,
                    JobID = j.Id,
                    VolunteerUserID = j.VolunteerUserId,
                    JobStatus = (HelpMyStreet.Utils.Enums.JobStatuses) j.JobStatusId,
                    SupportActivity = (HelpMyStreet.Utils.Enums.SupportActivities) j.SupportActivityId,
                    PostCode = j.NewRequest.PostCode
                });
            }
            return response;
            
        }
    }
}
