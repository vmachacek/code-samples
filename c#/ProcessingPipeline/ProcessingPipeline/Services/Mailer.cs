using ProcessingPipeline.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingPipeline.Services
{
    public class Mailer
    {
        //this should send emails
        internal Task SendEmails(Reservation reservation, string emailContent, byte[] ticketsContent)
        {
            return Task.CompletedTask;
        }
    }
}
