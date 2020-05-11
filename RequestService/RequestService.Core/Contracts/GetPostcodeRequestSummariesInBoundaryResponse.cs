using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RequestService.Core.Contracts
{
    [DataContract(Name = "getRequestSummaryCoordinatesResponse")]
    public class GetRequestSummaryCoordinatesResponse
    {
        [DataMember(Name = "postcodeSummaries")]
        public IReadOnlyList<PostcodeRequestSummary> PostcodeSummaries { get; set; }

        [DataMember(Name = "totalNumberOfRequests")]
        public int TotalNumberOfRequests { get; set; }
    }
}
