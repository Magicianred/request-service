using AutoMapper;
using HelpMyStreet.Contracts.ReportService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SupportActivities = RequestService.Repo.EntityFramework.Entities.SupportActivities;

namespace RequestService.Repo
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
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

            if (postNewRequestForHelpRequest.HelpRequest.RequestorType != RequestorType.OnBehalf)
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
                OrganisationName = postNewRequestForHelpRequest.HelpRequest.OrganisationName,
                PostCode = postNewRequestForHelpRequest.HelpRequest.Recipient.Address.Postcode,
                ForRequestor = postNewRequestForHelpRequest.HelpRequest.ForRequestor,
                PersonIdRecipientNavigation = recipient,
                PersonIdRequesterNavigation = requester,
                RequestorType = (byte) postNewRequestForHelpRequest.HelpRequest.RequestorType,
                FulfillableStatus = (byte) fulfillable,
                CreatedByUserId = postNewRequestForHelpRequest.HelpRequest.CreatedByUserId
            };

            foreach (HelpMyStreet.Utils.Models.Job job in postNewRequestForHelpRequest.NewJobsRequest.Jobs)
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

                foreach (var question in job.Questions)
                {
                    _context.JobQuestions.Add(new JobQuestions
                    {
                        Job = EFcoreJob,
                        QuestionId = question.Id,
                        Answer = question.Answer
                    });
                }

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

         

        private void AddJobStatus(int jobID, int? createdByUserID, int? volunteerUserID, byte jobStatus)
        {
            _context.RequestJobStatus.Add(new RequestJobStatus()
            {
                CreatedByUserId = createdByUserID,
                VolunteerUserId = volunteerUserID,
                JobId = jobID,
                JobStatusId = jobStatus
            });
        }

        public async Task<List<ActivityQuestionDTO>> GetActivityQuestions(List<HelpMyStreet.Utils.Enums.SupportActivities> activity,  CancellationToken cancellationToken)
        {
            return await _context.ActivityQuestions.Where(x => activity.Any(a => (int)a == x.ActivityId)).GroupBy(x => x.ActivityId).Select(g => new ActivityQuestionDTO
            {
                Activity = (HelpMyStreet.Utils.Enums.SupportActivities)g.Key,
                Questions = g.Select(x => new HelpMyStreet.Utils.Models.Question
                {
                    Id = x.Question.Id,
                    Name = x.Question.Name,
                    Required = x.Question.Required,
                    Type = (QuestionType)x.Question.QuestionType,
                    AddtitonalData = x.Question.AdditionalData != null ? JsonConvert.DeserializeObject<List<AdditonalQuestionData>>(x.Question.AdditionalData) : new List<AdditonalQuestionData>()
                }).ToList()
            }).ToListAsync(cancellationToken);
            
        }

        public async Task<bool> UpdateJobStatusOpenAsync(int jobID, int createdByUserID, CancellationToken cancellationToken)
        {
            bool response = false;
            byte openJobStatus = (byte) HelpMyStreet.Utils.Enums.JobStatuses.Open;
            var job = _context.Job.Where(w => w.Id == jobID).FirstOrDefault();
            if (job != null)
            {
                if (job.JobStatusId != openJobStatus)
                {
                    job.JobStatusId = openJobStatus;
                    job.VolunteerUserId = null;
                    AddJobStatus(jobID, createdByUserID, null, openJobStatus);
                    int result = await _context.SaveChangesAsync(cancellationToken);
                    if (result == 2)
                    {
                        response = true;
                    }
                }
            }
            return response;
        }

        public async Task<bool> UpdateJobStatusInProgressAsync(int jobID, int createdByUserID, int volunteerUserID, CancellationToken cancellationToken)
        {
            bool response = false;
            byte inProgressJobStatus = (byte)HelpMyStreet.Utils.Enums.JobStatuses.InProgress;
            var job = _context.Job.Where(w => w.Id == jobID).FirstOrDefault();
            if (job != null)
            {
                if (job.JobStatusId != inProgressJobStatus)
                {
                    job.JobStatusId = inProgressJobStatus;
                    job.VolunteerUserId = volunteerUserID;
                    AddJobStatus(jobID, createdByUserID, volunteerUserID, inProgressJobStatus);
                    int result = await _context.SaveChangesAsync(cancellationToken);
                    if (result == 2)
                    {
                        response = true;
                    }
                }
            }
            return response;
        }

        public async Task<bool> UpdateJobStatusDoneAsync(int jobID, int createdByUserID, CancellationToken cancellationToken)
        {
            bool response = false;
            byte doneJobStatus = (byte)HelpMyStreet.Utils.Enums.JobStatuses.Done;
            var job = _context.Job.Where(w => w.Id == jobID).FirstOrDefault();
            if (job != null)
            {
                if (job.JobStatusId != doneJobStatus)
                {
                    job.JobStatusId = doneJobStatus;
                    AddJobStatus(jobID, createdByUserID, null, doneJobStatus);
                    int result = await _context.SaveChangesAsync(cancellationToken);
                    if (result == 2)
                    {
                        response = true;
                    }
                }
            }
            return response;
        }

        public List<JobSummary> GetJobsAllocatedToUser(int volunteerUserID)
        {
            byte jobStatusID_InProgress = (byte)HelpMyStreet.Utils.Enums.JobStatuses.InProgress;

            List<EntityFramework.Entities.Job> jobSummaries = _context.Job
                                    .Include(i => i.NewRequest)
                                    .Include(i => i.JobQuestions)
                                    .ThenInclude(rq => rq.Question)
                                    .Where(w => w.VolunteerUserId == volunteerUserID 
                                                && w.JobStatusId == jobStatusID_InProgress
                                            ).ToList();

            return GetJobSummaries(jobSummaries);
            
        }

        public List<JobSummary> GetOpenJobsSummaries()
        {
            
            byte jobStatusID_Open = (byte)HelpMyStreet.Utils.Enums.JobStatuses.Open;

            List<EntityFramework.Entities.Job> jobSummaries = _context.Job
                                    .Include(i => i.NewRequest)
                                    .Include(i => i.JobQuestions)
                                    .ThenInclude(rq => rq.Question)
                                    .Where(w => w.JobStatusId == jobStatusID_Open
                                            ).ToList();
            return GetJobSummaries(jobSummaries);
            
        }

        public List<JobSummary> GetJobSummaries(List<EntityFramework.Entities.Job> jobs)
        {
            List<JobSummary> response = new List<JobSummary>();
            foreach (EntityFramework.Entities.Job j in jobs)
            {
                response.Add(new JobSummary()
                {
                    IsHealthCritical = j.IsHealthCritical,
                    DueDate = j.DueDate,
                    Details = j.Details,
                    JobID = j.Id,
                    VolunteerUserID = j.VolunteerUserId,
                    JobStatus = (HelpMyStreet.Utils.Enums.JobStatuses)j.JobStatusId,
                    SupportActivity = (HelpMyStreet.Utils.Enums.SupportActivities)j.SupportActivityId,
                    PostCode = j.NewRequest.PostCode,
                    OtherDetails = j.NewRequest.OtherDetails,                    
                    SpecialCommunicationNeeds = j.NewRequest.SpecialCommunicationNeeds,
                    Questions = MapToQuestions(j.JobQuestions)                    
                });
            }
            return response;
        }

        private List<HelpMyStreet.Utils.Models.Question> MapToQuestions(ICollection<JobQuestions> questions)
        {
            return questions.Select(x => new HelpMyStreet.Utils.Models.Question
            {
                Id = x.QuestionId,
                Answer = x.Answer,
                Name = x.Question.Name,
                Required = x.Question.Required,
                Type = (QuestionType)x.Question.QuestionType,
                AddtitonalData = JsonConvert.DeserializeObject<List<AdditonalQuestionData>>(x.Question.AdditionalData),
            }).ToList();
        }

        private RequestPersonalDetails GetPerson(Person person)
        {
            return new RequestPersonalDetails()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                EmailAddress = person.EmailAddress,
                MobileNumber = person.MobilePhone,
                OtherNumber = person.OtherPhone,
                Address = new Address()
                {
                    AddressLine1 = person.AddressLine1,
                    AddressLine2 = person.AddressLine2,
                    AddressLine3 = person.AddressLine3,
                    Locality = person.Locality,
                    Postcode = person.Postcode
                }
            };
        }

        public GetJobDetailsResponse GetJobDetails(int jobID)
        {
            GetJobDetailsResponse response = new GetJobDetailsResponse();
            var efJob = _context.Job
                        .Include(i => i.NewRequest)
                        .ThenInclude(i => i.PersonIdRecipientNavigation)
                        .Include(i => i.NewRequest)
                        .ThenInclude(i=> i.PersonIdRequesterNavigation)
                        .Where(w => w.Id == jobID).FirstOrDefault();

            if(efJob == null)
            {
                return response;
            }
            
            response = new GetJobDetailsResponse()
            {
                PostCode = efJob.NewRequest.PostCode,
                OtherDetails = efJob.NewRequest.OtherDetails,
                SpecialCommunicationNeeds = efJob.NewRequest.SpecialCommunicationNeeds,
                Recipient = GetPerson(efJob.NewRequest.PersonIdRecipientNavigation),
                Requestor = GetPerson(efJob.NewRequest.PersonIdRequesterNavigation),
                Details = efJob.Details,
                HealthCritical = efJob.IsHealthCritical,
                JobID = efJob.Id,
                VolunteerUserID = efJob.VolunteerUserId,
                JobStatus = (HelpMyStreet.Utils.Enums.JobStatuses)efJob.JobStatusId,
                SupportActivity = (HelpMyStreet.Utils.Enums.SupportActivities)efJob.SupportActivityId,
                DueDate= efJob.DueDate,
                ForRequestor = efJob.NewRequest.ForRequestor.Value,
                DateRequested = efJob.NewRequest.DateRequested,
                RequestorType = (RequestorType)efJob.NewRequest.RequestorType,
            };

            return response;
        }

        public Task<List<LatitudeAndLongitudeDTO>> GetLatitudeAndLongitudes(List<string> postCodes, CancellationToken cancellationToken)
        {
            var postcodeDetails = (from a in _context.Postcode
                                   where postCodes.Any(p => p == a.Postcode)
                                   select new LatitudeAndLongitudeDTO
                                   {
                                       Postcode = a.Postcode,
                                       Latitude = Convert.ToDouble(a.Latitude),
                                       Longitude = Convert.ToDouble(a.Longitude)
                                   }).ToListAsync(cancellationToken);

            return postcodeDetails;
        }
    }
}
