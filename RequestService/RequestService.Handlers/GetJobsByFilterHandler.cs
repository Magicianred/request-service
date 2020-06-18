using MediatR;
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
        private readonly IAddressService _addressService;
        private readonly IJobFilteringService _jobFilteringService;
        public GetJobsByFilterHandler(
            IRepository repository,
            IAddressService addressService,
            IJobFilteringService jobFilteringService)
        {
            _repository = repository;
            _addressService = addressService;
            _jobFilteringService = jobFilteringService;
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

            jobSummaries = await _jobFilteringService.FilterJobSummaries(jobSummaries, request.SupportActivities, request.Postcode, request.DistanceInMiles, request.ActivitySpecificSupportDistancesInMiles, cancellationToken);

            result = new GetJobsByFilterResponse()
            {
                JobSummaries = jobSummaries
            };
            return result;
        }

    }
}
