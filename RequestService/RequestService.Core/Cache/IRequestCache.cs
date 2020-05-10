using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Dto;

namespace RequestService.Core.Cache
{
    public interface IRequestCache
    {
        Task<IEnumerable<PostcodeRequestSummaryDto>> GetPostcodeRequestSummaries(CancellationToken cancellationToken);
    }
}