using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Repo.EntityFramework.Entities
{
    public class PersonalDetails
    {
        public int RequestId { get; set; }
        public bool OnBehalfOfAnother { get; set; }
        public string FurtherDetails { get; set; }
        public string RequestorFirstName { get; set; }
        public string RequestorLastName { get; set; }
        public string RequestorEmailAddress { get; set; }
        public string RequestorPhoneNumber { get; set; }

        public virtual Request Request { get; set; }
    }
}
