using ProcessingPipeline.ContextModels;
using ProcessingPipeline.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingPipeline.Services
{
    public class PaymentGate
    {
        internal  Task<PaymentResponse> TryToPay(Reservation reservation, PaymentModel cardPaymentModel)
        {
            //do your thing

            return Task.FromResult(new PaymentResponse()
            {
                Success = true
            });
        }
    }
}
