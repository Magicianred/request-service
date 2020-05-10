namespace RequestService.Core.Dto
{
    public class PostcodeWithNumberOfRequestsDto
    {
        public PostcodeWithNumberOfRequestsDto(string postcode, int numberOfRequests)
        {
            Postcode = postcode;
            NumberOfRequests = numberOfRequests;
        }

        public string Postcode { get;  }
        public int NumberOfRequests { get; }
    }
}
