using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Utils.Enums;
using Moq;
using NUnit.Framework;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class PutUpdateJobStatusToDoneHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<ICommunicationService> _communicationService;
        private Mock<IJobService> _jobService;

        private PutUpdateJobStatusToDoneHandler _classUnderTest;
        private PutUpdateJobStatusToDoneRequest _request;
        private UpdateJobStatusOutcome _updateJobStatusOutcome;
        private bool _hasPermission = true;
        private bool _isSameAsProposed = false;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupCommunicationService();
            SetupJobService();
            _classUnderTest = new PutUpdateJobStatusToDoneHandler(_repository.Object, _communicationService.Object,_jobService.Object);
        }

        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.UpdateJobStatusDoneAsync(
                It.IsAny<int>(), 
                It.IsAny<int>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _updateJobStatusOutcome);

            _repository.Setup(x => x.JobHasSameStatusAsProposedStatus(
                It.IsAny<int>(),
                It.IsAny<JobStatuses>())).Returns(() => _isSameAsProposed);

        }

        private void SetupJobService()
        {
            _jobService = new Mock<IJobService>();
            _jobService.Setup(x => x.HasPermissionToChangeStatusAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _hasPermission);
        }

        private void SetupCommunicationService()
        {
            _communicationService = new Mock<ICommunicationService>();
            _communicationService.Setup(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }

        [Test]
        public async Task WhenSuccessfullyChangingJobStatusToDone_ReturnsTrue()
        {
            _updateJobStatusOutcome =  UpdateJobStatusOutcome.Success;
            _request = new PutUpdateJobStatusToDoneRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            _isSameAsProposed = false;
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.JobHasSameStatusAsProposedStatus(It.IsAny<int>(), It.IsAny<JobStatuses>()), Times.Once);
            _repository.Verify(x => x.UpdateJobStatusDoneAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.AreEqual(UpdateJobStatusOutcome.Success, response.Outcome);
        }

        [Test]
        public async Task WhenUnSuccessfullyChangingJobStatusToDone_ReturnsFalse()
        {
            _updateJobStatusOutcome = UpdateJobStatusOutcome.BadRequest;
            _request = new PutUpdateJobStatusToDoneRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            _isSameAsProposed = false;
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.JobHasSameStatusAsProposedStatus(It.IsAny<int>(), It.IsAny<JobStatuses>()), Times.Once);
            _repository.Verify(x => x.UpdateJobStatusDoneAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobStatusOutcome.BadRequest, response.Outcome);
        }

        [Test]
        public async Task WhenVolunteerDoesNotHavePermission_ReturnsUnauthorised()
        { 
            _updateJobStatusOutcome = UpdateJobStatusOutcome.Unauthorized;

            _hasPermission = false;
            _isSameAsProposed = false;
            _request = new PutUpdateJobStatusToDoneRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.JobHasSameStatusAsProposedStatus(It.IsAny<int>(), It.IsAny<JobStatuses>()), Times.Once);
            _repository.Verify(x => x.UpdateJobStatusDoneAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobStatusOutcome.Unauthorized, response.Outcome);
        }

        [Test]
        public async Task WhenJobStatusIsAlreadyDone_ReturnsAlreadyInThisStatus()
        {
            _updateJobStatusOutcome =  UpdateJobStatusOutcome.AlreadyInThisStatus;
            _isSameAsProposed = true;
            _request = new PutUpdateJobStatusToDoneRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.JobHasSameStatusAsProposedStatus(It.IsAny<int>(), It.IsAny<JobStatuses>()), Times.Once);
            _repository.Verify(x => x.UpdateJobStatusDoneAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _communicationService.Verify(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobStatusOutcome.AlreadyInThisStatus, response.Outcome);
        }
    }
}