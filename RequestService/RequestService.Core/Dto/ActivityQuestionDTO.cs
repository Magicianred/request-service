using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class ActivityQuestionDTO
    {
        public List<Question> Questions { get; set; }
        public SupportActivities Activity { get; set; }
    }
}
