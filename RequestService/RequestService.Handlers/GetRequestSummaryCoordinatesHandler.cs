using HelpMyStreet.Utils.Extensions;
using MediatR;
using RequestService.Core.Cache;
using RequestService.Core.Contracts;
using RequestService.Core.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Handlers
{
    public class GetRequestSummaryCoordinatesHandler : IRequestHandler<GetRequestSummaryCoordinatesRequest, GetRequestSummaryCoordinatesResponse>
    {
        private readonly IPostcodeRequestSummaryCache _postcodeRequestSummaryCache;

        public GetRequestSummaryCoordinatesHandler(IPostcodeRequestSummaryCache postcodeRequestSummaryCache)
        {
            _postcodeRequestSummaryCache = postcodeRequestSummaryCache;
        }

        public async Task<GetRequestSummaryCoordinatesResponse> Handle(GetRequestSummaryCoordinatesRequest requestSummaryCoordinatesRequest, CancellationToken cancellationToken)
        {
            IEnumerable<PostcodeRequestSummaryDto> postcodeRequestSummaries = await _postcodeRequestSummaryCache.GetPostcodeRequestSummaries(cancellationToken);

            List<PostcodeRequestSummary> postcodeRequestSummariesInBoundary = postcodeRequestSummaries.WhereWithinBoundary(requestSummaryCoordinatesRequest.SwLatitude, requestSummaryCoordinatesRequest.SwLongitude, requestSummaryCoordinatesRequest.NeLatitude, requestSummaryCoordinatesRequest.NeLongitude).Select(x => new PostcodeRequestSummary()
            {
                Postcode = x.Postcode,
                NumberOfRequests = x.NumberOfRequests,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();

            GetRequestSummaryCoordinatesResponse getRequestSummaryCoordinatesResponse = new GetRequestSummaryCoordinatesResponse();
            getRequestSummaryCoordinatesResponse.PostcodeSummaries = postcodeRequestSummariesInBoundary;
            getRequestSummaryCoordinatesResponse.TotalNumberOfRequests = postcodeRequestSummariesInBoundary.Sum(x => x.NumberOfRequests);
            
            return getRequestSummaryCoordinatesResponse;
        }
    }
}
