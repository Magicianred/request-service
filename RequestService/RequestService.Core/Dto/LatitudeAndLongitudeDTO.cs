using System;
using System.Collections.Generic;
using System.Text;

namespace RequestService.Core.Dto
{
    public class LatitudeAndLongitudeDTO
    {
        public string Postcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
