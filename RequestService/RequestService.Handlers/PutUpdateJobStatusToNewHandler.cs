using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System.Collections.Generic;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.RequestService.Request;

namespace RequestService.Handlers
{
    public class PutUpdateJobStatusToNewHandler : IRequestHandler<PutUpdateJobStatusToNewRequest, PutUpdateJobStatusToNewResponse>
    {
        private readonly IRepository _repository;
        private readonly IJobService _jobService;
        private readonly ICommunicationService _communicationService;
        public PutUpdateJobStatusToNewHandler(IRepository repository, IJobService jobService, ICommunicationService communicationService)
        {
            _repository = repository;
            _jobService = jobService;
            _communicationService = communicationService;
        }

        public async Task<PutUpdateJobStatusToNewResponse> Handle(PutUpdateJobStatusToNewRequest request, CancellationToken cancellationToken)
        {
            PutUpdateJobStatusToNewResponse response = new PutUpdateJobStatusToNewResponse()
            {
                Outcome = UpdateJobStatusOutcome.Unauthorized
            };

            if (_repository.JobHasStatus(request.JobID, JobStatuses.New))
            {
                response.Outcome = UpdateJobStatusOutcome.AlreadyInThisStatus;
            }
            else
            {
                bool hasPermission = await _jobService.HasPermissionToChangeStatusAsync(request.JobID, request.CreatedByUserID, false, cancellationToken);

                if (hasPermission)
                {
                    var result = await _repository.UpdateJobStatusNewAsync(request.JobID, request.CreatedByUserID, cancellationToken);
                    response.Outcome = result;

                    if (result == UpdateJobStatusOutcome.Success)
                    {
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
