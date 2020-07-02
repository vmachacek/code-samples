using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProcessingPipeline.Domain
{
    public class Company 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CommissionPercentage { get; set; }
        public int ReturnTicketPercentageDiscount { get; set; }
        public int ReservationCutOffTimeHours { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> SupportEmailAddresses { get; set; }
        public Guid RegionId { get; set; }
        public string LogoUrl { get; set; }
        public float RatingValueTotal { get; set; }
        public string DefaultCurrency { get; set; }
        public bool TagBestSeller { get; set; }
        public bool TagGreatService { get; set; }

        public Company()
        {
            Id = Guid.NewGuid();
        }

        public string GetCompanyGroupName()
        {
            var s = Regex.Replace(Name, @"[^A-z]", "");
            if (s.Length > 10)
            {
                s = s.Substring(0, 9);
            }

            return $"{s}/{Id:D}";
        }

        public string GetDefaultCurrency()
        {
            return string.IsNullOrEmpty(DefaultCurrency) ? "IDR" : DefaultCurrency;
        }
    }
}
