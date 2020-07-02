using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Domain
{
    public enum OrderState
    {
        New = 1,
        CanceledByCustomer = 3,
        ChangedDate = 4,
        Confirmed = 5,
        Rejected = 10,
        RejectedClosed = 12,
        WaitingForPayment = 16,
        Paid = 20,
    }
}
