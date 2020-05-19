using HelpMyStreet.Utils.Models;
using Marvin.StreamExtensions;
using Newtonsoft.Json;
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

        public async Task<GetChampionsByPostcodeResponse> GetChampionsByPostcode(string postcode, CancellationToken cancellationToken)
        {
            string path = $"api/GetChampionsByPostcode?postcode={postcode}";
            GetChampionsByPostcodeResponse championsResponse;
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();                
                string content = await response.Content.ReadAsStringAsync();
                championsResponse = JsonConvert.DeserializeObject<GetChampionsByPostcodeResponse>(content);
            }
            return championsResponse;
        }

        public async Task<GetUserByIDResponse> GetUser(int userID, CancellationToken cancellationToken)
        {
            string path = $"api/GetUserByID?ID={userID}";
            GetUserByIDResponse userIDResponse;
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                userIDResponse = JsonConvert.DeserializeObject<GetUserByIDResponse>(content);
            }
            return userIDResponse;
        }
    }
}
