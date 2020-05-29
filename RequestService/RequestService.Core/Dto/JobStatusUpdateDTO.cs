using System;
using System.Collections.Generic;
using System.Text;
using HelpMyStreet.Utils.Enums;
namespace RequestService.Core.Dto
{
    public class JobStatusUpdateDTO
    {
        public bool ForRequestor { get; set; }

        public string RequestedFor { get; set; }

        public DateTime DateRequested { get; set; }
        
        public JobStatuses Statuses { get; set; }

        public SupportActivities SupportActivity { get; set; }

        public string CurrentTime { get; set; }
    }

}
