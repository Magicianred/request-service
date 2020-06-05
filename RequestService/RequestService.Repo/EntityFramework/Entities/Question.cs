using System.Collections.Generic;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class Question
    {
        public Question()
        {
            ActivityQuestions = new HashSet<ActivityQuestions>();
            RequestQuestions = new HashSet<RequestQuestions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte QuestionType { get; set; }
        public bool Required { get; set; }        
        public string AdditionalData { get; set; }
        public virtual ICollection<ActivityQuestions> ActivityQuestions { get; set; }
        public virtual ICollection<RequestQuestions> RequestQuestions { get; set; }
    }
}

