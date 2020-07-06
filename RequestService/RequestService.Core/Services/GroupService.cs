using HelpMyStreet.Contracts.GroupService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Contracts.Shared;
using HelpMyStreet.Utils.Utils;
using RequestService.Core.Config;
using RequestService.Core.Utils;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            var streamContent = HttpContentUtils.SerialiseToJsonAndCompress(request);

            ResponseWrapper<GetNewRequestActionsResponse, GroupServiceErrorCode> getNewRequestActionsResponseWithWrapper;
            using (HttpResponseMessage response = await _httpClientWrapper.GetAsync(HttpClientConfigName.GroupService, path, streamContent, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                Stream stream = await response.Content.ReadAsStreamAsync();
                getNewRequestActionsResponseWithWrapper = await Utf8Json.JsonSerializer.DeserializeAsync<ResponseWrapper<GetNewRequestActionsResponse, GroupServiceErrorCode>>(stream, StandardResolver.AllowPrivate);
            }

            if (!getNewRequestActionsResponseWithWrapper.IsSuccessful)
            {
                throw new Exception($"Calling Group Service GetNewRequestActions endpoint unsuccessful: {getNewRequestActionsResponseWithWrapper.Errors.FirstOrDefault()?.ErrorMessage}");
            }

            return getNewRequestActionsResponseWithWrapper.Content;

            throw new NotImplementedException();
        }
    }
}
