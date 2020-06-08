using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public partial class JobQuestions
    {
        public int JobId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public virtual Question Question { get; set; }
        public virtual Job Job { get; set; }
        
    }
}


