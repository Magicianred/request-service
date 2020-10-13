using HelpMyStreet.Contracts.GroupService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.Shared;
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Utils;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        public GroupService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<GetGroupActivityCredentialsResponse> GetGroupActivityCredentials(GetGroupActivityCredentialsRequest request)
        {
            string path = $"/api/GetGroupActivityCredentials";
            string absolutePath = $"{path}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            
            using (HttpResponseMessage response = await _httpClientWrapper.PostAsync(HttpClientConfigName.GroupService, absolutePath, jsonContent, CancellationToken.None).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var getJobsResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetGroupActivityCredentialsResponse, GroupServiceErrorCode>>(jsonResponse);
                if (getJobsResponse.HasContent && getJobsResponse.IsSuccessful)
                {
                    return getJobsResponse.Content;
                }
                return null;
            }
        }

        public async Task<GetGroupMemberResponse> GetGroupMember(GetGroupMemberRequest request)
        {
            string path = $"/api/GetGroupMember?groupID={request.GroupId}&userId={request.UserId}&authorisingUserId={request.AuthorisingUserId}";
            string absolutePath = $"{path}";
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.GroupService, absolutePath, CancellationToken.None).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var getJobsResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetGroupMemberResponse, GroupServiceErrorCode>>(jsonResponse);
                if (getJobsResponse.HasContent && getJobsResponse.IsSuccessful)
                {
                    return getJobsResponse.Content;
                }
                return null;
            }
        }

        public async Task<GetGroupMemberDetailsResponse> GetGroupMemberDetails(GetGroupMemberDetailsRequest request)
        {
            string path = $"/api/GetGroupMemberDetails?groupID={request.GroupId}&userId={request.UserId}&authorisingUserId={request.AuthorisingUserId}";
            string absolutePath = $"{path}";
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.GroupService, absolutePath, CancellationToken.None).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var getJobsResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetGroupMemberDetailsResponse, GroupServiceErrorCode>>(jsonResponse);
                if (getJobsResponse.HasContent && getJobsResponse.IsSuccessful)
                {
                    return getJobsResponse.Content;
                }
                return null;
            }
        }

        public async Task<GetGroupMembersResponse> GetGroupMembers(int groupID)
        {
            string path = $"/api/GetGroupMembers?groupID=" + groupID;
            string absolutePath = $"{path}";
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.GroupService, absolutePath, CancellationToken.None).ConfigureAwait(false))
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var getJobsResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetGroupMembersResponse, GroupServiceErrorCode>>(jsonResponse);
                if (getJobsResponse.HasContent && getJobsResponse.IsSuccessful)
                {
                    return getJobsResponse.Content;
                }
                return null;
            }
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

        public async Task<GetUserRolesResponse> GetUserRoles(int userId, CancellationToken cancellationToken)
        {
            string path = $"api/GetUserRoles?UserID={userId}";

            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.GroupService, path, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<ResponseWrapper<GetUserRolesResponse, GroupServiceErrorCode>>(content);
                if (jsonResponse.IsSuccessful)
                {
                    return jsonResponse.Content;
                }
            }
            throw new Exception("Unable to get user roles");
        }

        public async Task<PostAssignRoleResponse> PostAssignRole(PostAssignRoleRequest postAssignRoleRequest, CancellationToken cancellationToken)
        {
            string path = $"api/PostAssignRole";

            string json = JsonConvert.SerializeObject(postAssignRoleRequest);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await _httpClientWrapper.PostAsync(HttpClientConfigName.GroupService, path, data, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<ResponseWrapper<PostAssignRoleResponse, GroupServiceErrorCode>>(content);
                if (jsonResponse.IsSuccessful)
                {
                    return jsonResponse.Content;
                }
            }
            throw new Exception("Unable to assign role");
        }
    }
}