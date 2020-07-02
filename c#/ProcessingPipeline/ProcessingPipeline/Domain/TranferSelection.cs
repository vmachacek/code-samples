using ProcessingPipeline.Domain.ValueObjects;
using System;

namespace ProcessingPipeline.Domain
{
    public class TranferSelection
    {
        public Address Address { get; set; }
        public Guid Id { get; set; } 
        public Money Price { get; set; }
        public VehicleType VehicleType { get; set; }
        public TransferOptionType TransferType { get; set; }
        public string TicketNote { get; set; }
        public TimeSpan TimeOffset { get; set; }

        public bool GetInterested()
        {
            return !string.IsNullOrEmpty(Address?.AddressLine1);
        }
    }

    public enum VehicleType
    {
        ShuttleBus,
        PrivateCar
    }

    public enum TransferOptionType
    {
        AtYourAddress,
        PointNearby,
        Available,
        Outside
    }
}
