using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Utils.Enums;
using Moq;
using NUnit.Framework;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class PutUpdateJobDueDateHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<ICommunicationService> _communicationService;
        private Mock<IJobService> _jobService;

        private PutUpdateJobDueDateHandler _classUnderTest;
        private PutUpdateJobDueDateRequest _request;
        private UpdateJobOutcome _updateJobOutcome;
        private bool _hasPermission = true;
        private bool _isSameAsProposed = false;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupCommunicationService();
            SetupJobService();
            _classUnderTest = new PutUpdateJobDueDateHandler(_repository.Object, _communicationService.Object,_jobService.Object);
        }

        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.UpdateJobDueDateAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<DateTime>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _updateJobOutcome);
        }

        private void SetupJobService()
        {
            _jobService = new Mock<IJobService>();
            _jobService.Setup(x => x.HasPermissionToChangeJobAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _hasPermission);
        }

        private void SetupCommunicationService()
        {
            _communicationService = new Mock<ICommunicationService>();
            _communicationService.Setup(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }

        [Test]
        public async Task WhenSuccessfullyChangingJobDueDate_ReturnsTrue()
        {
            _updateJobOutcome =  UpdateJobOutcome.Success;
            _request = new PutUpdateJobDueDateRequest
            {
                AuthorisedByUserID = 2,
                JobID = 1,
                DueDate = DateTime.Now

            };
            _isSameAsProposed = false;
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _jobService.Verify(x => x.HasPermissionToChangeJobAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateJobDueDateAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.AreEqual(UpdateJobOutcome.Success, response.Outcome);
        }

        [Test]
        public async Task WhenUnSuccessfullyChangingJobDueDate_ReturnsFalse()
        {
            _updateJobOutcome = UpdateJobOutcome.BadRequest;
            _request = new PutUpdateJobDueDateRequest
            {
                AuthorisedByUserID = 2,
                JobID = 1,
                DueDate = DateTime.Now
            };
            _isSameAsProposed = false;
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _jobService.Verify(x => x.HasPermissionToChangeJobAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateJobDueDateAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobOutcome.BadRequest, response.Outcome);
        }

        [Test]
        public async Task WhenVolunteerDoesNotHavePermission_ReturnsUnauthorised()
        { 
            _updateJobOutcome = UpdateJobOutcome.Unauthorized;

            _hasPermission = false;
            _isSameAsProposed = false;
            _request = new PutUpdateJobDueDateRequest
            {
                AuthorisedByUserID = 2,
                JobID = 1,
                DueDate = DateTime.Now
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _jobService.Verify(x => x.HasPermissionToChangeJobAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateJobDueDateAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobOutcome.Unauthorized, response.Outcome);
        }

        [Test]
        public async Task WhenJobStatusIsAlreadyDone_ReturnsAlreadyInThisStatus()
        {
            _updateJobOutcome =  UpdateJobOutcome.AlreadyInThisState;
            _isSameAsProposed = true;
            _request = new PutUpdateJobDueDateRequest
            {
                AuthorisedByUserID = 2,
                JobID = 1,
                DueDate = DateTime.Now
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _jobService.Verify(x => x.HasPermissionToChangeJobAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateJobDueDateAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobOutcome.AlreadyInThisState, response.Outcome);
        }
    }
}