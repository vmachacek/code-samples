using ProcessingPipeline.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProcessingPipeline.Domain
{
    public class Payment 
    {
        public Guid Id { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime DatePaid { get; set; }
        public Money Paid { get; set; }
        public List<Refund> Refunds { get; set; }

        public Payment()
        {
            Refunds = new List<Refund>();
            Id = Guid.NewGuid();
        }
    }
}
