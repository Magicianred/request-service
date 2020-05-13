
using RequestService.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Services;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using System.Text.RegularExpressions;
using RequestService.Core.Domains.Entities;
using HelpMyStreet.Contracts.ReportService.Response;

namespace RequestService.Handlers
{
    public class GetReportHandler : IRequestHandler<GetReportRequest, GetReportResponse>
    {
        private readonly IRepository _repository;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        public GetReportHandler(IRepository repository, IUserService userService, IAddressService addressService)
        {
            _repository = repository;
            _userService = userService;
            _addressService = addressService;
        }

        public Task<GetReportResponse> Handle(GetReportRequest request, CancellationToken cancellationToken)
        {
            List<ReportItem> reportItems = _repository.GetDailyReport();

            return Task.FromResult(new GetReportResponse()
            {
                ReportItems = reportItems
            });
        }
    }
}
