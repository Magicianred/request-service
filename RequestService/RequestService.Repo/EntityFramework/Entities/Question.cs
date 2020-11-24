using System.Collections.Generic;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class Question
    {
        public Question()
        {
            ActivityQuestions = new HashSet<ActivityQuestions>();
            JobQuestions = new HashSet<JobQuestions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte QuestionType { get; set; }        
        public string AdditionalData { get; set; }
        public bool AnswerContainsSensitiveData { get; set; }
        public virtual ICollection<ActivityQuestions> ActivityQuestions { get; set; }
        public virtual ICollection<JobQuestions> JobQuestions { get; set; }
    }
}

