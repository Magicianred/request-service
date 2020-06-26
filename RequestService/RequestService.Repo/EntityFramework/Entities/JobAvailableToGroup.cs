using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public partial class JobAvailableToGroup
    {
        public int JobId { get; set; }
        public int GroupId { get; set; }        
        public virtual Job Job { get; set; }
    }
}
