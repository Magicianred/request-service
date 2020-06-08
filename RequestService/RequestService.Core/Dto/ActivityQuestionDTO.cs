using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class ActivityQuestionDTO
    {
        public List<Question> Questions { get; set; } = new List<Question>();
        public SupportActivities Activity { get; set; }
    }
}
