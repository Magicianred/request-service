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
    public class GetPostcodeRequestSummariesInBoundary : IRequestHandler<GetPostcodeRequestSummariesInBoundaryRequest, GetPostcodeRequestSummariesInBoundaryResponse>
    {
        private readonly IRequestCache _requestCache;

        public GetPostcodeRequestSummariesInBoundary(IRequestCache requestCache)
        {
            _requestCache = requestCache;
        }

        public async Task<GetPostcodeRequestSummariesInBoundaryResponse> Handle(GetPostcodeRequestSummariesInBoundaryRequest requestSummariesInBoundaryRequest, CancellationToken cancellationToken)
        {
            IEnumerable<PostcodeRequestSummaryDto> postcodeRequestSummaries = await _requestCache.GetPostcodeRequestSummaries(cancellationToken);

            List<PostcodeRequestSummary> postcodeRequestSummariesInBoundary = postcodeRequestSummaries.WhereWithinBoundary(requestSummariesInBoundaryRequest.SwLatitude, requestSummariesInBoundaryRequest.SwLongitude, requestSummariesInBoundaryRequest.NeLatitude, requestSummariesInBoundaryRequest.NeLongitude).Select(x => new PostcodeRequestSummary()
            {
                Postcode = x.Postcode,
                NumberOfRequests = x.NumberOfRequests,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();

            GetPostcodeRequestSummariesInBoundaryResponse getPostcodeRequestSummariesInBoundaryResponse = new GetPostcodeRequestSummariesInBoundaryResponse();
            getPostcodeRequestSummariesInBoundaryResponse.PostcodeSummaries = postcodeRequestSummariesInBoundary;
            getPostcodeRequestSummariesInBoundaryResponse.TotalNumberOfRequests = postcodeRequestSummariesInBoundary.Sum(x => x.NumberOfRequests);
            
            return getPostcodeRequestSummariesInBoundaryResponse;
        }
    }
}
