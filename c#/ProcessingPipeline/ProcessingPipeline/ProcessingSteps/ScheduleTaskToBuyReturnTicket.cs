using ProcessingPipeline.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    /// <summary>
    /// this will schedule task to send user email if customer bought just one way ticket to buy return ticket at the date of their trip
    /// </summary>
    public class ScheduleTaskToBuyReturnTicket : IReservationProcessingMiddleware
    {
        private readonly MailCampaigns mailCampaigns;

        public ScheduleTaskToBuyReturnTicket(MailCampaigns mailCampaigns)
        {
            this.mailCampaigns = mailCampaigns;
        }

        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            if (context.Reservation.IsOneWay())
            {
                var dateTrip = context.Reservation.Orders.First().Route.TripDate;

                var scheudled = dateTrip.Date.AddHours(8);

                //use Task Manager of your choice here, service bus, etc..
                //BackgroundJob.Schedule(() => mailCampaigns.SendNudgeForOneWayTrip(context.Reservation.Id), scheudled);
            }

            await next();
        };
    }
}
