using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;

namespace RequestService.Handlers
{
    public class GetJobsAllocatedToUserHandler : IRequestHandler<GetJobsAllocatedToUserRequest, GetJobsAllocatedToUserResponse>
    {
        private readonly IRepository _repository;
        private readonly IUserService _userService;
        private readonly IJobService _jobService;
        public GetJobsAllocatedToUserHandler(
            IRepository repository,
            IUserService userService,
            IJobService jobService)
        {
            _repository = repository;
            _userService = userService;
            _jobService = jobService;
        }

        public async Task<GetJobsAllocatedToUserResponse> Handle(GetJobsAllocatedToUserRequest request, CancellationToken cancellationToken)
        {
            GetJobsAllocatedToUserResponse result = new GetJobsAllocatedToUserResponse();
            List<JobSummary> jobSummaries = _repository.GetJobsAllocatedToUser(request.VolunteerUserID);

            GetUserByIDResponse userByIDResponse = await _userService.GetUser(request.VolunteerUserID, cancellationToken);
            if (userByIDResponse == null || userByIDResponse.User == null)
            {
                return result;
            }
            string volunteerPostCode = userByIDResponse.User.PostalCode;
            jobSummaries = await _jobService.AttachedDistanceToJobSummaries(volunteerPostCode, jobSummaries, cancellationToken);

            if (jobSummaries.Count == 0)
            {
                return result;
            }

            result = new GetJobsAllocatedToUserResponse()
            {
                JobSummaries = jobSummaries
            };
            return result;
        }
    }
}
