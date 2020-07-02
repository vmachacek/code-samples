using ProcessingPipeline.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessingPipeline.Domain
{
    /// <summary>
    /// Domain object Reservation - after the reservation is created in real world. 
    /// It knows about payments, refunds, order-lines, customer, discounts, subject of reservation - trips
    /// </summary>
    public class Reservation
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }

        internal void AddNewPayment(Guid paymentId, PaymentType creditCard, Money total)
        {
            this.Payments.Add(new Payment()
            {
                DatePaid = DateTime.UtcNow,
                Paid = total,
                PaymentType = creditCard,
            });
        }

        public PromoCode PromoCode { get; set; }
        public List<Payment> Payments { get; set; }
        public string Ref { get; set; }
        public string TicketUrl { get; set; }

        public Money Total
        {
            get
            {
                var pasengersCount = Customers?.Count ?? 1;

                decimal sum = Orders.Sum(f => f.Price.Amount * pasengersCount);

                if (PromoCode != null)
                    sum = sum * Math.Abs(1 - PromoCode.PercentageDiscount / (decimal)100);

                return new Money("EUR", Math.Round(sum, 2));
            }
        }

        public Reservation()
        {
            Id = Guid.NewGuid();
            Payments = new List<Payment>();
            Orders = new List<Order>();
            Customers = new List<Customer>();
        }

        public Payment GetLastPayment()
        {
            return Payments.OrderByDescending(f => f.DatePaid).FirstOrDefault();
        }

        public Order GetLastOrder()
        {
            return Orders.OrderByDescending(f => f.Route.TripDate).FirstOrDefault();
        }

        public bool NoRefund()
        {
            return Payments.All(f => f.Refunds.Count == 0);
        }

        public DateTime BookingDate { get; set; }
        public DateTime? DateReviewRequestSent { get; set; }
        public bool IsOneWay() => Orders.Count == 1;
        public string GetCustomerNationality() => Customers.FirstOrDefault()?.Nationality;

        internal bool HasApiExternalOrders()
        {
            return false;
        }

        public string GetFullName()
        {
            return Customers.FirstOrDefault()?.FullName;
        }

        public void ChangeStateOnOrder(Guid orderId, OrderState newState, string user, string remark = null)
        {
            var order = Orders.SingleOrDefault(f => f.Id == orderId) ?? throw new ArgumentNullException("Orders.SingleOrDefault(f => f.Id == orderId)");
            order.SetState(newState, user, remark);
        }
    }
}
