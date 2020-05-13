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
using System.Security.Cryptography.X509Certificates;
using UserService.Core.Utils;

namespace RequestService.Handlers
{
    public class GetJobsAllocatedToUserHandler : IRequestHandler<GetJobsAllocatedToUserRequest, GetJobsAllocatedToUserResponse>
    {
        private readonly IRepository _repository;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public GetJobsAllocatedToUserHandler(IRepository repository,
            IUserService userService, 
            IAddressService addressService, 
            IDistanceCalculator distanceCalculator,
            IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _userService = userService;
            _addressService = addressService;
            _distanceCalculator = distanceCalculator;
            _applicationConfig = applicationConfig;
        }

        public async Task<GetJobsAllocatedToUserResponse> Handle(GetJobsAllocatedToUserRequest request, CancellationToken cancellationToken)
        {
            List<JobSummary> jobSummaries = _repository.GetJobsAllocatedToUser(request.VolunteerUserID);

            if(jobSummaries.Count>0)
            {
                //Calculate the distance
                GetUserByIDResponse userByIDResponse = await _userService.GetUser(request.VolunteerUserID, cancellationToken);
                if(userByIDResponse != null && userByIDResponse.User!=null)
                {
                    string postCode = userByIDResponse.User.PostalCode;

                    List<string> distinctPostCodes = jobSummaries.Select(d => d.PostCode).Distinct().ToList();
                    if(!distinctPostCodes.Contains(postCode))
                    {
                        distinctPostCodes.Add(postCode);
                    }

                    var postcodeCoordinatesResponse = await _addressService.GetPostcodeCoordinatesAsync(distinctPostCodes, cancellationToken);
                    if(postcodeCoordinatesResponse != null)
                    {
                        var volunteerPostcodeCoordinates = postcodeCoordinatesResponse.PostcodeCoordinates.Where(w => w.Postcode == postCode).FirstOrDefault();

                        if (volunteerPostcodeCoordinates != null)
                        {
                            foreach (JobSummary jobSummary in jobSummaries)
                            {
                                var jobPostcodeCoordinates = postcodeCoordinatesResponse.PostcodeCoordinates.Where(w => w.Postcode == jobSummary.PostCode).FirstOrDefault();
                                if (jobPostcodeCoordinates != null)
                                {
                                    jobSummary.DistanceInMiles = _distanceCalculator.GetDistanceInMiles(volunteerPostcodeCoordinates.Longitude, volunteerPostcodeCoordinates.Latitude, jobPostcodeCoordinates.Longitude,jobPostcodeCoordinates.Latitude);
                                }
                            }
                        }
                    }
                }
            }

            GetJobsAllocatedToUserResponse result = new GetJobsAllocatedToUserResponse()
            {
                JobSummaries = jobSummaries
            };
            return result;
        }
    }
}
