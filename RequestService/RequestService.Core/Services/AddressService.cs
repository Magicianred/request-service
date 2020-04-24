using HelpMyStreet.Contracts.AddressService.Response;
using HelpMyStreet.Contracts.Shared;
using Marvin.StreamExtensions;
using Newtonsoft.Json;
using RequestService.Core.Config;
using RequestService.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        public AddressService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<bool> IsValidPostcode(string postcode, CancellationToken cancellationToken)
        {
            string path = $"api/getpostcode?postcode={postcode}";            
            ResponseWrapper<GetNearbyPostcodesResponse, AddressServiceErrorCode> nearbyPostcodeResponse;
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.AddressService, path, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                Stream stream = await response.Content.ReadAsStreamAsync();
                nearbyPostcodeResponse = stream.ReadAndDeserializeFromJson<ResponseWrapper<GetNearbyPostcodesResponse, AddressServiceErrorCode>>();
            }

            return nearbyPostcodeResponse.HasContent && nearbyPostcodeResponse.IsSuccessful;           
        }
    }
}
