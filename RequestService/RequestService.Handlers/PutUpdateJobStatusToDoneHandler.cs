using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Enums;
using System.Collections.Generic;

namespace RequestService.Handlers
{
    public class PutUpdateJobStatusToDoneHandler : IRequestHandler<PutUpdateJobStatusToDoneRequest, PutUpdateJobStatusToDoneResponse>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        private readonly IJobService _jobService;
        public PutUpdateJobStatusToDoneHandler(IRepository repository, ICommunicationService communicationService, IJobService jobService)
        {
            _repository = repository;
            _communicationService = communicationService;
            _jobService = jobService;
        }

        public async Task<PutUpdateJobStatusToDoneResponse> Handle(PutUpdateJobStatusToDoneRequest request, CancellationToken cancellationToken)
        {
            PutUpdateJobStatusToDoneResponse response = new PutUpdateJobStatusToDoneResponse()
            {
                Outcome = UpdateJobStatusOutcome.Unauthorized
            };

            if (_repository.JobHasStatus(request.JobID, JobStatuses.Done))
            {
                response.Outcome = UpdateJobStatusOutcome.AlreadyInThisStatus;
            }
            else
            {
                bool hasPermission = await _jobService.HasPermissionToChangeStatusAsync(request.JobID, request.CreatedByUserID, cancellationToken);

                if (hasPermission)
                {
                    var result = await _repository.UpdateJobStatusDoneAsync(request.JobID, request.CreatedByUserID, cancellationToken);
                    response.Outcome = result;

                    if (result == UpdateJobStatusOutcome.Success)
                    {
                        response.Outcome = UpdateJobStatusOutcome.Success;
                        await _communicationService.RequestCommunication(
                        new RequestCommunicationRequest()
                        {
                            CommunicationJob = new CommunicationJob() { CommunicationJobType = CommunicationJobTypes.SendTaskStateChangeUpdate },
                            JobID = request.JobID,
                            AdditionalParameters = new Dictionary<string, string>()
                            {
                                { "FieldUpdated","Status" }
                            }
                        },
                        cancellationToken);
                    }
                }
            }
            return response;
        }
    }
}
