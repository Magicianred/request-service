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

namespace RequestService.Core.BusinessLogic
{
    public class RequestsForCacheGetter : IRequestsForCacheGetter
    {
        private readonly IRepository _repository;
        private readonly IAddressService _addressService;

        public RequestsForCacheGetter(IRepository repository, IAddressService addressService)
        {
            _repository = repository;
            _addressService = addressService;
        }

        public async Task<IEnumerable<PostcodeRequestSummaryDto>> GetRequestPostcodeSummariesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<PostcodeWithNumberOfRequestsDto> postcodesWithRequestNumbers = await _repository.GetNumberOfRequestsPerPostcode();

            IEnumerable<IEnumerable<string>> postcodeChunks = postcodesWithRequestNumbers.Select(x => x.Postcode).Chunk(20000); // TODO: put in app setting

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
