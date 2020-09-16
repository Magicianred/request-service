using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HelpMyStreet.Utils.Enums;

namespace RequestService.UnitTests
{
    public class GetJobsByFilterHandlerTests
    {
        private Mock<IRepository> _repository;
        private Mock<IJobFilteringService> _jobFilteringService;
        private Mock<IAddressService> _addressService;
        private GetJobsByFilterHandler _classUnderTest;
        private GetJobsByFilterRequest _request;
        private GetJobsByFilterResponse _response;
        private List<JobHeader> _jobHeaders;
        private MockRepository _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            SetupRepository();
            SetUpJobFilteringService();
            SetupAddressService();

            _jobHeaders = new List<JobHeader>();
            _jobHeaders.Add(new JobHeader
            {
                JobID = 1,
                DistanceInMiles = 25d,
                SupportActivity = SupportActivities.CheckingIn
            });
            _jobHeaders.Add(new JobHeader
            {
                JobID = 1,
                DistanceInMiles = 15d,
                SupportActivity = SupportActivities.CollectingPrescriptions
            });
            _jobHeaders.Add(new JobHeader
            {
                JobID = 1,
                DistanceInMiles = 20d,
                SupportActivity = SupportActivities.FaceMask
            });
            _jobHeaders.Add(new JobHeader
            {
                JobID = 1,
                DistanceInMiles = 0d,
                SupportActivity = SupportActivities.Errands
            });
            _jobHeaders.Add(new JobHeader
            {
                JobID = 1,
                DistanceInMiles = 30d,
                SupportActivity = SupportActivities.Errands
            });

            _response = new GetJobsByFilterResponse()
            {
                JobHeaders = _jobHeaders
            };

            _classUnderTest = new GetJobsByFilterHandler(_repository.Object, _addressService.Object, _jobFilteringService.Object);
        }

        private void SetUpJobFilteringService()
        {
            _jobFilteringService = _mockRepository.Create<IJobFilteringService>();
            //_jobFilteringService.Setup(x => x.FilterJobSummaries(It.IsAny<List<JobSummary>>(), It.IsAny<int?>(), It.IsAny<List<SupportActivities>>(), It.IsAny<string>(), It.IsAny<double?>(), It.IsAny<Dictionary<SupportActivities, double?>>(), null, null, null, It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(() => _jobHeaders);
        }

        private void SetupRepository()
        {
            _repository = _mockRepository.Create<IRepository>();
            //_repository.Setup(x => x.GetOpenJobsSummaries())
            //    .Returns(() => _jobHeaders);
        }

        private void SetupAddressService()
        {
            _addressService = _mockRepository.Create<IAddressService>();
            _addressService.Setup(x => x.IsValidPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }

        [TearDown]
        public void VerifyAndTearDown()
        {
            _mockRepository.VerifyAll();
        }
    }
}