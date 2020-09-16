using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using HelpMyStreet.Utils.Utils;
using RequestService.Core.Dto;
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
        private readonly IGroupService _groupService;

        public JobService(
            IDistanceCalculator distanceCalculator,
            IRepository repository,
            IGroupService groupService)
        {
            _repository = repository;
            _distanceCalculator = distanceCalculator;
            _groupService = groupService;
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
                    jobSummary.DistanceInMiles = _distanceCalculator.GetDistanceInMiles(volunteerPostcodeCoordinates.Latitude, volunteerPostcodeCoordinates.Longitude, jobPostcodeCoordinates.Latitude, jobPostcodeCoordinates.Longitude);
                }
            }
            return jobSummaries;
        }

        public async Task<List<JobHeader>> AttachedDistanceToJobHeaders(string volunteerPostCode, List<JobHeader> jobHeaders, CancellationToken cancellationToken)
        {
            if (jobHeaders.Count == 0)
            {
                return null;
            }
              
            volunteerPostCode = PostcodeFormatter.FormatPostcode(volunteerPostCode);

            List<string> distinctPostCodes = jobHeaders.Select(d => d.PostCode).Distinct().Select(x => PostcodeFormatter.FormatPostcode(x)).ToList();

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

            foreach (JobHeader jobHeader in jobHeaders)
            {
                var jobPostcodeCoordinates = postcodeCoordinatesResponse.Where(w => w.Postcode == jobHeader.PostCode).FirstOrDefault();
                if (jobPostcodeCoordinates != null)
                {
                    jobHeader.DistanceInMiles = _distanceCalculator.GetDistanceInMiles(volunteerPostcodeCoordinates.Latitude, volunteerPostcodeCoordinates.Longitude, jobPostcodeCoordinates.Latitude, jobPostcodeCoordinates.Longitude);
                }
            }
            return jobHeaders;
        }

        public async Task<bool> HasPermissionToChangeStatusAsync(int jobID, int createdByUserID, CancellationToken cancellationToken)
        {
            var jobDetails = _repository.GetJobDetails(jobID);

            if (jobDetails == null)
            {
                throw new Exception($"Unable to retrieve job details for jobID:{jobID}");
            }

            if (createdByUserID == jobDetails.JobSummary.VolunteerUserID)
            {
                return true;
            }

            int? referringGroupId = await _repository.GetReferringGroupIDForJobAsync(jobID, cancellationToken);

            if (!referringGroupId.HasValue)
            {
                throw new Exception($"Unable to retrieve referring groupId for jobID:{jobID}");
            }

            var userRoles = await _groupService.GetUserRoles(createdByUserID, cancellationToken);

            if (userRoles.UserGroupRoles[referringGroupId.Value].Contains((int)GroupRoles.TaskAdmin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
