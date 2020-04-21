using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class SendEmailToUsersRequest
    {
        public Recipients Recipients { get; set; }
        public string Subject { get; set; }
        public string BodyHTML { get; set; }
        public string BodyText { get; set; }
    }

    public class Recipients
    {
        public List<int> ToUserID { get; set; }
        public List<int> CcUserIDs { get; set; }
        public List<int> BccUserIDs { get; set; }
    }
}
