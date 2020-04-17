using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Domains.Entities
{
    public class LogRequestResponse
    {
        public int RequestID { get; set; }
        public bool Fulfillable { get; set; }
    }
}
