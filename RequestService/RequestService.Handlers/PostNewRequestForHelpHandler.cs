using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using HelpMyStreet.Contracts.RequestService.Response;
using RequestService.Core.Dto;

namespace RequestService.Handlers
{
    public class PostNewRequestForHelpHandler : IRequestHandler<PostNewRequestForHelpRequest, PostNewRequestForHelpResponse>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public PostNewRequestForHelpHandler(
            IRepository repository, 
            IUserService userService, 
            IAddressService addressService,
            ICommunicationService communicationService,
            IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _userService = userService;
            _addressService = addressService;
            _communicationService = communicationService;
            _applicationConfig = applicationConfig;
        }

        private void CopyRequestorAsRecipient(PostNewRequestForHelpRequest request)
        {
            request.HelpRequest.Requestor.Address.Postcode = HelpMyStreet.Utils.Utils.PostcodeFormatter.FormatPostcode(request.HelpRequest.Requestor.Address.Postcode);

            if (request.HelpRequest.ForRequestor)
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

            int championCount = await _userService.GetChampionCountByPostcode(postcode, cancellationToken);
            if (championCount > 0)
            {
                response.Fulfillable = Fulfillable.Accepted_PassToStreetChampion;
            }
            else
            {
                response.Fulfillable = Fulfillable.Accepted_ManualReferral;
            }

            var result = await _repository.NewHelpRequestAsync(request, response.Fulfillable);
            response.RequestID = result;

            foreach(HelpMyStreet.Utils.Models.Job job in request.NewJobsRequest.Jobs)
            {
                EmailJobDTO emailJob = EmailJobDTO.GetEmailJobDTO(request, job, postcode);

                bool commsSent = await SendEmailAsync(
                  request.HelpRequest.ForRequestor
                , job.HealthCritical
                , postcode
                , request.HelpRequest.Requestor
                , job.SupportActivity
                , job.Details
                , request.HelpRequest.SpecialCommunicationNeeds
                , request.HelpRequest.OtherDetails
                , cancellationToken);
                await _repository.UpdateCommunicationSentAsync(response.RequestID, commsSent, cancellationToken);
            }

            
            return response;
        }

        private async Task<bool> SendEmailAsync(
            bool forRequestor, 
            bool isHealthCritical, 
            string postCode, 
            HelpMyStreet.Utils.Models.RequestPersonalDetails requestorDetails, 
            SupportActivities supportActivity,
            string jobDetails,
            string specialCommunicationNeeds,
            string otherDetails,
            CancellationToken cancellationToken)
        {
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
                    BodyHTML = EmailBuilder.BuildHelpRequestedEmail(forRequestor, isHealthCritical, requestorDetails, supportActivity, postCode, jobDetails,specialCommunicationNeeds,otherDetails)
                };
                bool manualEmailSent = await _communicationService.SendEmail(request, cancellationToken);

                if (!string.IsNullOrEmpty(requestorDetails.EmailAddress))
                {
                    SendEmailRequest confirmationNoChampion = new SendEmailRequest()
                    {
                        Subject = "Thank you for registering your request via HelpMyStreet.org",
                        ToAddress = requestorDetails.EmailAddress,
                        ToName = $"{requestorDetails.FirstName} {requestorDetails.LastName}",
                        BodyHTML = EmailBuilder.BuildConfirmationRequestEmail(false)
                    };
                    await _communicationService.SendEmail(confirmationNoChampion, cancellationToken);
                }
                return manualEmailSent;
            }


            int toUserId = ChampionIds.First();
            if (champions.Users.Count > 1)
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
                $"{requestorDetails.FirstName} {requestorDetails.LastName} has requested some help with {supportActivity.ToString()} \r\n" +
                $"Here Are some details to get in touch with {requestorDetails.FirstName} {requestorDetails.LastName}" +
                $"Email Address: {requestorDetails.EmailAddress} \r\n" +
                $"Phone Number: {requestorDetails.MobileNumber} \r\n" +
                $"Alternative Number: {requestorDetails.OtherNumber} \r\n" +
                $"Further Details: {jobDetails} \r\n" +
                $"On Behalf of Someone: {forRequestor} \r\n" +
                $"Critical to Health or Wellbeing Concern: {isHealthCritical} \r\n" +
                $"Thank you \r\n" +
                $"HelpMyStreet \r\n",
                BodyHTML = EmailBuilder.BuildHelpRequestedEmail(
                    forRequestor,
                    isHealthCritical,
                    requestorDetails, 
                    supportActivity, 
                    postCode,
                    jobDetails,
                    specialCommunicationNeeds,
                    otherDetails
                    )
            };

            bool emailSent = await _communicationService.SendEmailToUsersAsync(emailRequest, cancellationToken);
            if (!string.IsNullOrEmpty(requestorDetails.EmailAddress))
            {
                SendEmailRequest confimration = new SendEmailRequest()
                {
                    Subject = "Thank you for registering your request via HelpMyStreet.org",
                    ToAddress = requestorDetails.EmailAddress,
                    ToName = $"{requestorDetails.FirstName} {requestorDetails.LastName}",
                    BodyHTML = EmailBuilder.BuildConfirmationRequestEmail(true)
                };

                await _communicationService.SendEmail(confimration, cancellationToken);
            }
            return emailSent;
        }

    }
}
