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
    public class PutUpdateJobDueDateHandler : IRequestHandler<PutUpdateJobDueDateRequest, PutUpdateJobDueDateResponse>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        private readonly IJobService _jobService;
        public PutUpdateJobDueDateHandler(IRepository repository, ICommunicationService communicationService, IJobService jobService)
        {
            _repository = repository;
            _communicationService = communicationService;
            _jobService = jobService;
        }

        public async Task<PutUpdateJobDueDateResponse> Handle(PutUpdateJobDueDateRequest request, CancellationToken cancellationToken)
        {
            PutUpdateJobDueDateResponse response = new PutUpdateJobDueDateResponse()
            {
                Outcome = UpdateJobOutcome.Unauthorized
            };

            bool hasPermission = await _jobService.HasPermissionToChangeJobAsync(request.JobID.Value, request.AuthorisedByUserID.Value, cancellationToken);

            if (hasPermission)
            {
                var result = await _repository.UpdateJobDueDateAsync(request.JobID.Value, request.AuthorisedByUserID.Value, request.DueDate, cancellationToken);
                response.Outcome = result;

                if (result == UpdateJobOutcome.Success)
                {
                    response.Outcome = UpdateJobOutcome.Success;
                    await _communicationService.RequestCommunication(
                    new RequestCommunicationRequest()
                    {
                        CommunicationJob = new CommunicationJob() { CommunicationJobType = CommunicationJobTypes.SendTaskStateChangeUpdate },
                        JobID = request.JobID,
                        AdditionalParameters = new Dictionary<string, string>()
                        {
                            { "FieldUpdated","Due Date" }
                        }
                    },
                    cancellationToken);
                }
            }
            return response;
        }
    }
}
