using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
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
    public class PutUpdateJobStatusToCancelledHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<IJobService> _jobService;
        private Mock<ICommunicationService> _communicationService;
        private PutUpdateJobStatusToCancelledHandler _classUnderTest;
        private PutUpdateJobStatusToCancelledRequest _request;
        private bool _success;
        private bool _hasPermission = true;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupJobService();
            SetupCommunicationService();
            _classUnderTest = new PutUpdateJobStatusToCancelledHandler(_repository.Object, _jobService.Object, _communicationService.Object);
        }

        private void SetupJobService()
        {
            _jobService = new Mock<IJobService>();
            _jobService.Setup(x => x.HasPermissionToChangeStatusAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _hasPermission);
        }

        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.UpdateJobStatusCancelledAsync(
                It.IsAny<int>(), 
                It.IsAny<int>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _success);

        }

        private void SetupCommunicationService()
        {
            _communicationService = new Mock<ICommunicationService>();
            _communicationService.Setup(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }

        [Test]
        public async Task WhenSuccessfullyChangingJobStatusToCancelled_ReturnsTrue()
        {
            _success = true;
            _request = new PutUpdateJobStatusToCancelledRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.UpdateJobStatusCancelledAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            
            Assert.AreEqual(UpdateJobStatusOutcome.Success, response.Outcome);
        }

        [Test]
        public async Task WhenUnSuccessfullyChangingJobStatusToCancelled_ReturnsFalse()
        {
            _success = false;
            _request = new PutUpdateJobStatusToCancelledRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.UpdateJobStatusCancelledAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.AreEqual(UpdateJobStatusOutcome.BadRequest, response.Outcome);
        }

        [Test]
        public async Task WhenVolunteerDoesNotHavePermission_ReturnsUnauthorised()
        {
            _success = false;
            _hasPermission = false;
            _request = new PutUpdateJobStatusToCancelledRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.UpdateJobStatusCancelledAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(UpdateJobStatusOutcome.Unauthorized, response.Outcome);
        }

        [Test]
        public async Task WhenJobStatusIsAlreadyCancelled_ReturnsBadRequest()
        {
            _success = false;
            _hasPermission = true;
            _request = new PutUpdateJobStatusToCancelledRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            _repository.Verify(x => x.UpdateJobStatusCancelledAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.AreEqual(UpdateJobStatusOutcome.BadRequest, response.Outcome);
        }
    }
}