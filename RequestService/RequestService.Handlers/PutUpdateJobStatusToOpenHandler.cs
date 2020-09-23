using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;

namespace RequestService.Handlers
{
    public class PutUpdateJobStatusToOpenHandler : IRequestHandler<PutUpdateJobStatusToOpenRequest, PutUpdateJobStatusToOpenResponse>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        private readonly IJobService _jobService;
        public PutUpdateJobStatusToOpenHandler(IRepository repository, ICommunicationService communicationService, IJobService jobService)
        {
            _repository = repository;
            _communicationService = communicationService;
            _jobService = jobService;
        }

        public async Task<PutUpdateJobStatusToOpenResponse> Handle(PutUpdateJobStatusToOpenRequest request, CancellationToken cancellationToken)
        {
            PutUpdateJobStatusToOpenResponse response = new PutUpdateJobStatusToOpenResponse()
            {
                Outcome = UpdateJobStatusOutcome.Unauthorized
            };
            if (_repository.JobHasSameStatusAsProposedStatus(request.JobID, JobStatuses.Open))
            {
                response.Outcome = UpdateJobStatusOutcome.AlreadyInThisStatus;
            }
            else
            {

                bool hasPermission = await _jobService.HasPermissionToChangeStatusAsync(request.JobID, request.CreatedByUserID, cancellationToken);

                if (hasPermission)
                {
                    var result = await _repository.UpdateJobStatusOpenAsync(request.JobID, request.CreatedByUserID, cancellationToken);
                    response.Outcome = result;

                    if (result == UpdateJobStatusOutcome.Success)
                    {
                        await _communicationService.RequestCommunication(
                        new RequestCommunicationRequest()
                        {
                            CommunicationJob = new CommunicationJob() { CommunicationJobType = CommunicationJobTypes.SendTaskStateChangeUpdate },
                            JobID = request.JobID
                        },
                        cancellationToken);
                    }
                }
            }
            return response;
        }
    }
}
