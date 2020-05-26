using HelpMyStreet.Contracts.Shared;
using HelpMyStreet.Contracts.UserService.Request;
using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Enums;
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

        public async Task<GetHelpersByPostcodeResponse> GetHelpersByPostCodeAsync(string postcode, CancellationToken cancellationToken)
        {
            string path = $"api/GetHelpersByPostCode?postcode={postcode}";
            GetHelpersByPostcodeResponse helpersResponse;
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                helpersResponse = JsonConvert.DeserializeObject<GetHelpersByPostcodeResponse>(content);
            }
            return helpersResponse;
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

        public async Task<GetVolunteersByPostcodeAndActivityResponse> GetVolunteersByPostcodeAndActivityAsync(string postcode, SupportActivities activity, CancellationToken cancellationToken)
        {
            string path = $"api/GetVolunteersByPostcodeAndActivity";

            GetVolunteersByPostcodeAndActivityRequest request = new GetVolunteersByPostcodeAndActivityRequest()
            {
                VolunteerFilter = new VolunteerFilter()
                {
                    Postcode = postcode,
                    Activity = activity
                }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await _httpClientWrapper.PostAsync(HttpClientConfigName.UserService, path, jsonContent, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var getVolunteersByPostcodeAndActivityResponse = JsonConvert.DeserializeObject<GetVolunteersByPostcodeAndActivityResponse>(jsonResponse);
                
                return getVolunteersByPostcodeAndActivityResponse;
            }
        }
    }
}
