using HelpMyStreet.Contracts.AddressService.Request;
using HelpMyStreet.Contracts.AddressService.Response;
using System.Threading;
using System.Threading.Tasks;
namespace RequestService.Core.Services
{
    public interface IAddressService
    {
        Task<bool> IsValidPostcode(string postcode,  CancellationToken cancellationToken);

        Task<GetPostcodeCoordinatesResponse> GetPostcodeCoordinatesAsync(GetPostcodeCoordinatesRequest getPostcodeCoordinatesRequest, CancellationToken cancellationToken);
    }
}
