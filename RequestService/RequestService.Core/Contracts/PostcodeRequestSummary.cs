using System.Runtime.Serialization;

namespace RequestService.Core.Contracts
{
    [DataContract(Name = "requestPostcodeSummary")]
    public class PostcodeRequestSummary
    {
        [DataMember(Name = "pc")]
        public string Postcode { get; set; }

        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        [DataMember(Name = "req")]
        public int NumberOfRequests { get; set; }
    }
}
