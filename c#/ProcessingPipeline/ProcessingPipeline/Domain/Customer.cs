using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Domain
{
    public class Customer
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime? BirthDate { get; set; }
        public string NationalityId { get; set; }
        public string CountryResidenceId { get; set; }
        public Gender? Gender { get; set; }
        public string PassportNo { get; set; }
        public string PassportPlaceIssue { get; set; }
        public string PassportCountryIssueId { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public int Age { get; set; }

        public bool IsValidForIntegration(IntegrationType integrationType)
        {
            var isValidForIntegration = BirthDate.HasValue && Gender.HasValue && PassportIssueDate.HasValue && PassportExpiryDate.HasValue;
            return isValidForIntegration;
        }
    }

    public enum IntegrationType
    {
        BatamFast,
        SindoFerry
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
