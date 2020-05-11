using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;

namespace RequestService.Handlers
{
    public class GetJobDetailsHandler : IRequestHandler<GetJobDetailsRequest, GetJobDetailsResponse>
    {
        private readonly IRepository _repository;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;
        public GetJobDetailsHandler(IRepository repository, IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _applicationConfig = applicationConfig;
        }

        public async Task<GetJobDetailsResponse> Handle(GetJobDetailsRequest request, CancellationToken cancellationToken)
        {
            HelpRequest helpRequest = new HelpRequest()
            {
                OtherDetails = "Other Details",
                Recipient = new RequestPersonalDetails()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    EmailAddress = "John@Smith.com",
                    Address = new Address()
                    {
                        AddressLine1 = "40 Friar Lane",
                        AddressLine2 = "Nottingham",
                        AddressLine3 = "Nottinghamshire",
                        Locality = "Locality",
                        Postcode = "NG1 6DQ"
                    },
                    MobileNumber = "07765 432 421",
                    OtherNumber = "01332 675 453"
                },
                Requestor = new RequestPersonalDetails()
                {
                    FirstName = "Harold",
                    LastName = "James",
                    EmailAddress = "harold@james.com",
                    Address = new Address()
                    {
                        AddressLine1 = "36 Friar Lane",
                        AddressLine2 = "Nottingham",
                        AddressLine3 = "Nottinghamshire",
                        Locality = "Locality",
                        Postcode = "NG1 6DQ"
                    },
                    MobileNumber = "07765 432 421",
                    OtherNumber = "01332 675 453"
                },
                ConsentForContact = true,
                ReadPrivacyNotice = true,
                AcceptedTerms = true,
                SpecialCommunicationNeeds = "None",
                ForRequestor = false
            };

            Job job = new Job()
            {
                VolunteerUserID = 1,
                JobStatus = JobStatuses.Open,
                UniqueIdentifier = Guid.NewGuid(),
                HealthCritical = true,
                Details = "Job Details",
                DueDays = 5,
                SupportActivity = SupportActivities.DogWalking,
                JobID = 1
            };

            GetJobDetailsResponse result = new GetJobDetailsResponse()
            {
                HelpRequest = helpRequest,
                Job = job
            };
            return result;
        }
    }
}
