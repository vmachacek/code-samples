using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class ValidationStep : IReservationProcessingMiddleware
    {
        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            //use fluent validation or similar 
            await next();
        };
    }
}
