using HelpMyStreet.Utils.Models;
using HelpMyStreet.Utils.Utils;
using RequestService.Core.Interfaces.Repositories;
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
        private readonly IRepository _repository;
   
        private readonly IDistanceCalculator _distanceCalculator;
        public JobService(            
            IDistanceCalculator distanceCalculator,
            IRepository repository)
        {
            _repository = repository;
            _distanceCalculator = distanceCalculator;
        }

        public async Task<List<JobSummary>> AttachedDistanceToJobSummaries(string volunteerPostCode, List<JobSummary> jobSummaries, CancellationToken cancellationToken)
        {
            if (jobSummaries.Count == 0)
            {
                return null;
            }
            
            volunteerPostCode = PostcodeFormatter.FormatPostcode(volunteerPostCode);

            List<string> distinctPostCodes = jobSummaries.Select(d => d.PostCode).Distinct().Select(x => PostcodeFormatter.FormatPostcode(x)).ToList();

            if (!distinctPostCodes.Contains(volunteerPostCode))
            {
                distinctPostCodes.Add(volunteerPostCode);
            }

            var postcodeCoordinatesResponse = await _repository.GetLatitudeAndLongitudes(distinctPostCodes, cancellationToken);

            if (postcodeCoordinatesResponse == null)
            {
                return null;
            }

            var volunteerPostcodeCoordinates = postcodeCoordinatesResponse.Where(w => w.Postcode == volunteerPostCode).FirstOrDefault();
            if (volunteerPostcodeCoordinates == null)
            {
                return null;
            }

            foreach (JobSummary jobSummary in jobSummaries)
            {
                var jobPostcodeCoordinates = postcodeCoordinatesResponse.Where(w => w.Postcode == jobSummary.PostCode).FirstOrDefault();
                if (jobPostcodeCoordinates != null)
                {
                    jobSummary.DistanceInMiles = _distanceCalculator.GetDistanceInMiles(volunteerPostcodeCoordinates.Longitude, volunteerPostcodeCoordinates.Latitude, jobPostcodeCoordinates.Longitude, jobPostcodeCoordinates.Latitude);
                }
            }
            return jobSummaries;
        }
    }
}
