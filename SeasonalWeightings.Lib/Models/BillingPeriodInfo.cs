using System;

namespace SeasonalWeightings.Lib.Models
{
    public class BillingPeriodInfo
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SeasonalWeighting { get; set; }
    }
}
