using HelpMyStreet.Contracts.ReportService.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Domains.Entities
{
    public class GetReportRequest : IRequest<GetReportResponse>
    {
    }
}
