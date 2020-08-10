using HelpMyStreet.Contracts.AddressService.Response;
using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Utils;

namespace RequestService.UnitTests
{
    public class JobServiceTests
    {
        private MockRepository _mockRepository;
        Mock<IDistanceCalculator> _mockDistanceCalculator;
        Mock<IRepository> _repository;
        Mock<IGroupService> _groupService;
        private JobService _classUnderTest;
        private double _distance;
        private List<LatitudeAndLongitudeDTO> _getPostcodeCoordinatesResponse;
        private GetJobDetailsResponse _jobDetails;
        private GetUserRolesResponse _getUserRolesResponse;
        private int? _refferingGroupID;
        private GetJobDetailsResponse _getjobdetailsResponse;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            SetupRepository();
            SetupDistanceCalculator();
            SetupGroupService();
            _classUnderTest = new JobService(_mockDistanceCalculator.Object, _repository.Object, _groupService.Object);
        }

        private void SetupRepository()
        {
            _repository = _mockRepository.Create<IRepository>();
            _repository.Setup(x => x.GetLatitudeAndLongitudes(It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(() => _getPostcodeCoordinatesResponse);
            _repository.Setup(x => x.GetJobDetails(It.IsAny<int>())).Returns(() => _jobDetails);
            _repository.Setup(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _refferingGroupID);
            _repository.Setup(x => x.GetJobDetails(It.IsAny<int>()))
                .Returns(() => _getjobdetailsResponse);
        }

        private void SetupDistanceCalculator()
        {
            _mockDistanceCalculator = _mockRepository.Create<IDistanceCalculator>();
            _mockDistanceCalculator.Setup(x => x.GetDistanceInMiles(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .Returns(() => _distance);
        }

        private void SetupGroupService()
        {
            _groupService = _mockRepository.Create<IGroupService>();
            _groupService.Setup(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => _getUserRolesResponse);
        }


        [TearDown]
        public void VerifyAndTearDown()
        {
            //_mockRepository.VerifyAll();
        }

        [Test]
        public async Task WhenPasscodesAreKnownToAddressService_ReturnJobSummaryWithDistance()
        {
            _distance = 50d;
            List<LatitudeAndLongitudeDTO> postcodeCoordinates = new List<LatitudeAndLongitudeDTO>();
            postcodeCoordinates.Add(new LatitudeAndLongitudeDTO
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode"
            });
            postcodeCoordinates.Add(new LatitudeAndLongitudeDTO
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode2"
            });
            _getPostcodeCoordinatesResponse = postcodeCoordinates;


            string postCode = "PostCode2";
            List<JobSummary> jobSummaries = new List<JobSummary>();
            jobSummaries.Add(new JobSummary()
            {
                JobID = 1,
                PostCode = "PostCode"
            });
            var response = await _classUnderTest.AttachedDistanceToJobSummaries(postCode, jobSummaries, CancellationToken.None);

        }

        [Test]
        public async Task WhenVolunteerPasscodesIsNotKnownToAddressService_ReturnNull()
        {
            _distance = 50d;
            List<LatitudeAndLongitudeDTO> postcodeCoordinates = new List<LatitudeAndLongitudeDTO>();
            postcodeCoordinates.Add(new LatitudeAndLongitudeDTO()
            {
                Latitude = 1d,
                Longitude = 2,
                Postcode = "PostCode"
            });
            _getPostcodeCoordinatesResponse = postcodeCoordinates;

            string postCode = "PostCode2";
            List<JobSummary> jobSummaries = new List<JobSummary>();
            jobSummaries.Add(new JobSummary()
            {
                JobID = 1,
                PostCode = "PostCode"
            });
            var response = await _classUnderTest.AttachedDistanceToJobSummaries(postCode, jobSummaries, CancellationToken.None);
            _mockDistanceCalculator.Verify(v => v.GetDistanceInMiles(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Never);
        }

        [Test]
        public async Task WhenVolunteerIsDiffentToCreatedByButUserIsTaskAdmin_ReturnsTrue()
        {
            int jobId = 1;
            int createdByUserID = 1;
            _refferingGroupID = 1;
            _getjobdetailsResponse = new GetJobDetailsResponse()
            {
                VolunteerUserID = 2
            };

            Dictionary<int, List<int>> roles = new Dictionary<int, List<int>>();
            roles.Add(1, new List<int>() { (int) GroupRoles.TaskAdmin });

            _getUserRolesResponse = new GetUserRolesResponse()
            {
                UserGroupRoles = roles
            };
            var response = await _classUnderTest.HasPermissionToChangeStatusAsync(jobId, createdByUserID, CancellationToken.None);

            _repository.Verify(x => x.GetJobDetails(It.IsAny<int>()), Times.Once);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _groupService.Verify(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.AreEqual(true, response);
        }

        [Test]
        public async Task WhenVolunteerIsSameAsCreatedBy_ReturnsTrue()
        {
            int jobId = 1;
            int createdByUserID = 1;
            _refferingGroupID = 1;
            _getjobdetailsResponse = new GetJobDetailsResponse()
            {
                VolunteerUserID = 1
            };

            Dictionary<int, List<int>> roles = new Dictionary<int, List<int>>();
            roles.Add(1, new List<int>() { (int)GroupRoles.Member });

            _getUserRolesResponse = new GetUserRolesResponse()
            {
                UserGroupRoles = roles
            };
            var response = await _classUnderTest.HasPermissionToChangeStatusAsync(jobId, createdByUserID, CancellationToken.None);
            _repository.Verify(x => x.GetJobDetails(It.IsAny<int>()), Times.Once);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _groupService.Verify(x=> x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);

            Assert.AreEqual(true, response);
        }


        [Test]
        public async Task WhenVolunteerIsDiffentToCreatedByButUserIsNotTaskAdmin_ReturnsFalse()
        {
            int jobId = 1;
            int createdByUserID = 1;
            _refferingGroupID = 1;
            _getjobdetailsResponse = new GetJobDetailsResponse()
            {
                VolunteerUserID = 2
            };

            Dictionary<int, List<int>> roles = new Dictionary<int, List<int>>();
            roles.Add(1, new List<int>() { (int)GroupRoles.Member });

            _getUserRolesResponse = new GetUserRolesResponse()
            {
                UserGroupRoles = roles
            };
            var response = await _classUnderTest.HasPermissionToChangeStatusAsync(jobId, createdByUserID, CancellationToken.None);

            _repository.Verify(x => x.GetJobDetails(It.IsAny<int>()), Times.Once);
            _repository.Verify(x => x.GetReferringGroupIDForJobAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _groupService.Verify(x => x.GetUserRoles(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.AreEqual(false, response);
        }
    }
}