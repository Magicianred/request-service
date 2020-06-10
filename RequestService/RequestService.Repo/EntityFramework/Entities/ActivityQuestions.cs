using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public partial class ActivityQuestions
    {
        public int ActivityId { get; set; }
        public int QuestionId { get; set; }        
        public int Order { get; set; }
        public virtual Question Question { get; set; }
    }
}
