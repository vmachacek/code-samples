using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Domain
{
    public class Place 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasLandTransportOption {get;set;}
    }
}
