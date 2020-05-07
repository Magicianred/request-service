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
            HelpMyStreet.Utils.Models.Job job = new HelpMyStreet.Utils.Models.Job()
            {
                VolunteerUserID = 1,
                JobStatus = JobStatuses.Open,
                UniqueIdentifier = Guid.NewGuid(),
                Critical = true,
                Details = "Job Details",
                DueDays = 5,
                SupportActivity = SupportActivities.DogWalking,
                Recipient = new HelpMyStreet.Utils.Models.JobPersonalDetails()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    EmailAddress = "John@Smith.com",
                    ContactNumbers = new HelpMyStreet.Utils.Models.ContactNumber()
                    {
                        ContactNumbers = new List<string>()
                        {
                            "07897 565 4321",
                            "01332 365 543"
                        }
                    },
                    Address = new HelpMyStreet.Utils.Models.Address()
                    {
                        AddressLine1 = "40 Friar Lane",
                        AddressLine2 = "Nottingham",
                        AddressLine3 = "Nottinghamshire",
                        Locality = "Locality",
                        Postcode = "NG1 6DQ"
                    }
                },
                Requestor = new HelpMyStreet.Utils.Models.JobPersonalDetails()
                {
                    FirstName = "Harold",
                    LastName = "Jones",
                    EmailAddress = "Harold@Jones.com",
                    ContactNumbers = new HelpMyStreet.Utils.Models.ContactNumber()
                    {
                        ContactNumbers = new List<string>()
                        {
                            "07897 565 4321",
                            "01332 365 543"
                        }
                    },
                    Address = new HelpMyStreet.Utils.Models.Address()
                    {
                        AddressLine1 = "30 Friar Lane",
                        AddressLine2 = "Nottingham",
                        AddressLine3 = "Nottinghamshire",
                        Locality = "Locality",
                        Postcode = "NG1 6DQ"
                    }
                }
            };

            GetJobDetailsResponse result = new GetJobDetailsResponse()
            {
                Job = job
            };
            return result;
        }
    }
}
