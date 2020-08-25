using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class PutUpdateJobStatusToInProgressHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<ICommunicationService> _communicationService;
        private Mock<IGroupService> _groupService;
        private Mock<IUserService> _userService;

        private PutUpdateJobStatusToInProgressHandler _classUnderTest;
        private PutUpdateJobStatusToInProgressRequest _request;
        private bool _success;
        private GetUserGroupsResponse _getUserGroupsReponse;
        private GetUserRolesResponse _getUserRolesResponse;
        private List<int> _getGroupsForJobResponse;
        private int? _referringGroupId;
        private GetUserByIDResponse _getUserbyIdResponse;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupCommunicationService();
            SetupGroupService();
            SetupUserService();
            _classUnderTest = new PutUpdateJobStatusToInProgressHandler(_repository.Object, _communicationService.Object,_groupService.Object, _userService.Object);
        }

        private void SetupCommunicationService()
        {
            _communicationService = new Mock<ICommunicationService>();
            _communicationService.Setup(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }

        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.UpdateJobStatusInProgressAsync(
                It.IsAny<int>(), 
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _success);

            _repository.Setup(x => x.GetGroupsForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _getGroupsForJobResponse);

            _repository.Setup(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _referringGroupId);

        }

        private void SetupGroupService()
        {
            _groupService = new Mock<IGroupService>();
            _groupService.Setup(x => x.GetUserGroups(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _getUserGroupsReponse);

            _groupService.Setup(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _getUserRolesResponse);

        }

        private void SetupUserService()
        {
            _userService = new Mock<IUserService>();

            _getUserbyIdResponse = new GetUserByIDResponse()
            {
                User = new User()
                {
                    ID = 1,
                    IsVerified = true
                }
            };

            _userService.Setup(x => x.GetUser(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _getUserbyIdResponse);
        }

        //[Test]
        //public async Task WhenSuccessfullyChangingJobStatusToInProgress_ReturnsTrue()
        //{
        //    _success = true;
        //    _getUserGroupsReponse = new GetUserGroupsResponse()
        //    {
        //        Groups = new List<int>()
        //        {
        //            1
        //        }
        //    };
        //    _getGroupsForJobResponse = new List<int>()
        //    {
        //        1
        //    };
        //    _referringGroupId = 1;
        //    _getUserRolesResponse = new GetUserRolesResponse()
        //    {
        //        UserGroupRoles = new Dictionary<int, List<int>>()
        //    };

            

        //    _request = new PutUpdateJobStatusToInProgressRequest
        //    {
        //        CreatedByUserID = 1,
        //        JobID = 1,
        //        VolunteerUserID = 1
        //    };
        //    var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            
        //    Assert.AreEqual(UpdateJobStatusOutcome.Success, response.Outcome);
        //}

        //[Test]
        //public async Task WhenUnSuccessfullyChangingJobStatusToInProgress_ReturnsBadRequest()
        //{
        //    _success = false;
        //    _getUserGroupsReponse = new GetUserGroupsResponse()
        //    {
        //        Groups = new List<int>()
        //        {
        //            1
        //        }
        //    };
        //    _getGroupsForJobResponse = new List<int>()
        //    {
        //        1
        //    };
        //    _referringGroupId = 1;

        //    Dictionary<int, List<int>> roles = new Dictionary<int, List<int>>();
        //    roles.Add(1, new List<int>() { (int) GroupRoles.TaskAdmin });

        //    _getUserRolesResponse = new GetUserRolesResponse()
        //    {
        //        UserGroupRoles = roles
        //     };
        //    _request = new PutUpdateJobStatusToInProgressRequest
        //    {
        //        CreatedByUserID = 1,
        //        JobID = 1,
        //        VolunteerUserID = 1
        //    };
        //    var response = await _classUnderTest.Handle(_request, CancellationToken.None);
        //    Assert.AreEqual(UpdateJobStatusOutcome.BadRequest, response.Outcome);
        //}

        [Test]
        public async Task WhenUserIsNotVerified_ReturnsBadRequest()
        {
            _success = false;
            _request = new PutUpdateJobStatusToInProgressRequest
            {
                CreatedByUserID = 1,
                JobID = 1,
                VolunteerUserID = 1
            };
            _getUserbyIdResponse = new GetUserByIDResponse()
            {
                User = new User()
                {
                    ID = 1,
                    IsVerified = false
                }
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _groupService.Verify(x => x.GetUserGroups(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _repository.Verify(x => x.GetGroupsForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _groupService.Verify(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _repository.Verify(x => x.UpdateJobStatusInProgressAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobStatusOutcome.BadRequest, response.Outcome);
        }

        [Test]
        public async Task WhenjobGroupDoesNotContainsVolunteerGroups_ReturnsBadRequest()
        {
            _success = false;
            _request = new PutUpdateJobStatusToInProgressRequest
            {
                CreatedByUserID = 1,
                JobID = 1,
                VolunteerUserID = 1
            };
            _getUserbyIdResponse = new GetUserByIDResponse()
            {
                User = new User()
                {
                    ID = 1,
                    IsVerified = true
                }
            };

            _getUserGroupsReponse = new GetUserGroupsResponse()
            {
                Groups = new List<int>()
                {
                    1
                }
            };

            _getGroupsForJobResponse = new List<int>()
            {
                2
            };

            _referringGroupId = 1;

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _groupService.Verify(x => x.GetUserGroups(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetGroupsForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            _groupService.Verify(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _repository.Verify(x => x.UpdateJobStatusInProgressAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobStatusOutcome.BadRequest, response.Outcome);
        }


        [Test]
        public async Task WhenCreatedByUserIsDifferentToVolunteerUserIDAndNotTaskAdmin_ReturnsUnauthorized()
        {
            _success = false;
            _request = new PutUpdateJobStatusToInProgressRequest
            {
                CreatedByUserID = 1,
                JobID = 1,
                VolunteerUserID = 2
            };
            _getUserbyIdResponse = new GetUserByIDResponse()
            {
                User = new User()
                {
                    ID = 1,
                    IsVerified = true
                }
            };

            _getUserGroupsReponse = new GetUserGroupsResponse()
            {
                Groups = new List<int>()
                {
                    1
                }
            };

            _getGroupsForJobResponse = new List<int>()
            {
                1
            };

            _referringGroupId = 1;

            Dictionary<int, List<int>> roles = new Dictionary<int, List<int>>();
            roles.Add(1, new List<int>() { (int) GroupRoles.Member });

            _getUserRolesResponse = new GetUserRolesResponse()
            {
                UserGroupRoles = roles
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _groupService.Verify(x => x.GetUserGroups(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetGroupsForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _groupService.Verify(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            _repository.Verify(x => x.UpdateJobStatusInProgressAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobStatusOutcome.Unauthorized, response.Outcome);
        }

        [Test]
        public async Task WhenCreatedByUserIsDifferentToVolunteerUserIDAndTaskAdmin_ReturnsSuccess()
        {
            _success = true;
            _request = new PutUpdateJobStatusToInProgressRequest
            {
                CreatedByUserID = 1,
                JobID = 1,
                VolunteerUserID = 2
            };
            _getUserbyIdResponse = new GetUserByIDResponse()
            {
                User = new User()
                {
                    ID = 1,
                    IsVerified = true
                }
            };

            _getUserGroupsReponse = new GetUserGroupsResponse()
            {
                Groups = new List<int>()
                {
                    1
                }
            };

            _getGroupsForJobResponse = new List<int>()
            {
                1
            };

            _referringGroupId = 1;

            Dictionary<int, List<int>> roles = new Dictionary<int, List<int>>();
            roles.Add(1, new List<int>() { (int)GroupRoles.TaskAdmin });

            _getUserRolesResponse = new GetUserRolesResponse()
            {
                UserGroupRoles = roles
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _groupService.Verify(x => x.GetUserGroups(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetGroupsForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _groupService.Verify(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            _repository.Verify(x => x.UpdateJobStatusInProgressAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.AreEqual(UpdateJobStatusOutcome.Success, response.Outcome);
        }

        [TestCase(GroupRoles.TaskAdmin)]
        [TestCase(GroupRoles.Member)]
        [TestCase(GroupRoles.Owner)]
        [TestCase(GroupRoles.RequestSubmitter)]
        [TestCase(GroupRoles.UserAdmin)]
        public async Task WhenCreatedByUserIsSameAsVolunteerUserID_ReturnsSuccess(GroupRoles role)
        {
            _success = true;
            _request = new PutUpdateJobStatusToInProgressRequest
            {
                CreatedByUserID = 1,
                JobID = 1,
                VolunteerUserID = 1
            };
            _getUserbyIdResponse = new GetUserByIDResponse()
            {
                User = new User()
                {
                    ID = 1,
                    IsVerified = true
                }
            };

            _getUserGroupsReponse = new GetUserGroupsResponse()
            {
                Groups = new List<int>()
                {
                    1
                }
            };

            _getGroupsForJobResponse = new List<int>()
            {
                1
            };

            _referringGroupId = 1;

            Dictionary<int, List<int>> roles = new Dictionary<int, List<int>>();
            roles.Add(1, new List<int>() { (int)role });

            _getUserRolesResponse = new GetUserRolesResponse()
            {
                UserGroupRoles = roles
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _groupService.Verify(x => x.GetUserGroups(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetGroupsForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _groupService.Verify(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            _repository.Verify(x => x.UpdateJobStatusInProgressAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.AreEqual(UpdateJobStatusOutcome.Success, response.Outcome);
        }
    }
}