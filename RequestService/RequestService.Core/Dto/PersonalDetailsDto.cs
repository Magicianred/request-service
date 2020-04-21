using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class PersonalDetailsDto
    {
        public int RequestID { get; set; }
        public bool OnBehalfOfAnother { get; set; }
        public string FurtherDetails { get; set; }
        public string RequestorFirstName { get; set; }
        public string RequestorLastName { get; set; }
        public string RequestorEmailAddress { get; set; }
        public string RequestorPhoneNumber { get; set; }
    }
}
