using ProcessingPipeline.Domain.ValueObjects;
using System;

namespace ProcessingPipeline.Domain
{
    public class Order 
    {
        public Guid Id { get; set; }
        public string Ref { get; set; }
        public OrderState State { get; set; }
        public Money Price { get; set; }
        public Money Paid { get; set; }
        public Money InvoicePrice { get; set; }
        public Company Company { get; set; }
        public Route Route { get; set; }

        public Order()
        {
        }

        public Order(Trip trip, Company company, Money invoicePrice, string reservationReference, int index, Money customerPaid)
        {
            var route = trip.Route;

            Ref = $"{reservationReference}-{index + 1}";
            State = OrderState.New;
            
            Company = company;
            Route = new Route
            {
                TripDate = trip.Date.Date,
                FromPlace = route.FromPlace,
                ToPlace = route.ToPlace,
                PortNameFrom = route.PortNameFrom,
                PortNameTo = route.PortNameTo,
                ExternalPayload = route.ExternalPayload
            };
            Id = Guid.NewGuid();
            InvoicePrice = invoicePrice;
            Paid = customerPaid;
        }

        public void SetInvoicePrice(Money invoicePrice, string user)
        {
            //add logs
            InvoicePrice = invoicePrice;
        }

        public void SetState(OrderState newState, string user, string remark = null)
        {
            //add auditing 

            State = newState;
        }


        public bool IsCanceled()
        {
            return State == OrderState.RejectedClosed ||
                   State == OrderState.CanceledByCustomer;
        }
    }
}
