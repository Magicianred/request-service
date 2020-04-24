using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HelpMyStreet.Contracts.AddressService.Response;
namespace RequestService.Core.Services
{
    public interface IAddressService
    {
        Task<bool> IsValidPostcode(string postcode,  CancellationToken cancellationToken);
    }
}
