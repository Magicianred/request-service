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
    public class GetJobDetailsHandlerTests
    {
        private Mock<IRepository> _repository;
        private GetJobDetailsHandler _classUnderTest;
        private GetJobDetailsRequest _request;
        private GetJobDetailsResponse _response;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            _classUnderTest = new GetJobDetailsHandler(_repository.Object);
            _response = new GetJobDetailsResponse()
            {
                Details = "DETAILS",
                JobID = 1,
                
            };
        }

        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.GetJobDetails(It.IsAny<int>())).Returns(()=>_response);
        }

        [Test]
        public async Task WhenPassesInKnownJobID_ReturnsDetails()
        {
            _request = new GetJobDetailsRequest
            {
                JobID = 1
            };

            var response = await _classUnderTest.Handle(_request, CancellationToken.None);
            Assert.AreEqual(_request.JobID, response.JobID);
            Assert.AreEqual(_response, response);
        }

    }
}