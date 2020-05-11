using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Dto;

namespace RequestService.Core.Cache
{
    public interface IPostcodeRequestSummaryCache
    {
        Task<IEnumerable<PostcodeRequestSummaryDto>> GetPostcodeRequestSummaries(CancellationToken cancellationToken);
    }
}