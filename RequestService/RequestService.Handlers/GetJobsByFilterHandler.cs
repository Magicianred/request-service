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

namespace RequestService.Handlers
{
    public class GetJobsByFilterHandler : IRequestHandler<GetJobsByFilterRequest, GetJobsByFilterResponse>
    {
        private readonly IRepository _repository;
        private readonly IAddressService _addressService;
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public GetJobsByFilterHandler(
            IRepository repository,
            IAddressService addressService,
            IDistanceCalculator distanceCalculator,
            IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _addressService = addressService;
            _distanceCalculator = distanceCalculator;
            _applicationConfig = applicationConfig;
        }

        public async Task<GetJobsByFilterResponse> Handle(GetJobsByFilterRequest request, CancellationToken cancellationToken)
        {
            GetJobsByFilterResponse result = new GetJobsByFilterResponse();
            List<JobSummary> jobSummaries = _repository.GetOpenJobsSummaries();
            List<string> distinctPostCodes = null;

            if (jobSummaries.Count == 0)
            {
                return result;
            }

            distinctPostCodes = jobSummaries.Select(d => d.PostCode).Distinct().ToList();
            if (!distinctPostCodes.Contains(request.Postcode))
            {
                distinctPostCodes.Add(request.Postcode);
            }
            
            var postcodeCoordinatesResponse = await _addressService.GetPostcodeCoordinatesAsync(distinctPostCodes, cancellationToken);

            if (postcodeCoordinatesResponse == null)
            {
                return result;
            }
            
            var volunteerPostcodeCoordinates = postcodeCoordinatesResponse.PostcodeCoordinates.Where(w => w.Postcode == request.Postcode).FirstOrDefault();
            if (volunteerPostcodeCoordinates == null)
            {
                return result;
            }

            foreach (JobSummary jobSummary in jobSummaries)
            {
                var jobPostcodeCoordinates = postcodeCoordinatesResponse.PostcodeCoordinates.Where(w => w.Postcode == jobSummary.PostCode).FirstOrDefault();
                if (jobPostcodeCoordinates != null)
                {
                    jobSummary.DistanceInMiles = _distanceCalculator.GetDistanceInMiles(volunteerPostcodeCoordinates.Longitude, volunteerPostcodeCoordinates.Latitude, jobPostcodeCoordinates.Longitude, jobPostcodeCoordinates.Latitude);
                }
            }
            result = new GetJobsByFilterResponse()
            {
                JobSummaries = jobSummaries.Where(w => w.DistanceInMiles<=request.DistanceInMiles).ToList()
            };
            return result;
        }
    }
}
