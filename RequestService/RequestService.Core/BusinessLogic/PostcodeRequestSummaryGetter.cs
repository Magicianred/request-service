using HelpMyStreet.Contracts.AddressService.Request;
using HelpMyStreet.Contracts.AddressService.Response;
using RequestService.Core.Dto;
using RequestService.Core.Extensions;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RequestService.Core.Config;

namespace RequestService.Core.BusinessLogic
{
    public class PostcodeRequestSummaryGetter : IPostcodeRequestSummaryGetter
    {
        private readonly IRepository _repository;
        private readonly IAddressService _addressService;
        private readonly IOptionsSnapshot<ApplicationConfig> _applicationConfig;

        public PostcodeRequestSummaryGetter(IRepository repository, IAddressService addressService, IOptionsSnapshot<ApplicationConfig> applicationConfig)
        {
            _repository = repository;
            _addressService = addressService;
            _applicationConfig = applicationConfig;
        }

        public async Task<IEnumerable<PostcodeRequestSummaryDto>> GetRequestPostcodeSummariesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<PostcodeWithNumberOfRequestsDto> postcodesWithRequestNumbers = await _repository.GetNumberOfRequestsPerPostcode();

            IEnumerable<IEnumerable<string>> postcodeChunks = postcodesWithRequestNumbers.Select(x => x.Postcode).Chunk(_applicationConfig.Value.CoordinatesBatchSize); 

            List<Task<GetPostcodeCoordinatesResponse>> postcodeCoordinateTasks = new List<Task<GetPostcodeCoordinatesResponse>>();

            foreach (IEnumerable<string> postcodeChunk in postcodeChunks)
            {
                GetPostcodeCoordinatesRequest getPostcodeCoordinatesRequest = new GetPostcodeCoordinatesRequest()
                {
                    Postcodes = postcodeChunk
                };

                Task<GetPostcodeCoordinatesResponse> postcodeCoordinatesTask = _addressService.GetPostcodeCoordinatesAsync(getPostcodeCoordinatesRequest, cancellationToken);
                postcodeCoordinateTasks.Add(postcodeCoordinatesTask);
            }

            List<PostcodeCoordinate> postcodeCoordinates = new List<PostcodeCoordinate>();

            while (postcodeCoordinateTasks.Count > 0)
            {
                Task<GetPostcodeCoordinatesResponse> finishedTask = await Task.WhenAny(postcodeCoordinateTasks);
                postcodeCoordinateTasks.Remove(finishedTask);

                GetPostcodeCoordinatesResponse coordinatesBatch = await finishedTask;

                postcodeCoordinates.AddRange(coordinatesBatch.PostcodeCoordinates);
            }

            List<PostcodeRequestSummaryDto> requestPostcodeSummaryDtos = (from r in postcodesWithRequestNumbers
                join c in postcodeCoordinates on r.Postcode equals c.Postcode
                select new PostcodeRequestSummaryDto(
                    r.Postcode, r.NumberOfRequests, c.Latitude, c.Longitude
                )).ToList();


            return requestPostcodeSummaryDtos;
        }
    }
}
