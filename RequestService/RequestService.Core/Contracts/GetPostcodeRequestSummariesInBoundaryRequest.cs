using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RequestService.Core.Contracts
{
    public class GetRequestSummaryCoordinatesRequest : IRequest<GetRequestSummaryCoordinatesResponse>
    {
        [Required]
        [Range(-90, 90)]
        public double SwLatitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double SwLongitude { get; set; }

        [Required]
        [Range(-90, 90)]
        public double NeLatitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double NeLongitude { get; set; }
    }
}
