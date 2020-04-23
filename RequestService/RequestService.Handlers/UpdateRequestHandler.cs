using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;

namespace RequestService.Handlers
{
    public class UpdateRequestHandler : IRequestHandler<UpdateRequestRequest>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        private readonly IUserService _userService;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public UpdateRequestHandler(IRepository repository, ICommunicationService communicationService, IUserService userService, IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _communicationService = communicationService;
            _userService = userService;
            _applicationConfig = applicationConfig;
        }

        public async Task<Unit> Handle(UpdateRequestRequest request, CancellationToken cancellationToken)
        {
           var personalDetails =  await UpdatePersonalDetailsAsync(request, cancellationToken);

           var supportDetails = await UpdateSupportActivitiesAsync(request.RequestID, request.SupportActivitiesRequired.SupportActivities, cancellationToken);

           bool commsSent = await SendEmailAsync(request.RequestID, personalDetails, supportDetails, cancellationToken);

           await _repository.UpdateCommunicationSentAsync(request.RequestID, commsSent, cancellationToken);

           return await Unit.Task;
        }

        private async Task<bool> SendEmailAsync(int requestId, PersonalDetailsDto requestorDetails, SupportActivityDTO supportActivities, CancellationToken cancellationToken )
        {            

            string postCode = await _repository.GetRequestPostCodeAsync(requestId, cancellationToken);
            var champions = await _userService.GetChampionsByPostcode(postCode, cancellationToken);         
            
            List<int> ChampionIds = champions.Users.Select(x => x.ID).ToList();
            List<int> ccList = new List<int>();             
            if (champions.Users.Count == 0)
            {
                SendEmailRequest request = new SendEmailRequest()
                {
                    Subject = "MANUAL ACTION REQUIRED: A REQUEST FOR HELP has arrived via HelpMyStreet.org",
                    ToAddress = _applicationConfig.Value.ManualReferEmail,
                    ToName = _applicationConfig.Value.ManualReferName,
                    BodyHTML = EmailBuilder.BuildHelpRequestedEmail(requestorDetails, supportActivities, postCode)
                };
                bool manualEmailSent = await _communicationService.SendEmail(request, cancellationToken);

                if (!string.IsNullOrEmpty(requestorDetails.RequestorEmailAddress))
                {
                    SendEmailRequest confirmationNoChampion = new SendEmailRequest()
                    {
                        Subject = "Thank you for registering your request via HelpMyStreet.org",
                        ToAddress = requestorDetails.RequestorEmailAddress,
                        ToName = $"{requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName}",
                        BodyHTML = EmailBuilder.BuildConfirmationRequestEmail(false)
                    };
                    await _communicationService.SendEmail(confirmationNoChampion, cancellationToken);
                }
                return manualEmailSent;
            }


            int toUserId = ChampionIds.First();           
            if(champions.Users.Count > 1)
            {
                Random random = new Random();
                var randomElementIndex = random.Next(0, (ChampionIds.Count - 1));
                toUserId = ChampionIds.ElementAt(randomElementIndex);
                ChampionIds.RemoveAt(randomElementIndex);

                if (champions.Users.Count > 3)
                {                    
                    var randomCCElementIndex = random.Next(0, (ChampionIds.Count - 1));
                    ccList.Add(ChampionIds.ElementAt(randomCCElementIndex));
                    ChampionIds.RemoveAt(randomCCElementIndex);

                    randomCCElementIndex = random.Next(0, (ChampionIds.Count - 1));
                    ccList.Add(ChampionIds.ElementAt(randomCCElementIndex));
                    ChampionIds.RemoveAt(randomCCElementIndex);
                }
                else
                {                    
                    ccList = ChampionIds.Select(x => x).ToList();
                }                            
            }

            var selectedChampion = champions.Users.First(x => x.ID == toUserId);            
            SendEmailToUsersRequest emailRequest = new SendEmailToUsersRequest
            {
                Recipients = new Recipients
                {
                    ToUserIDs = new List<int> { toUserId },
                    CCUserIDs = ccList,
                },
                Subject = "ACTION REQUIRED: A REQUEST FOR HELP has arrived via HelpMyStreet.org",
                BodyText = $"Help Requested \r\n Hi {selectedChampion.UserPersonalDetails.FirstName} {selectedChampion.UserPersonalDetails.LastName}, \r\n " +
                $"{requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName} has requested some help with the following: \r\n" +
                $"{supportActivities.SupportActivities.Select(x => x.ToString() + "\r\n")}" +
                $"Here Are some details to get in touch with {requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName}" +
                $"Email Address: {requestorDetails.RequestorEmailAddress} \r\n" +
                $"Phone Number: {requestorDetails.RequestorPhoneNumber} \r\n" +
                $"Further Details: {requestorDetails.FurtherDetails} \r\n" +
                $"On Behalf of Someone: {requestorDetails.OnBehalfOfAnother} \r\n" +
                $"Health Or Wellbeing Concern: {requestorDetails.HealthOrWellbeingConcern} \r\n" +
                $"Thank you \r\n" +
                $"HelpMyStreet \r\n",
                BodyHTML = EmailBuilder.BuildHelpRequestedEmail(requestorDetails, supportActivities, postCode)
            };

            bool emailSent = await _communicationService.SendEmailToUsersAsync(emailRequest, cancellationToken);
            if (!string.IsNullOrEmpty(requestorDetails.RequestorEmailAddress))
            {
                SendEmailRequest confimration = new SendEmailRequest()
                {
                    Subject = "Thank you for registering your request via HelpMyStreet.org",
                    ToAddress = requestorDetails.RequestorEmailAddress,
                    ToName = $"{requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName}",
                    BodyHTML = EmailBuilder.BuildConfirmationRequestEmail(true)
                };

                await _communicationService.SendEmail(confimration, cancellationToken);
            }
            return emailSent;
        }
        

        

        private async Task<PersonalDetailsDto> UpdatePersonalDetailsAsync(UpdateRequestRequest request, CancellationToken cancellationToken)
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
                HealthOrWellbeingConcern = request.HealthOrWellbeingConcern

            };

            await _repository.UpdatePersonalDetailsAsync(dto, cancellationToken);

            return dto;
        }
        private async Task<SupportActivityDTO> UpdateSupportActivitiesAsync(int requestId,  List<HelpMyStreet.Utils.Enums.SupportActivities> activities, CancellationToken cancellationToken)
        {
            SupportActivityDTO activies = new SupportActivityDTO
            {
                RequestID = requestId,
                SupportActivities = activities
            };

            await _repository.AddSupportActivityAsync(activies, cancellationToken);
            return activies;
        }
    }
}
