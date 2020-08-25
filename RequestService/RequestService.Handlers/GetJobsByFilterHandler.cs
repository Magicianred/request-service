using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System.Collections.Generic;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
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
            if (!string.IsNullOrEmpty(request.Postcode))
            {
                request.Postcode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.Postcode);

                try
                {
                    var postcodeValid = await _addressService.IsValidPostcode(request.Postcode, cancellationToken);
                }
                catch (HttpRequestException)
                {
                    throw new PostCodeException();
                }
            }
        
            GetJobsByFilterResponse result = new GetJobsByFilterResponse() { JobSummaries = new List<JobSummary>() };
            List<JobSummary> jobSummaries = _repository.GetJobSummaries();

            if (jobSummaries.Count == 0)
                return result;

            jobSummaries = await _jobFilteringService.FilterJobSummaries(jobSummaries, 
                request.UserID,
                request.SupportActivities?.SupportActivities, 
                request.Postcode, 
                request.DistanceInMiles, 
                request.ActivitySpecificSupportDistancesInMiles, 
                request.ReferringGroupID,
                request.Groups?.Groups,
                request.JobStatuses?.JobStatuses, 
                cancellationToken);

            result = new GetJobsByFilterResponse()
            {
                JobSummaries = jobSummaries
            };
            return result;
        }

    }
}
