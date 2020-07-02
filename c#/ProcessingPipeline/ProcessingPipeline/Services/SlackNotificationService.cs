using ProcessingPipeline.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingPipeline.Services
{
    public class SlackNotificationService
    {
        internal Task SendSlackNotification(Reservation reservation)
        {
            return Task.CompletedTask;
        }
    }
}
