using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Dto;

namespace RequestService.Core.BusinessLogic
{
    public interface IRequestsForCacheGetter
    {
        Task<IEnumerable<PostcodeRequestSummaryDto>> GetRequestPostcodeSummariesAsync(CancellationToken cancellationToken);
    }
}