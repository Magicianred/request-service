using HelpMyStreet.Utils.CoordinatedResetCache;
using RequestService.Core.BusinessLogic;
using RequestService.Core.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Cache
{
    public class PostcodeRequestSummaryCache : IPostcodeRequestSummaryCache
    {
        private readonly ICoordinatedResetCache _coordinatedResetCache;
        private readonly IPostcodeRequestSummaryGetter _postcodeRequestSummaryGetter;

        public PostcodeRequestSummaryCache(ICoordinatedResetCache coordinatedResetCache, IPostcodeRequestSummaryGetter postcodeRequestSummaryGetter)
        {
            _coordinatedResetCache = coordinatedResetCache;
            _postcodeRequestSummaryGetter = postcodeRequestSummaryGetter;
        }

        public async Task<IEnumerable<PostcodeRequestSummaryDto>> GetPostcodeRequestSummaries(CancellationToken cancellationToken)
        {
            IEnumerable<PostcodeRequestSummaryDto> requestPostcodeSummaryDtos = await _coordinatedResetCache.GetCachedDataAsync(async () => await _postcodeRequestSummaryGetter.GetRequestPostcodeSummariesAsync(cancellationToken), "PostcodeRequestSummaryDtos", CoordinatedResetCacheTime.OnHour);

            return requestPostcodeSummaryDtos;
        }
    }
}
