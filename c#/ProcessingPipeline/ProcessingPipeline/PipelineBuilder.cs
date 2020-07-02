namespace ProcessingPipeline
{
    using System.Collections.Generic;
    using ProcessingPipeline.Domain;
    using ProcessingPipeline.ProcessingSteps;

    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly SendEmail _sendEmail;
        private readonly SaveChanges _saveChanges;
        private readonly RegisterUser _registerUser;
        private readonly ScheduleTaskToBuyReturnTicket _scheduleTaskToBuyReturnTicket;
        private readonly NotifySlack _notifySlack;
        private readonly GenerateTickets _generateTickets;
        private readonly GenerateEmail _generateEmail;
        private readonly CardinityPayment _creditCardPayment;
        private readonly PayPalPayment _payPalPayment;

        public PipelineBuilder(
            SendEmail sendEmail,
            SaveChanges saveChanges,
            RegisterUser registerUser,
            ScheduleTaskToBuyReturnTicket scheduleTaskToBuyReturnTicket,
            NotifySlack notifySlack,
            GenerateTickets generateTickets,
            GenerateEmail generateEmail,
            CardinityPayment creditCardPayment,
            PayPalPayment payPalPayment)
        {
            //keeping short for brevity
            _sendEmail = sendEmail;
            _saveChanges = saveChanges;
            _registerUser = registerUser;
            _scheduleTaskToBuyReturnTicket = scheduleTaskToBuyReturnTicket;
            _notifySlack = notifySlack;
            _generateTickets = generateTickets;
            _generateEmail = generateEmail;
            _creditCardPayment = creditCardPayment;
            _payPalPayment = payPalPayment;
        }

        public List<IReservationProcessingMiddleware> GetPipeline(Reservation reservation, PaymentType paymentType)
        {
            var executionPipeline = new List<IReservationProcessingMiddleware>();

            //altering pipeline based on properties of the payload
            if (paymentType == PaymentType.CreditCard)
            {
                executionPipeline.Add(_creditCardPayment);
            }

            if (paymentType == PaymentType.PayPal)
            {
                executionPipeline.Add(_payPalPayment);
            }

            // save immediately after payment
            executionPipeline.Add(_saveChanges);

            if (!reservation.HasApiExternalOrders())
            {
                executionPipeline.Add(_generateTickets);
            }

            executionPipeline.Add(_generateEmail);
            executionPipeline.Add(_saveChanges);
            executionPipeline.Add(_sendEmail);
            executionPipeline.Add(_notifySlack);
            executionPipeline.Add(_registerUser);
            executionPipeline.Add(_saveChanges);
            executionPipeline.Add(_scheduleTaskToBuyReturnTicket);

            return executionPipeline;
        }
    }
}
