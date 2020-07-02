using System;
using System.Threading.Tasks;

namespace ProcessingPipeline
{
    public interface IReservationProcessingMiddleware
    {
        Func<ReservationProcessingContext, Func<Task>, Task> Execute { get; }
    }
}
