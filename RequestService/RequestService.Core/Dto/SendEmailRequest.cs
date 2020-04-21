using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class SendEmailRequest
    {
        public string ToAddress { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string BodyHTML { get; set; }
        public string BodyText { get; set; }

    }
}
