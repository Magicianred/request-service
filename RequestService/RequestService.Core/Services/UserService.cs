using HelpMyStreet.Contracts.Shared;
using HelpMyStreet.Contracts.UserService.Request;
using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Utils;
using Marvin.StreamExtensions;
using Newtonsoft.Json;
using RequestService.Core.Dto;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var getChampionCountByPostcodeResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetChampionCountByPostcodeResponse, UserServiceErrorCode>>(jsonResponse);

                if (getChampionCountByPostcodeResponse.HasContent && getChampionCountByPostcodeResponse.IsSuccessful)
                {
                    return getChampionCountByPostcodeResponse.Content.Count;
                }
                else
                {
                    throw new System.Exception(getChampionCountByPostcodeResponse.Errors.ToString());
                }
            }
        }

        public async Task<GetChampionsByPostcodeResponse> GetChampionsByPostcode(string postcode, CancellationToken cancellationToken)
        {
            string path = $"api/GetChampionsByPostcode?postcode={postcode}";
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var championsResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetChampionsByPostcodeResponse, UserServiceErrorCode>>(jsonResponse);

                if (championsResponse.HasContent && championsResponse.IsSuccessful)
                {
                    return championsResponse.Content;
                }
                else
                {
                    throw new System.Exception(championsResponse.Errors.ToString());
                }
            }
        }

        public async Task<GetVolunteersByPostcodeAndActivityResponse> GetHelpersByPostcodeAndTaskType(string postcode, List<SupportActivities> activities, CancellationToken cancellationToken)
        {
            string path = $"api/GetVolunteersByPostcodeAndActivity";
            GetVolunteersByPostcodeAndActivityRequest request = new GetVolunteersByPostcodeAndActivityRequest
            {
                VolunteerFilter = new VolunteerFilter
                {
                    Postcode = postcode,
                    Activities = activities
                }                                
            };

            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, request, cancellationToken).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var helperResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetVolunteersByPostcodeAndActivityResponse, UserServiceErrorCode>>(jsonResponse);

                if (helperResponse.HasContent && helperResponse.IsSuccessful)
                {
                    return helperResponse.Content;
                }
                else
                {
                    throw new System.Exception(helperResponse.Errors.ToString());
                }
            }
        }

        public async Task<GetUserByIDResponse> GetUser(int userID, CancellationToken cancellationToken)
        {
            string path = $"api/GetUserByID?ID={userID}";
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var userIDResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetUserByIDResponse, UserServiceErrorCode>>(jsonResponse);

                if (userIDResponse.HasContent && userIDResponse.IsSuccessful)
                {
                    return userIDResponse.Content;
                }
                else
                {
                    throw new System.Exception(userIDResponse.Errors.ToString());
                }
            }
        }

        public async Task<GetUsersResponse> GetUsers(CancellationToken cancellationToken)
        {
            string path = $"api/GetUsers";
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.UserService, path, cancellationToken).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var usersResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetUsersResponse, UserServiceErrorCode>>(jsonResponse);

                if (usersResponse.HasContent && usersResponse.IsSuccessful)
                {
                    return usersResponse.Content;
                }
                else
                {
                    throw new System.Exception(usersResponse.Errors.ToString());
                }
            }
        }
    }
}
