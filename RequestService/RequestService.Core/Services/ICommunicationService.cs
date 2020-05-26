using HelpMyStreet.Contracts.CommunicationService.Request;
using RequestService.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface ICommunicationService
    {
        Task<bool> SendEmailToUsersAsync(SendEmailToUsersRequest request, CancellationToken cancellationToken);

        Task<bool> SendEmail(SendEmailRequest request, CancellationToken cancellationToken);

        Task<bool> SendEmailToUserAsync(SendEmailToUserRequest request, CancellationToken cancellationToken);

    }
}
