using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class JobStatus
    {
        public JobStatus()
        {
            RequestJobStatus = new HashSet<RequestJobStatus>();
        }

        public byte Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<RequestJobStatus> RequestJobStatus { get; set; }
        public virtual ICollection<Job> Job { get; set; }
    }
}
