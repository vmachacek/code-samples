using ProcessingPipeline.Services;
using System;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class NotifySlack : IReservationProcessingMiddleware
    {
        private readonly SlackNotificationService _slackNotificationService;

        public NotifySlack(SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
        }

        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            //send notification to company slack
            await _slackNotificationService.SendSlackNotification(context.Reservation);
            await next();
        };
    }
}
