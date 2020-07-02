using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class PayPalPayment : IReservationProcessingMiddleware
    {
        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (c, next) =>
        {
            //execute payment with PayPal
        };
    }
}
