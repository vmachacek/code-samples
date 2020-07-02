using ProcessingPipeline.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Domain
{
    public class Trip
    {
        public Place From { get; set; }
        public Place To { get; set; }
        public DateTime Date { get; set; }
        public Route Route { get; set; }
        public Address PickupAddress { get; set; }
        public Address DropoffAddress { get; set; }   
    }
}
