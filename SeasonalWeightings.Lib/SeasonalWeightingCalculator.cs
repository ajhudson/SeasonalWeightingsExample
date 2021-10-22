using System;

namespace SeasonalWeightings.Lib
{
    public class SeasonalWeightingCalculator : ISeasonalWeightingCalculator
    {
        public decimal CalculateSeasonWeighting(int annualQuantity, int seasonalWeighting)
        {
            const int daysInYear = 365;
            int dailyUsage = annualQuantity / daysInYear;
            decimal seasonalWeightingMultiplier = seasonalWeighting / 100.0m;
            decimal dailyAnnualQuantityWithWeightingKwh = dailyUsage *
            (seasonalWeightingMultiplier + 1.0m);
            int daysInBillingPeriod = 31;
            decimal estimatedUsage = dailyAnnualQuantityWithWeightingKwh *
            daysInBillingPeriod;

            return estimatedUsage;
        }
    }
}
