using System;
using ProcessingPipeline.ContextModels;
using ProcessingPipeline.Domain;

namespace ProcessingPipeline
{
    public class ReservationProcessingContext
    {
        public PaymentModel CardPaymentModel { get; set; }
        public Reservation Reservation { get; set; }
        public ProcessingResult ProcessingResult { get; set; }
        public string EmailContent { get; set; }
        public Guid DraftId { get; set; }
        public byte[] TicketsContent { get; set; }
        public object PartnerData { get; set; }
    }

    public class ProcessingResult
    {
        public string ErrorMessage { get; set; }
        public string ErrorTitle { get; set; }
        public ErrorType ErrorType { get; set; }
        public bool IsSuccess { get; set; }

        public ProcessingResult()
        {
            IsSuccess = false;
        }
    }

    public enum ErrorType
    {
        MakingReservation = 1,
        PaymentError,
        PaymentDeclined,
        Confirming,
        Other,
        ServerError
    }
}
