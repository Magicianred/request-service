using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.GroupService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Enums;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class PostNewRequestForHelpHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<IUserService> _userService;
        private Mock<ICommunicationService> _communicationService;
        private Mock<IAddressService> _adddressService;
        private Mock<IGroupService> _groupService;
        private PostNewRequestForHelpHandler _classUnderTest;
        private PostNewRequestForHelpRequest _request;
        private Mock<IOptionsSnapshot<ApplicationConfig>> _applicationConfig;
        private int requestId;
        private bool _validPostcode;
        private int _championCount;
        private bool _emailSent;
        private GetNewRequestActionsResponse _getNewRequestActionsResponse;
        private GetVolunteersByPostcodeAndActivityResponse _getVolunteersByPostcodeAndActivityResponse;
        private GetGroupMembersResponse _getGroupMembersResponse;
        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupAddressService();
            SetupCommunicationService();
            SetupApplicationConfig();
            SetupUserService();
            SetupGroupService();
            _classUnderTest = new PostNewRequestForHelpHandler(_repository.Object, _userService.Object, _adddressService.Object, _communicationService.Object, _groupService.Object, _applicationConfig.Object);
        }
        private void SetupCommunicationService()
        {
            _communicationService = new Mock<ICommunicationService>();
            _communicationService.Setup(x => x.SendEmail(It.IsAny<SendEmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _emailSent);

        }
        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.NewHelpRequestAsync(
                It.IsAny<PostNewRequestForHelpRequest>(),
                It.IsAny<Fulfillable>()))
                .ReturnsAsync(() => requestId);
            _repository.Setup(x => x.UpdateCommunicationSentAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()));

        }
        private void SetupApplicationConfig()
        {
            _applicationConfig = new Mock<IOptionsSnapshot<ApplicationConfig>>();
            _applicationConfig.SetupGet(x => x.Value).Returns(new ApplicationConfig
            {
                ManualReferName = "test",
                ManualReferEmail = "manual@test.com",
                EmailBaseUrl = "helpmystreettest",
                FaceMaskChunkSize = 10
            });
        }

        private void SetupUserService()
        {
            _userService = new Mock<IUserService>();
            _userService.Setup(x => x.GetChampionCountByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _championCount);
            _userService.Setup(x => x.GetHelpersByPostcodeAndTaskType(It.IsAny<string>(), It.IsAny<List<SupportActivities>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _getVolunteersByPostcodeAndActivityResponse);
        }

        private void SetupAddressService()
        {
            _adddressService = new Mock<IAddressService>();
            _adddressService.Setup(x => x.IsValidPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _validPostcode);
        }

        private void SetupGroupService()
        {
            _groupService = new Mock<IGroupService>();
            Dictionary<int, TaskAction> actions = new Dictionary<int, TaskAction>();
            actions.Add(1, new TaskAction()
            {
                TaskActions = new Dictionary<NewTaskAction, List<int>>()
            });

            _getNewRequestActionsResponse = new GetNewRequestActionsResponse()
            {
                Actions = actions
            };


            _groupService.Setup(x => x.GetNewRequestActions(It.IsAny<GetNewRequestActionsRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _getNewRequestActionsResponse);

            _getGroupMembersResponse = new GetGroupMembersResponse()
            {
                Users = new List<int>() { 1, 2 }
            };

            _groupService.Setup(x => x.GetGroupMembers(It.IsAny<int>()))
                .ReturnsAsync(() => _getGroupMembersResponse);
        }

        [Test]
        public async Task WhenIPostDiyRequest_FullfiableStatusGetSetToDiy()
        {
            _validPostcode = true;
            _emailSent = true;
            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    RequestorType = RequestorType.Myself,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    },
                    VolunteerUserId = 1,
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            _getNewRequestActionsResponse = new GetNewRequestActionsResponse() { Actions = new Dictionary<int, TaskAction>() };
            _getNewRequestActionsResponse.Actions.Add(0, new TaskAction() { TaskActions = new Dictionary<NewTaskAction, List<int>>() });
            _getNewRequestActionsResponse.Actions[0].TaskActions.Add(NewTaskAction.AssignToVolunteer, new List<int>() { 1 });

            var response = await _classUnderTest.Handle(request, new CancellationToken());
            Assert.AreEqual(Fulfillable.Accepted_DiyRequest, response.Fulfillable);
        }


        [Test]
        public async Task WhenIPostDiyRequest_IOnlySendConfirmationEMail()
        {
            _validPostcode = true;
            _emailSent = true;
       
            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    RequestorType = RequestorType.Myself,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        EmailAddress = "test",
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    },
                    VolunteerUserId = 1,
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };
            _getNewRequestActionsResponse = new GetNewRequestActionsResponse() { Actions = new Dictionary<int, TaskAction>() };
            _getNewRequestActionsResponse.Actions.Add(0, new TaskAction() { TaskActions = new Dictionary<NewTaskAction, List<int>>() });
            _getNewRequestActionsResponse.Actions[0].TaskActions.Add(NewTaskAction.AssignToVolunteer, new List<int>() { 1 });

            await _classUnderTest.Handle(request, new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.IsAny<SendEmailToUsersRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            _repository.Verify(x => x.AssignJobToVolunteerAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _userService.Verify(x => x.GetHelpersByPostcodeAndTaskType(It.IsAny<string>(), It.IsAny <List<SupportActivities>>(), It.IsAny<CancellationToken>()), Times.Never);
        }



        [Test]
        public async Task WhenIPostRequest_WithInvalidPostcode_IGetRejected()
        {            
            _validPostcode = false;

            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    RequestorType = RequestorType.Myself,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            var response = await _classUnderTest.Handle(request, new CancellationToken());
            Assert.AreEqual(Fulfillable.Rejected_InvalidPostcode, response.Fulfillable);     
        }

        [Test]
        public async Task WhenIPostRequest_WithNoChampions_IGetManualReer()
        {
            requestId = 1;
            _validPostcode = true;
            _championCount = 0;
            _emailSent = true;
            _getVolunteersByPostcodeAndActivityResponse = new GetVolunteersByPostcodeAndActivityResponse
            {
                Volunteers = new List<VolunteerSummary>
                {
                    new VolunteerSummary
                    {
                        UserID = 1,
                         IsStreetChampionForGivenPostCode = true,
                         IsVerified = true,
                        DistanceInMiles = 1,
                    }
                }
            };
            var request = new PostNewRequestForHelpRequest
            {
                HelpRequest = new HelpMyStreet.Utils.Models.HelpRequest
                {
                    ForRequestor = true,
                    RequestorType = RequestorType.Myself,
                    Requestor = new HelpMyStreet.Utils.Models.RequestPersonalDetails
                    {
                        Address = new HelpMyStreet.Utils.Models.Address
                        {
                            Postcode = "test",
                        }
                    }
                },
                NewJobsRequest = new NewJobsRequest
                {
                    Jobs = new List<HelpMyStreet.Utils.Models.Job>
                    {
                        new HelpMyStreet.Utils.Models.Job
                        {
                            HealthCritical = true,
                            DueDays = 5,
                            SupportActivity = SupportActivities.Shopping
                        }
                    }
                }
            };

            _getNewRequestActionsResponse = new GetNewRequestActionsResponse() { Actions = new Dictionary<int, TaskAction>() };
            _getNewRequestActionsResponse.Actions.Add(0, new TaskAction() { TaskActions = new Dictionary<NewTaskAction, List<int>>() });
            _getNewRequestActionsResponse.Actions[0].TaskActions.Add(NewTaskAction.MakeAvailableToGroups, new List<int>() { 1 });
            _getNewRequestActionsResponse.Actions[0].TaskActions.Add(NewTaskAction.NotifyMatchingVolunteers, new List<int>() { 1 });


            var response = await _classUnderTest.Handle(request, new CancellationToken());
            Assert.AreEqual(Fulfillable.Accepted_ManualReferral, response.Fulfillable);
        }




    }
}