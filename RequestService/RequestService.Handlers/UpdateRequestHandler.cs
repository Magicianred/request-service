using RequestService.Core.Domains.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;

namespace RequestService.Handlers
{
    public class UpdateRequestHandler : IRequestHandler<UpdateRequestRequest>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        public UpdateRequestHandler(IRepository repository, ICommunicationService communicationService)
        {
            _repository = repository;
            _communicationService = communicationService;
        }

        public async Task<Unit> Handle(UpdateRequestRequest request, CancellationToken cancellationToken)
        {
            await UpdatePersonalDetailsAsync(request, cancellationToken);

            await UpdateSupportActivitiesAsync(request.RequestID, request.SupportActivtiesRequired.SupportActivities, cancellationToken);

            await SendEmailAsync(request.RequestorEmailAddress, $"{request.RequestorFirstName} {request.RequestorLastName}", 
                "Help My Street - Help Requested", "Help has been requested", cancellationToken);

            return await Unit.Task;
        }

        private async Task SendEmailAsync(string ToAddress, string ToName, string Subject, string BodyText, CancellationToken cancellationToken)
        {
            SendEmailRequest emailRequest = new SendEmailRequest
            {
                ToAddress = ToAddress,
                ToName = ToName,
                Subject = Subject,
                BodyText = BodyText
            };
            await _communicationService.SendEmailAsync(emailRequest, cancellationToken);
        }
        private async Task UpdatePersonalDetailsAsync(UpdateRequestRequest request, CancellationToken cancellationToken)
        {
            PersonalDetailsDto dto = new PersonalDetailsDto
            {
                RequestID = request.RequestID,
                RequestorEmailAddress = request.RequestorEmailAddress,
                RequestorFirstName = request.RequestorFirstName,
                RequestorLastName = request.RequestorLastName,
                RequestorPhoneNumber = request.RequestorPhoneNumber,
                FurtherDetails = request.FurtherDetails,
                OnBehalfOfAnother = request.OnBehalfOfAnother,
            };


            await _repository.UpdatePersonalDetailsAsync(dto, cancellationToken);           
        }
        private async Task UpdateSupportActivitiesAsync(int requestId,  List<HelpMyStreet.Utils.Enums.SupportActivities> activities, CancellationToken cancellationToken)
        {
            SupportActivityDTO activies = new SupportActivityDTO
            {
                RequestId = requestId,
                SupportActivities = activities
            };

            await _repository.AddSupportActivityAsync(activies, cancellationToken);
        }
    }
}
