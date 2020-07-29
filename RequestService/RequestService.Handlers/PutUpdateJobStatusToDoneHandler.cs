using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Response;

namespace RequestService.Handlers
{
    public class PutUpdateJobStatusToDoneHandler : IRequestHandler<PutUpdateJobStatusToDoneRequest, PutUpdateJobStatusToDoneResponse>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        public PutUpdateJobStatusToDoneHandler(IRepository repository, ICommunicationService communicationService)
        {
            _repository = repository;
            _communicationService = communicationService;
        }

        public async Task<PutUpdateJobStatusToDoneResponse> Handle(PutUpdateJobStatusToDoneRequest request, CancellationToken cancellationToken)
        {
            var result = await  _repository.UpdateJobStatusDoneAsync(request.JobID, request.CreatedByUserID, cancellationToken);
            if (result) 
            {
                await _communicationService.RequestCommunication(
                    new RequestCommunicationRequest()
                    {
                        CommunicationJob = new CommunicationJob() { CommunicationJobType = CommunicationJobTypes.SendTaskStateChangeUpdate},
                        JobID = request.JobID
                    },
                    cancellationToken);
            }
            PutUpdateJobStatusToDoneResponse response = new PutUpdateJobStatusToDoneResponse()
            {
                Success = result
            };
            return response;
        }
    }
}
