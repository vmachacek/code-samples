using ProcessingPipeline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class RegisterUser : IReservationProcessingMiddleware
    {
        private readonly Auth0Service _auth0Service;

        public RegisterUser(Auth0Service auth0Service)
        {
            _auth0Service = auth0Service;
        }

        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (c, next) =>
        {
            //for users convenience we are creating user's account after they buy for a first time 
            await _auth0Service.CreateUserIfDoesNotExistAsync(new Auth0UserCreationDto
            {
                email = c.Reservation.Email,
                password = c.Reservation.PhoneNumber,
                name = c.Reservation.GetFullName(),
            });

            await next();
        };
    }
}
