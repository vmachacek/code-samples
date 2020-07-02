using ProcessingPipeline.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class SaveChanges : IReservationProcessingMiddleware
    {
        private readonly ReservationRepository _reservationRepository;

        public SaveChanges(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        //this steps can be used multiple times, whenever it is needed to save current state of reservation
        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            await _reservationRepository.UpdateOrCreateAsync(context.Reservation);
            await next();
        };
    }
}
