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
        

            GetJobsByFilterResponse result = new GetJobsByFilterResponse();
            List<JobSummary> jobSummaries = _repository.GetOpenJobsSummaries();

            jobSummaries = await _jobService.AttachedDistanceToJobSummaries(request.Postcode, jobSummaries, cancellationToken);

            if(jobSummaries.Count==0)
            {
                return result;
            }

            result = new GetJobsByFilterResponse()
            {
                JobSummaries = jobSummaries.Where(w => w.DistanceInMiles<=request.DistanceInMiles).ToList()
            };
            return result;
        }
    }
}
