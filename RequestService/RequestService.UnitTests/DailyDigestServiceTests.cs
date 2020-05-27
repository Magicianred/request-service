using HelpMyStreet.Contracts.AddressService.Response;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Models;
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
        private  Mock<IJobService> _jobservice;
        private  Mock<IUserService> _userService;
        private  Mock<IRepository> _repository;
        private  Mock<ICommunicationService> _communicationService;
        private  Mock<IOptionsSnapshot<ApplicationConfig>> _applicationConfig;
        private DailyDigestService _classUnderTest;
        private int _maxDistance = 3;
        private List<JobSummary> _jobSummaries;
        private List<JobSummary> _jobSummariesWithDistance { get; set; }
        private GetUsersResponse _users;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            SetupObjects();
            SetupRepository();
            SetupCommunciationService();
            SetupUserService();
            SetupConfig();
            SetupJobService();
            _classUnderTest = new DailyDigestService(_userService.Object, _jobservice.Object, _applicationConfig.Object, _communicationService.Object, _repository.Object);
        }

        private void SetupObjects()
        {
            _jobSummaries = new List<JobSummary>
            {
                new JobSummary
                {
                    JobID = 1,
                    PostCode = "T4ST1",
                    SupportActivity = HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn,
                },
                new JobSummary
                {
                    JobID = 2,
                    PostCode = "T4ST2",
                       SupportActivity = HelpMyStreet.Utils.Enums.SupportActivities.Shopping,
                }
            };
            // create copy of _joubSummaries and attach a distance
            _jobSummariesWithDistance = _jobSummaries.Select(x => x).ToList();
            _jobSummariesWithDistance.First().DistanceInMiles = 21;
            _jobSummariesWithDistance.ElementAt(1).DistanceInMiles = 2;

            _users = new GetUsersResponse
            {
                UserDetails = new List<UserDetails>
                {
                    new UserDetails
                    {
                       UserID = 1,
                       PostCode = "T4ST1"
                    },
                    new UserDetails
                    {
                       UserID = 2,
                       PostCode = "T4ST2"
                    }
                }
            };
        }
        private void SetupJobService()
        {
            _jobservice = _mockRepository.Create<IJobService>();
            _jobservice.Setup(x => x.AttachedDistanceToJobSummaries(It.IsAny<string>(), It.IsAny<List<JobSummary>>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => _jobSummariesWithDistance);
        }

        private void SetupConfig()
        {
            _applicationConfig = _mockRepository.Create<IOptionsSnapshot<ApplicationConfig>>();
            _applicationConfig.Setup(x => x.Value).Returns(() => new ApplicationConfig
            {
                EmailBaseUrl = "helpmytest.org",
                MaxDistanceDailyDigest = _maxDistance
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
            _repository.Verify(x => x.GetOpenJobsSummaries(), Times.Never);
            _jobservice.Verify(x => x.AttachedDistanceToJobSummaries(It.IsAny<string>(), It.IsAny<List<JobSummary>>(), It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task WhenNoRequestsAreReturned_IStopExecution()
        {
            _jobSummaries = new List<JobSummary>();
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
           
            _jobservice.Verify(x => x.AttachedDistanceToJobSummaries(It.IsAny<string>(), It.IsAny<List<JobSummary>>(), It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task WhenJobServiceReturnsNoJobSummaries_ISkipUser()
        {
            _jobservice.Setup(x => x.AttachedDistanceToJobSummaries("T4ST1", It.IsAny<List<JobSummary>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<JobSummary>());
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenUserIsOutsideRadius_IDontSendEmail()
        {
            _maxDistance = 1;
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task WhenUserIsInsideRadius_ISendEmail()
        {
            _maxDistance = 30;
            await _classUnderTest.SendDailyDigestEmailAsync(new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUserAsync(It.IsAny<SendEmailToUserRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }
    }
}