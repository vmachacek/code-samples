using System;

namespace ProcessingPipeline.Services
{
    class PaymentResponse
    {
        public bool Success { get; set; }

        public Guid PaymentId { get; set; }
        public string ErrorMessage { get; internal set; }

        public PaymentResponse()
        {
            PaymentId = new Guid();
        }
    }
}
