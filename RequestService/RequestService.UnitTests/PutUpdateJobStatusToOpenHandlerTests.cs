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
    public class PutUpdateJobStatusToOpenHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<ICommunicationService> _communicationService;
        private PutUpdateJobStatusToOpenHandler _classUnderTest;
        private PutUpdateJobStatusToOpenRequest _request;
        private bool _success;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupCommunicationService();
            _classUnderTest = new PutUpdateJobStatusToOpenHandler(_repository.Object, _communicationService.Object);
        }


        private void SetupCommunicationService()
        {
            _communicationService = new Mock<ICommunicationService>();
            _communicationService.Setup(x => x.RequestCommunication(It.IsAny<RequestCommunicationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }


        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.UpdateJobStatusOpenAsync(
                It.IsAny<int>(), 
                It.IsAny<int>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _success);

        }

        [Test]
        public async Task WhenSuccessfullyChangingJobStatusToOpen_ReturnsTrue()
        {
            _success = true;
            _request = new PutUpdateJobStatusToOpenRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            
            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public async Task WhenUnSuccessfullyChangingJobStatusToOpen_ReturnsFalse()
        {
            _success = false;
            _request = new PutUpdateJobStatusToOpenRequest
            {
                CreatedByUserID = 1,
                JobID = 1
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(false, response.Success);
        }
    }
}