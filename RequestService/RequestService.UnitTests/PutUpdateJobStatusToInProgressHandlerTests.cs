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
    public class PutUpdateJobStatusToInProgressHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<IJobService> _jobService;
        private PutUpdateJobStatusToInProgressHandler _classUnderTest;
        private PutUpdateJobStatusToInProgressRequest _request;
        private bool _success;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            SetupJobService();
            _classUnderTest = new PutUpdateJobStatusToInProgressHandler(_repository.Object, _jobService.Object);
        }

        private void SetupJobService()
        {
            _jobService = new Mock<IJobService>();
            _jobService.Setup(x => x.SendUpdateStatusEmail(It.IsAny<int>(), It.IsAny<JobStatuses>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
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

        }

        [Test]
        public async Task WhenSuccessfullyChangingJobStatusToInProgress_ReturnsTrue()
        {
            _success = true;
            _request = new PutUpdateJobStatusToInProgressRequest
            {
                CreatedByUserID = 1,
                JobID = 1,
                VolunteerUserID = 2
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            
            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public async Task WhenUnSuccessfullyChangingJobStatusToInProgress_ReturnsFalse()
        {
            _success = false;
            _request = new PutUpdateJobStatusToInProgressRequest
            {
                CreatedByUserID = 1,
                JobID = 1,
                VolunteerUserID = 2
            };
            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(false, response.Success);
        }
    }
}