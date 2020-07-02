using ProcessingPipeline.Domain;
using System.Collections.Generic;

namespace ProcessingPipeline
{
    public interface IPipelineBuilder
    {
        List<IReservationProcessingMiddleware> GetPipeline(Reservation reservation, PaymentType paymentType);
    }
}