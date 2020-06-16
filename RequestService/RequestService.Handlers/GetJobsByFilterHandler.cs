﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using UserService.Core.Utils;
using RequestService.Core.Exceptions;
using System.Net.Http;

namespace RequestService.Handlers
{
    public class GetJobsByFilterHandler : IRequestHandler<GetJobsByFilterRequest, GetJobsByFilterResponse>
    {
        private readonly IRepository _repository;
        private readonly IJobService _jobService;
        private readonly IAddressService _addressService;
        public GetJobsByFilterHandler(
            IRepository repository,
            IJobService jobService,
            IAddressService addressService)
        {
            _repository = repository;
            _jobService = jobService;
            _addressService = addressService;
        }

        public async Task<GetJobsByFilterResponse> Handle(GetJobsByFilterRequest request, CancellationToken cancellationToken)
        {
            string postcode = request.Postcode;
            request.Postcode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.Postcode);

            try
            {
                var postcodeValid = await _addressService.IsValidPostcode(request.Postcode, cancellationToken);
            }
            catch(HttpRequestException)
            {
                throw new PostCodeException();
            }
        

            GetJobsByFilterResponse result = new GetJobsByFilterResponse() { JobSummaries = new List<JobSummary>() };
            List<JobSummary> jobSummaries = _repository.GetOpenJobsSummaries();

            if (jobSummaries.Count == 0)
                return result;

            jobSummaries = await _jobService.AttachedDistanceToJobSummaries(request.Postcode, jobSummaries, cancellationToken);

            if(jobSummaries.Count==0)
            {
                return result;
            }

            result = new GetJobsByFilterResponse()
            {
                JobSummaries = jobSummaries
                                    .Where(w => request.SupportActivities == null || request.SupportActivities.Contains(w.SupportActivity))
                                    .Where(w => w.DistanceInMiles <= GetSupportDistanceForActivity(w.SupportActivity, request))
                                    .OrderBy(a => a.DistanceInMiles).ThenBy(a => a.DueDate).ThenByDescending(a => a.IsHealthCritical)
                                    .ToList()
            };
            return result;
        }

        private double GetSupportDistanceForActivity(SupportActivities supportActivity, GetJobsByFilterRequest request)
        {
            if (request.ActivitySpecificSupportDistancesInMiles != null && request.ActivitySpecificSupportDistancesInMiles.ContainsKey(supportActivity))
            {
                return request.ActivitySpecificSupportDistancesInMiles[supportActivity] ?? int.MaxValue;
            }
            else
            {
                return request.DistanceInMiles ?? int.MaxValue;
            }
        }
    }
}
