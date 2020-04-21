using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class SupportActivityDTO
    {
        public List<HelpMyStreet.Utils.Enums.SupportActivities> SupportActivities { get; set; }
        public int RequestId { get; set; }            
    }

}
