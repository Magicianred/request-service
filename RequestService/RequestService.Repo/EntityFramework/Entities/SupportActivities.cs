using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class SupportActivities
    {
        public int RequestId { get; set; }
        public int ActivityId { get; set; }
        public virtual Request Request { get; set; }
    }
}
