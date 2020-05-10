using HelpMyStreet.Utils.CoordinatedResetCache;
using RequestService.Core.BusinessLogic;
using RequestService.Core.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Cache
{
    public class RequestCache : IRequestCache
    {
        private readonly ICoordinatedResetCache _coordinatedResetCache;
        private readonly IRequestsForCacheGetter _requestsForCacheGetter;

        public RequestCache(ICoordinatedResetCache coordinatedResetCache, IRequestsForCacheGetter requestsForCacheGetter)
        {
            _coordinatedResetCache = coordinatedResetCache;
            _requestsForCacheGetter = requestsForCacheGetter;
        }

        public async Task<IEnumerable<PostcodeRequestSummaryDto>> GetPostcodeRequestSummaries(CancellationToken cancellationToken)
        {
            IEnumerable<PostcodeRequestSummaryDto> requestPostcodeSummaryDtos = await _coordinatedResetCache.GetCachedDataAsync(async () => await _requestsForCacheGetter.GetRequestPostcodeSummariesAsync(cancellationToken), "PostcodeRequestSummaryDtos", CoordinatedResetCacheTime.OnHour);

            return requestPostcodeSummaryDtos;
        }
    }
}
