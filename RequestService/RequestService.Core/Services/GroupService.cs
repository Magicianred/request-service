using HelpMyStreet.Contracts.GroupService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.Shared;
using HelpMyStreet.Utils.Utils;
using Newtonsoft.Json;
using RequestService.Core.Config;
using RequestService.Core.Utils;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json.Resolvers;

namespace RequestService.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        public GroupService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<GetNewRequestActionsResponse> GetNewRequestActions(GetNewRequestActionsRequest request, CancellationToken cancellationToken)
        {
            string path = $"api/GetNewRequestActions";

            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.GroupService, path, request, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetNewRequestActionsResponse, GroupServiceErrorCode>>(content);
                if (jsonResponse.IsSuccessful)
                {
                    return jsonResponse.Content;
                }
            }
            throw new Exception("Unable to get new request actions");
        }

        public async Task<GetUserGroupsResponse> GetUserGroups(int userId, CancellationToken cancellationToken)
        {
            string path = $"api/GetUserGroups?UserID={userId}";

            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.GroupService, path, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetUserGroupsResponse, GroupServiceErrorCode>>(content);
                if (jsonResponse.IsSuccessful)
                {
                    return jsonResponse.Content;
                }
            }
            throw new Exception("Unable to get user groups");
        }
    }
}