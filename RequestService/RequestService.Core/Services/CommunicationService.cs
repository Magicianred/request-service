using Newtonsoft.Json;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public class CommunicationService: ICommunicationService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public CommunicationService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }     

        public async Task<bool> SendEmailToUsersAsync(SendEmailToUsersRequest request,  CancellationToken cancellationToken)
        {
            string path = $"api/SendEmailToUsers";
            
            var jsonContent =  new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await _httpClientWrapper.PostAsync(HttpClientConfigName.CommunicationService, path, jsonContent, cancellationToken).ConfigureAwait(false)){
                response.EnsureSuccessStatusCode();
                return true;
            }      
        }
 

    }
}
