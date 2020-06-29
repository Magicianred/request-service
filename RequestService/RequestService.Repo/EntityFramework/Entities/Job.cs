using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class Job
    {
        public Job()
        {
            RequestJobStatus = new HashSet<RequestJobStatus>();
            JobQuestions = new HashSet<JobQuestions>();
            JobAvailableToGroup = new HashSet<JobAvailableToGroup>();
        }

        public int? VolunteerUserId { get; set; }
        public byte? JobStatusId { get; set; }
        public int Id { get; set; }
        public int RequestId { get; set; }
        public byte SupportActivityId { get; set; }
        public string Details { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsHealthCritical { get; set; }
        public virtual Request NewRequest { get; set; }
        public virtual ICollection<JobQuestions> JobQuestions { get; set; }
        public virtual ICollection<RequestJobStatus> RequestJobStatus { get; set; }
        public virtual ICollection<JobAvailableToGroup> JobAvailableToGroup { get; set; }
    }
}
