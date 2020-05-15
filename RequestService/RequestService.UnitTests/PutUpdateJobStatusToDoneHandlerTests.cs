using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
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
        private PutUpdateJobStatusToDoneHandler _classUnderTest;
        private PutUpdateJobStatusToDoneRequest _request;
        private bool _success;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            _classUnderTest = new PutUpdateJobStatusToDoneHandler(_repository.Object);
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