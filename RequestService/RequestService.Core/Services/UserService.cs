using Marvin.StreamExtensions;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public UserService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<int> GetChampionCountByPostcode(string postcode, CancellationToken cancellationToken)
        {
            string path = $"api/GetChampionCountByPostcode?postcode={postcode}";
            GetChampionCountByPostcodeResponse championCountResponse ;
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                Stream stream = await response.Content.ReadAsStreamAsync();
                championCountResponse = stream.ReadAndDeserializeFromJson<GetChampionCountByPostcodeResponse>();
            }
            return championCountResponse.Count;
        }
    }
}
