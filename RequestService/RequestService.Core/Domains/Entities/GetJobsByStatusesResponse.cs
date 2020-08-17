using HelpMyStreet.Utils.Models;
using System.Collections.Generic;

namespace RequestService.Core.Domains.Entities
{
    public class GetJobsByStatusesResponse
    {
        public List<JobSummary> JobSummaries { get; set; }
    }
}
