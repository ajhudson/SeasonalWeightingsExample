using SeasonalWeightings.Lib.Models;
using System.Collections.Generic;

namespace SeasonalWeightings.Lib
{
    public class EstimationSettings
    {
        public int AnnualQuantity { get; set; }
        public List<BillingPeriodInfo> BillingPeriods { get; set; }
    }
}
