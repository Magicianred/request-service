using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class SupportActivity
    {
        public SupportActivity()
        {
            Job = new HashSet<Job>();
        }

        public byte Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
