using Moq;
using NUnit.Framework;
using RequestService.Core.Domains.Entities;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class LogRequestHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<IUserService> _userService;
        private LogRequestRequest _request;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.CreateRequestAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _userService = new Mock<IUserService>();
            _request = new LogRequestRequest
            {
                Postcode = "TE5T1NG"
            };
        }

        [Test]
        public async Task WhenICall_LogRequestHandler_WithChampions_IGetFullfilable_EqualsTrue()
        {
            _userService.Setup(x => x.GetChampionCountByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);

            LogRequestHandler handler = new LogRequestHandler(_repository.Object, _userService.Object);
            var response = await handler.Handle(_request, new CancellationToken());
            Assert.AreEqual(true, response.Fulfillable);
            Assert.AreEqual(1, response.RequestID);
        }

        [Test]
        public async Task WhenICall_LogRequestHandler_WithNoChampions_IGetFullfilable_EqualsFalse()
        {
            _userService.Setup(x => x.GetChampionCountByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(0);
            LogRequestHandler handler = new LogRequestHandler(_repository.Object, _userService.Object);
            var response = await handler.Handle(_request, new CancellationToken());
            Assert.AreEqual(false, response.Fulfillable);
            Assert.AreEqual(1, response.RequestID);
        }

        [Test]
        public async Task WhenICall_LogRequestHandler_WithLowercasePostCode_ItFormatsToUpperCase()
        {
            _userService.Setup(x => x.GetChampionCountByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(0);
            _request = new LogRequestRequest
            {
                Postcode = "te5T1ng"
            };
            LogRequestHandler handler = new LogRequestHandler(_repository.Object, _userService.Object);
            var response = await handler.Handle(_request, new CancellationToken());
            _repository.Verify(x =>  x.CreateRequestAsync("TE5T 1NG", It.IsAny<CancellationToken>()));         
        }
    }
}