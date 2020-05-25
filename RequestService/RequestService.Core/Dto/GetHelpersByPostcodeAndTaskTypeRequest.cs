using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class GetHelpersByPostcodeAndTaskTypeRequest 
    {
        public string Postcode { get; set; }
        public TasksRequested RequestedTasks { get; set; }
    }

    public class TasksRequested
    {
        public List<SupportActivities> SupportActivities { get; set; }
    }
}
