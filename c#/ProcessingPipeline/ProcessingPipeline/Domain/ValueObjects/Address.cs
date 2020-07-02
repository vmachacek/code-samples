using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Domain.ValueObjects
{
    public class Address
    {
        public string Location { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public GeoCoordinate LocationCoordinates { get; set; }
    }
}
