using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class Request
    {
        public Request()
        {
            SupportActivities = new HashSet<SupportActivities>();
        }

        public int Id { get; set; }
        public string PostCode { get; set; }
        public DateTime DateRequested { get; set; }
        public bool IsFulfillable { get; set; }
        public bool CommunicationSent { get; set; }

        public byte? FulfillableStatus { get; set; }

        public virtual PersonalDetails PersonalDetails { get; set; }
        public virtual ICollection<SupportActivities> SupportActivities { get; set; }
    }
}
