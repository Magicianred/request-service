using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using HelpMyStreet.Contracts.RequestService.Response;
using RequestService.Core.Dto;
using Newtonsoft.Json;

namespace RequestService.Handlers
{
    public class PostNewRequestForHelpHandler : IRequestHandler<PostNewRequestForHelpRequest, PostNewRequestForHelpResponse>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IGroupService _groupService;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public PostNewRequestForHelpHandler(
            IRepository repository,
            IUserService userService,
            IAddressService addressService,
            ICommunicationService communicationService,
            IGroupService groupService,
            IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _userService = userService;
            _addressService = addressService;
            _communicationService = communicationService;
            _groupService = groupService;
            _applicationConfig = applicationConfig;
        }

        private void CopyRequestorAsRecipient(PostNewRequestForHelpRequest request)
        {
            request.HelpRequest.Requestor.Address.Postcode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.HelpRequest.Requestor.Address.Postcode);

            if (request.HelpRequest.RequestorType == RequestorType.Myself)
            {
                request.HelpRequest.Recipient = request.HelpRequest.Requestor;                
            }
            else
            {
                request.HelpRequest.Recipient.Address.Postcode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.HelpRequest.Recipient.Address.Postcode);
            }
        }

        public async Task<PostNewRequestForHelpResponse> Handle(PostNewRequestForHelpRequest request, CancellationToken cancellationToken)
        {
            PostNewRequestForHelpResponse response = new PostNewRequestForHelpResponse();

            CopyRequestorAsRecipient(request);
            string postcode = request.HelpRequest.Recipient.Address.Postcode;
     
            var postcodeValid = await _addressService.IsValidPostcode(postcode, cancellationToken);

            if (!postcodeValid || postcode.Length > 10)
            {
                return new PostNewRequestForHelpResponse
                {
                    RequestID = -1,
                    Fulfillable = Fulfillable.Rejected_InvalidPostcode
                };
            }

            // ifdosnet have a volunteerUserId then its a normal request
            if (!request.HelpRequest.VolunteerUserId.HasValue)
            {
                int championCount = await _userService.GetChampionCountByPostcode(postcode, cancellationToken);
                if (championCount > 0)
                {
                    response.Fulfillable = Fulfillable.Accepted_PassToStreetChampion;
                }
                else
                {
                    response.Fulfillable = Fulfillable.Accepted_ManualReferral;
                }
            }
            else
            {
                response.Fulfillable = Fulfillable.Accepted_DiyRequest;
            }

            var result = await _repository.NewHelpRequestAsync(request, response.Fulfillable);
            response.RequestID = result;

            var actions = _groupService.GetNewRequestActions(new HelpMyStreet.Contracts.GroupService.Request.GetNewRequestActionsRequest()
            {
                HelpRequest = request.HelpRequest,
                NewJobsRequest = request.NewJobsRequest
            },cancellationToken).Result;

            if(actions!=null)
            {
               // actions.Actions[]
            }

            EmailJobDTO emailJob = EmailJobDTO.GetEmailJobDTO(request, request.NewJobsRequest.Jobs.First(), postcode);

            bool commsSent = await SendEmailAsync(
                emailJob
            , response.Fulfillable
            , cancellationToken);
            await _repository.UpdateCommunicationSentAsync(response.RequestID, commsSent, cancellationToken);
            
            
            return response;
        }

        private async Task<bool> SendEmailAsync(EmailJobDTO emailJobDTO, Fulfillable fulfillable, CancellationToken cancellationToken)
        {
            List<bool> emailsSent = new List<bool>();            
            if (fulfillable != Fulfillable.Accepted_DiyRequest)
            {
                var helperResponse = await _userService.GetHelpersByPostcodeAndTaskType(emailJobDTO.PostCode, new List<SupportActivities> { emailJobDTO.Activity }, cancellationToken);
                if (helperResponse.Volunteers == null || helperResponse.Volunteers.Count() == 0)
                {
                    SendEmailRequest emailRequest = new SendEmailRequest
                    {
                        ToAddress = _applicationConfig.Value.ManualReferEmail,
                        ToName = _applicationConfig.Value.ManualReferName,
                        Subject = "ACTION REQUIRED: A REQUEST FOR HELP has arrived via HelpMyStreet.org",
                        BodyHTML = EmailBuilder.BuildHelpRequestedEmail(emailJobDTO, _applicationConfig.Value.EmailBaseUrl)
                    };
                    await _communicationService.SendEmail(emailRequest, cancellationToken);
                }

            
                foreach (var volunteer in helperResponse.Volunteers)
                {
                    emailJobDTO.IsVerified = volunteer.IsVerified.Value;
                    emailJobDTO.IsStreetChampionOfPostcode = volunteer.IsStreetChampionForGivenPostCode.Value;
                    emailJobDTO.DistanceFromPostcode = volunteer.DistanceInMiles;

                    SendEmailToUserRequest emailRequest = new SendEmailToUserRequest
                    {
                        ToUserID = volunteer.UserID,
                        Subject = "ACTION REQUIRED: A REQUEST FOR HELP has arrived via HelpMyStreet.org",
                        BodyHTML = EmailBuilder.BuildHelpRequestedEmail(emailJobDTO, _applicationConfig.Value.EmailBaseUrl)
                    };
                    emailsSent.Add(await _communicationService.SendEmailToUserAsync(emailRequest, cancellationToken));
                };
            }

            if (!string.IsNullOrEmpty(emailJobDTO.Requestor.EmailAddress))
            {
                SendEmailRequest confirmation = new SendEmailRequest()
                {
                    Subject = "Thank you for registering your request via HelpMyStreet.org",
                    ToAddress = emailJobDTO.Requestor.EmailAddress,
                    ToName = $"{emailJobDTO.Requestor.FirstName} {emailJobDTO.Requestor.LastName}",
                    BodyHTML = EmailBuilder.BuildConfirmationRequestEmail(true, emailJobDTO, fulfillable == Fulfillable.Accepted_DiyRequest, _applicationConfig.Value.EmailBaseUrl)
                };

                emailsSent.Add(await _communicationService.SendEmail(confirmation, cancellationToken));
            }

            return emailsSent.Count > 0;
        }
    }
}
