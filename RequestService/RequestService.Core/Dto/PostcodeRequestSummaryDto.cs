using HelpMyStreet.Utils.Dtos;

namespace RequestService.Core.Dto
{
    public class PostcodeRequestSummaryDto : ILatitudeLongitude
    {
        public PostcodeRequestSummaryDto(string postcode, int numberOfRequests, double latitude, double longitude)
        {
            Postcode = postcode;
            NumberOfRequests = numberOfRequests;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Postcode { get; set; }

        public int NumberOfRequests { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
