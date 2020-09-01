using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface IArchiveService
    {
       void ArchiveOldRequests(int daysSinceJobRequested, int daysSinceJobStatusChanged);
    }
}
