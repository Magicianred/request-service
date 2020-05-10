﻿using HelpMyStreet.Contracts.AddressService.Response;
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
using HelpMyStreet.Contracts.AddressService.Request;
using HelpMyStreet.Utils.Utils;
using Utf8Json.Resolvers;

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

        public async Task<GetPostcodeCoordinatesResponse> GetPostcodeCoordinatesAsync(GetPostcodeCoordinatesRequest getPostcodeCoordinatesRequest, CancellationToken cancellationToken)
        {
            string path = $"api/GetPostcodeCoordinates";

            var streamContent = HttpContentUtils.SerialiseToJsonAndCompress(getPostcodeCoordinatesRequest);

            ResponseWrapper<GetPostcodeCoordinatesResponse, AddressServiceErrorCode> getPostcodeCoordinatesResponseWithWrapper;
            using (HttpResponseMessage response = await _httpClientWrapper.PostAsync(HttpClientConfigName.AddressService, path, streamContent, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                Stream stream = await response.Content.ReadAsStreamAsync();
                getPostcodeCoordinatesResponseWithWrapper = await Utf8Json.JsonSerializer.DeserializeAsync<ResponseWrapper<GetPostcodeCoordinatesResponse, AddressServiceErrorCode>>(stream, StandardResolver.AllowPrivate);
            }

            if (!getPostcodeCoordinatesResponseWithWrapper.IsSuccessful)
            {
                throw new Exception($"Calling Address Service GetPostcodeCoordinatesAsync endpoint unsuccessful: {getPostcodeCoordinatesResponseWithWrapper.Errors.FirstOrDefault()?.ErrorMessage}");
            }

            return getPostcodeCoordinatesResponseWithWrapper.Content;
        }
    }
}
