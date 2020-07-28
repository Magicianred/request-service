using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.CommunicationService.Request;

namespace RequestService.Handlers
{
    public class PutUpdateJobStatusToOpenHandler : IRequestHandler<PutUpdateJobStatusToOpenRequest, PutUpdateJobStatusToOpenResponse>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        public PutUpdateJobStatusToOpenHandler(IRepository repository, ICommunicationService communicationService)
        {
            _repository = repository;
            _communicationService = communicationService;
        }

        public async Task<PutUpdateJobStatusToOpenResponse> Handle(PutUpdateJobStatusToOpenRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdateJobStatusOpenAsync(request.JobID, request.CreatedByUserID, cancellationToken);

            if (result)
            {
                await _communicationService.RequestCommunication(
                    new RequestCommunicationRequest()
                    {
                        CommunicationJob = new CommunicationJob() { CommunicationJobType = CommunicationJobTypes.SendTaskStateChangeUpdate },
                        JobID = request.JobID
                    },
                    cancellationToken);
            }
            PutUpdateJobStatusToOpenResponse response = new PutUpdateJobStatusToOpenResponse()
            {
                Success = result
            };
            return response;
        }
    }
}
