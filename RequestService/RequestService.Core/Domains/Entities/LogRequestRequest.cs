using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Domains.Entities
{
    public class LogRequestRequest : IRequest<LogRequestResponse>
    {
        public string PostCode { get; set; }
    }
}
