namespace ProcessingPipeline.Tests.Stubs
{
    using System;
    using System.Threading.Tasks;

    // spy to check the email content during pipeline generation
    public class EmailContentSpy : IReservationProcessingMiddleware
    {
        public string EmailContent { get; set; }

        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            EmailContent = context.EmailContent;
            await next();
        };
    }
}
