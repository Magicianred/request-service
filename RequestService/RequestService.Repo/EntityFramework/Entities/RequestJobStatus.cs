using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class RequestJobStatus
    {
        public int JobId { get; set; }
        public byte JobStatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? VolunteerUserId { get; set; }
        public int? CreatedByUserId { get; set; }

        public virtual Job Job { get; set; }
        public virtual JobStatus JobStatus { get; set; }
    }
}
