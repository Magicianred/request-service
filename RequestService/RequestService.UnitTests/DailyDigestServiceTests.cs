using HelpMyStreet.Contracts.AddressService.Response;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Utils;

namespace RequestService.UnitTests
{
    public class DailyDigestServiceTests
    {
        private MockRepository _mockRepository;
        private Mock<IJobFilteringService> _jobFilteringService;
        private Mock<IUserService> _userService;
        private Mock<IRepository> _repository;
        private Mock<ICommunicationService> _communicationService;
        private Mock<IGroupService> _groupService;
        private Mock<IOptionsSnapshot<ApplicationConfig>> _applicationConfig;
        private DailyDigestService _classUnderTest;
        private int _maxDistance = 3;
        private List<JobSummary> _jobSummaries;
        private GetUsersResponse _users;
        private Mock<ILogger<DailyDigestService>> _logger;
        private GetUserGroupsResponse _groupResponse;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);

            SetupObjects();
            SetupLogger();
            SetupRepository();
            SetupCommunciationService();
            SetupUserService();
            SetupConfig();
            SetUpJobFilteringService();
            SetupGroupService();
            _classUnderTest = new DailyDigestService(_userService.Object, _applicationConfig.Object, _communicationService.Object, _repository.Object, _logger.Object, _jobFilteringService.Object,_groupService.Object);
        }

        private void SetupLogger()
        {
            _logger = _mockRepository.Create<ILogger<DailyDigestService>>();
        }

        private void SetupObjects()
        {
            _jobSummaries = new List<JobSummary>
            {
                new JobSummary
                {
                    JobID = 1,
                    SupportActivity = SupportActivities.CheckingIn,
                    DistanceInMiles = 5d
                },
                new JobSummary
                {
                    JobID = 2,
                    SupportActivity = SupportActivities.DogWalking,
                    DistanceInMiles = 20d
                },
                new JobSummary
                {
                    JobID = 2,
                    SupportActivity = SupportActivities.FaceMask,
                    DistanceInMiles = 20d
                }
            };

            _users = new GetUsersResponse
            {
                UserDetails = new List<UserDetails>
                {
                    new UserDetails
                    {
                       UserID = 1,
                       PostCode = "T4ST1",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.Errands}
                    },
                    new UserDetails
                    {
                       UserID = 2,
                       PostCode = "T4ST2",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.Shopping}
                    }
                }
            };
        }
        private void SetUpJobFilteringService()
        {
            _jobFilteringService = _mockRepository.Create<IJobFilteringService>();
            _jobFilteringService.Setup(x => x.FilterJobSummaries(
                It.IsAny<List<JobSummary>>(),
                It.IsAny<List<SupportActivities>>(),
                It.IsAny<string>(),
                It.IsAny<double?>(),
                It.IsAny<Dictionary<SupportActivities, double?>>(),
                It.IsAny<int?>(),
                It.IsAny<List<int>>(),
                It.IsAny<List<JobStatuses>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _jobSummaries);
        }

        private void SetupConfig()
        {
            _applicationConfig = _mockRepository.Create<IOptionsSnapshot<ApplicationConfig>>();
            _applicationConfig.Setup(x => x.Value).Returns(() => new ApplicationConfig
            {
                EmailBaseUrl = "helpmytest.org",
                DistanceInMilesForDailyDigest = _maxDistance
            });
        }

        private void SetupUserService()
        {
            _userService = _mockRepository.Create<IUserService>();
            _userService.Setup(x => x.GetUsers(It.IsAny<CancellationToken>())).ReturnsAsync(() => _users);
        }

        private void SetupCommunciationService()
        {
            _communicationService = _mockRepository.Create<ICommunicationService>();
            _communicationService.Setup(X => X.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()));

        }

        private void SetupGroupService()
        {
            _groupService = _mockRepository.Create<IGroupService>();

            _groupResponse = new GetUserGroupsResponse()
            {
                Groups = new List<int>() {1,2}
            };

            _groupService.Setup(X => X.GetUserGroups(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _groupResponse);

        }

        private void SetupRepository()
        {
            _repository = _mockRepository.Create<IRepository>();
            _repository.Setup(x => x.GetOpenJobsSummaries())
                 .Returns(() => _jobSummaries);
        }

        [Test]
        public async Task WhenNoUsersAreReturned_IStopExecution()
        {
            _users = new GetUsersResponse();
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
            _repository.Verify(x => x.GetOpenJobsSummaries(), Times.Once);
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task WhenNoRequestsAreReturned_IStopExecution()
        {
            _jobSummaries = new List<JobSummary>();
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());

            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task WhenUserIsOutsideRadiusAndTaskTypeMatches_IDontSendEmail()
        {
            _maxDistance = 1;
            _users = new GetUsersResponse
            {
                UserDetails = new List<UserDetails>
                {
                    new UserDetails
                    {
                       UserID = 1,
                       PostCode = "T4ST1",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.Errands}
                    },
                    new UserDetails
                    {
                       UserID = 2,
                       PostCode = "T4ST2",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.Shopping}
                    },
                    new UserDetails
                    {
                       UserID = 3,
                       PostCode = "T4ST2",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.DogWalking}
                    },
                    new UserDetails
                    {
                       UserID = 4,
                       PostCode = "T4ST2",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.FaceMask}
                    }
                }
            };
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task WhenUserIsInsideRadiusAndTaskTypeMatches_ISendEmail()
        {
            _maxDistance = 30;
            _users = new GetUsersResponse
            {
                UserDetails = new List<UserDetails>
                {
                    new UserDetails
                    {
                       UserID = 1,
                       PostCode = "T4ST1",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.Errands}
                    },
                    new UserDetails
                    {
                       UserID = 2,
                       PostCode = "T4ST2",
                       SupportRadiusMiles = 3,
                       SupportActivities = new List<SupportActivities> { SupportActivities.Shopping}
                    },
                    new UserDetails
                    {
                       UserID = 3,
                       PostCode = "T4ST2",
                       SupportRadiusMiles = 30,
                       SupportActivities = new List<SupportActivities> { SupportActivities.DogWalking}
                    }
                }
            };
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        }

        [Test]
        public async Task WhenUserIsInsideRadiusAndTaskTypeDoesNotMatch_IDontSendEmail()
        {
            _maxDistance = 30;
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}