using System;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    /// <summary>
    /// task's responsibility is to generate tickets
    /// </summary>
    public class GenerateTickets : IReservationProcessingMiddleware
    {
        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            //something like this - generate pdf tickets and add it to context
            //context.TicketsContent = await _ticketsHelper.GenerateTickets(context.Reservation);
            await next();
        };
    }
}
