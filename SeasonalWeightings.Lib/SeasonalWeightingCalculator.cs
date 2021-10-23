using SeasonalWeightings.Lib.Models;
using SeasonalWeightings.Lib.Repository;
using System;
using System.Linq;

namespace SeasonalWeightings.Lib
{
    public class SeasonalWeightingCalculator : ISeasonalWeightingCalculator
    {
        private readonly ISeasonalWeightingService _seasonalWeightingService;

        public SeasonalWeightingCalculator(ISeasonalWeightingService seasonalWeightingService)
        {
            this._seasonalWeightingService = seasonalWeightingService;
        }


        public decimal CalculateSeasonWeighting(EstimationSettings estimationSettings)
        {
            const int daysInYear = 365;
            int dailyUsage = estimationSettings.AnnualQuantity / daysInYear;
            decimal estimatedUsage = estimationSettings.BillingPeriods.Select(bp => this.GetEstimatedConsumption(dailyUsage, bp)).Sum();

            return estimatedUsage;
        }


        /// <summary>
        /// Gets the estimated consumption for a specific billing period.
        /// </summary>
        /// <param name="dailyUsage">The daily usage.</param>
        /// <param name="billingPeriodInfo">The billing period information.</param>
        /// <returns></returns>
        private decimal GetEstimatedConsumption(int dailyUsage, BillingPeriodInfo billingPeriodInfo)
        {
            decimal seasonalWeightingMultiplier = billingPeriodInfo.SeasonalWeighting / 100.0m;
            decimal dailyAnnualQtyWithWeightKwh = dailyUsage * (seasonalWeightingMultiplier + 1.0m);
            TimeSpan billingPeriod = billingPeriodInfo.EndDate - billingPeriodInfo.StartDate;
            int billingPeriodDays = billingPeriod.Days + 1; // need at add one so last billing day is inclusive
            decimal estimatedUsage = dailyAnnualQtyWithWeightKwh * billingPeriodDays;
            
            return estimatedUsage;
        }
    }
}
