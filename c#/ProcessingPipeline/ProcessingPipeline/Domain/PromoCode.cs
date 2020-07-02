using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Domain
{
    public class PromoCode
    {
        public string Code { get; set; }
        public int PercentageDiscount { get; set; }
        public int PercentageComission { get; set; }
    }
}
