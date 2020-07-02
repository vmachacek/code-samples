using ProcessingPipeline.Domain.ValueObjects;
using System;

namespace ProcessingPipeline.Domain
{
    public class Refund 
    {
        public Guid Id { get; set; }
        public string ExternalSystemId { get; set; }
        public Money Refunded { get; set; }
        public DateTime Created { get; set; }
        public string ReasonText { get; set; }
        public Guid? OrderId { get; set; }

        public Refund()
        {
            Id = Guid.NewGuid();
        }
    }
}
