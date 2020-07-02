using ProcessingPipeline.Services;
using System;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class SendEmail : IReservationProcessingMiddleware
    {
        private readonly Mailer _mailer;

        public SendEmail(Mailer mailer)
        {
            _mailer = mailer;
        }

        // send email 
        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            await _mailer.SendEmails(context.Reservation, context.EmailContent, context.TicketsContent);
            await next();
        };
    }
}
