using ProcessingPipeline.Domain;
using ProcessingPipeline.Services;
using System;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class CardinityPayment : IReservationProcessingMiddleware
    {
        private readonly PaymentGate _PaymentGate;

        public CardinityPayment(PaymentGate PaymentGate)
        {
            _PaymentGate = PaymentGate;
        }

        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            var response = await _PaymentGate.TryToPay(context.Reservation, context.CardPaymentModel);

            context.Reservation.AddNewPayment(response.PaymentId, PaymentType.CreditCard, context.Reservation.Total);

            if (response.Success)
            {
                await next();
            }
            else
            {
                context.ProcessingResult = new ProcessingResult()
                {
                    ErrorTitle = "There was a problem processing your credit card",
                    ErrorMessage = response.ErrorMessage,
                    ErrorType = ErrorType.PaymentDeclined,
                };
            }
        };
    }
}
