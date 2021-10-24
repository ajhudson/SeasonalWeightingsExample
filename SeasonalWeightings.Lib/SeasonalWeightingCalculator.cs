using SeasonalWeightings.Lib.Models;
using SeasonalWeightings.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeasonalWeightings.Lib
{
    public class SeasonalWeightingCalculator : ISeasonalWeightingCalculator
    {
        private readonly ISeasonalWeightingService _seasonalWeightingService;

        public SeasonalWeightingCalculator(ISeasonalWeightingService seasonalWeightingService)
        {
            this._seasonalWeightingService = seasonalWeightingService;
        }

        public async Task<decimal> CalculateSeasonWeightingAsync(EstimationSettings estimationSettings)
        {
            const int daysInYear = 365;
            int dailyUsage = estimationSettings.AnnualQuantity / daysInYear;
            IEnumerable<Task<decimal>> getEstimatedUsageTasks = estimationSettings.BillingPeriods.Select(async bp => await this.GetEstimatedConsumption(dailyUsage, bp));
            decimal[] estimatedUsages = await Task.WhenAll(getEstimatedUsageTasks);
            decimal estimatedUsage = estimatedUsages.Sum();

            return estimatedUsage;
        }


        /// <summary>
        /// Gets the estimated consumption for a specific billing period.
        /// </summary>
        /// <param name="dailyUsage">The daily usage.</param>
        /// <param name="billingPeriodInfo">The billing period information.</param>
        /// <returns></returns>
        private async Task<decimal> GetEstimatedConsumption(int dailyUsage, BillingPeriodInfo billingPeriodInfo)
        {
            int monthNum = billingPeriodInfo.StartDate.Month;
            int seasonalWeighting = await this._seasonalWeightingService.GetSeasonalWeightingForMonthAsync(monthNum);

            decimal seasonalWeightingMultiplier = seasonalWeighting / 100.0m;
            decimal dailyAnnualQtyWithWeightKwh = dailyUsage * (seasonalWeightingMultiplier + 1.0m);
            TimeSpan billingPeriod = billingPeriodInfo.EndDate - billingPeriodInfo.StartDate;
            int billingPeriodDays = billingPeriod.Days + 1; // need at add one so last billing day is inclusive
            decimal estimatedUsage = dailyAnnualQtyWithWeightKwh * billingPeriodDays;
            
            return estimatedUsage;
        }
    }
}
