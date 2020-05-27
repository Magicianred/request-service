using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface IDailyDigestService
    {
        Task SendDailyDigestEmailAsync(CancellationToken cancellationToken);
    }
}
