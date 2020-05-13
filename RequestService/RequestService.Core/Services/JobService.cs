using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Utils;

namespace RequestService.Core.Services
{
    public class JobService : IJobService
    {
        private readonly IAddressService _addressService;
        private readonly IDistanceCalculator _distanceCalculator;
        public JobService(
            IAddressService addressService,
            IDistanceCalculator distanceCalculator)
        {
            _addressService = addressService;
            _distanceCalculator = distanceCalculator;
        }

        public async Task GetJobSummaries(string postCode, List<JobSummary> jobSummaries, CancellationToken cancellationToken)
        {
            if (jobSummaries.Count == 0)
            {
                return;
            }

            List<string> distinctPostCodes = jobSummaries.Select(d => d.PostCode).Distinct().ToList();

            if (!distinctPostCodes.Contains(postCode))
            {
                distinctPostCodes.Add(postCode);
            }

            var postcodeCoordinatesResponse = await _addressService.GetPostcodeCoordinatesAsync(distinctPostCodes, cancellationToken);

            if (postcodeCoordinatesResponse == null)
            {
                return;
            }

            var volunteerPostcodeCoordinates = postcodeCoordinatesResponse.PostcodeCoordinates.Where(w => w.Postcode == postCode).FirstOrDefault();
            if (volunteerPostcodeCoordinates == null)
            {
                return;
            }

            foreach (JobSummary jobSummary in jobSummaries)
            {
                var jobPostcodeCoordinates = postcodeCoordinatesResponse.PostcodeCoordinates.Where(w => w.Postcode == jobSummary.PostCode).FirstOrDefault();
                if (jobPostcodeCoordinates != null)
                {
                    jobSummary.DistanceInMiles = _distanceCalculator.GetDistanceInMiles(volunteerPostcodeCoordinates.Longitude, volunteerPostcodeCoordinates.Latitude, jobPostcodeCoordinates.Longitude, jobPostcodeCoordinates.Latitude);
                }
            }
        }
    }
}
