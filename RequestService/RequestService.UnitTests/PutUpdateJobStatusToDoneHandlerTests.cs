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
    public class PutUpdateJobStatusToDoneHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<ICommunicationService> _communicationService;

        private PutUpdateJobStatusToDoneHandler _classUnderTest;
        private PutUpdateJobStatusToDoneRequest _request;
        private bool _success;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupCommunicationService();
            _classUnderTest = new PutUpdateJobStatusToDoneHandler(_repository.Object, _communicationService.Object);
        }

        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.UpdateJobStatusDoneAsync(
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
        public async Task WhenSuccessfullyChangingJobStatusToDone_ReturnsTrue()
        {
            _success = true;
            _request = new PutUpdateJobStatusToDoneRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            
            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public async Task WhenUnSuccessfullyChangingJobStatusToDone_ReturnsFalse()
        {
            _success = false;
            _request = new PutUpdateJobStatusToDoneRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(false, response.Success);
        }
    }
}