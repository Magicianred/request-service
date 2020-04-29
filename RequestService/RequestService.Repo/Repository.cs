using AutoMapper;
using HelpMyStreet.Contracts.ReportService.Response;
using HelpMyStreet.Contracts.RequestService.Response;
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
    }
}
