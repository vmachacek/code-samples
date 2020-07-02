using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Domain
{
    public class Route
    {
        public Place FromPlace { get; set; }
        public Place ToPlace { get; set; }
        public DateTime TripDate { get; set; }
        public TimeSpan TimeOfDeparture { get; set; }
        public string PortNameFrom { get; set; }
        public string PortNameTo { get; set; }
        public string ExternalPayload { get; set; }
    }
}
