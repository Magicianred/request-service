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
using System;
using HelpMyStreet.Utils.Models;

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
            }, cancellationToken).Result;

            if(actions==null)
            {
                throw new Exception("No new request actions returned");
            }

            foreach(int jobID in actions.Actions.Keys)
            {
                if (actions.Actions[jobID].TaskActions.ContainsKey(NewTaskAction.MakeAvailableToGroups))
                {
                    List<int> availableGroups = actions.Actions[jobID].TaskActions[NewTaskAction.MakeAvailableToGroups];
                    if (availableGroups != null)
                    {
                        foreach (int i in availableGroups)
                        {
                            await _repository.AddJobAvailableToGroupAsync(jobID, i, cancellationToken);
                        }
                    }
                }

                if (actions.Actions[jobID].TaskActions.ContainsKey(NewTaskAction.NotifyMatchingVolunteers))
                {
                    List<int> matchingVolunteersGroup = actions.Actions[jobID].TaskActions[NewTaskAction.NotifyMatchingVolunteers];
                    if (matchingVolunteersGroup != null)
                    {
                        foreach (int i in matchingVolunteersGroup)
                        {
                            await _communicationService.RequestCommunication(new RequestCommunicationRequest()
                            {
                                JobID = jobID,
                                CommunicationJob = new CommunicationJob()
                                {
                                    CommunicationJobType = CommunicationJobTypes.SendNewTaskNotification
                                },
                                GroupID = i
                            }, cancellationToken);
                        }
                    }
                }

                if (actions.Actions[jobID].TaskActions.ContainsKey(NewTaskAction.AssignToVolunteer))
                {
                    List<int> volunteer = actions.Actions[jobID].TaskActions[NewTaskAction.AssignToVolunteer];
                    if (volunteer != null)
                    {
                        foreach (int i in volunteer)
                        {
                            await _repository.AssignJobToVolunteerAsync(jobID, i, cancellationToken);
                        }
                    }
                }
            }            
            return response;
        }
    }
}
