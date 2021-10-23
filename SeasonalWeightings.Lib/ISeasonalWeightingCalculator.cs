using System.Threading.Tasks;

namespace SeasonalWeightings.Lib
{
    public interface ISeasonalWeightingCalculator
    {
        Task<decimal> CalculateSeasonWeightingAsync(EstimationSettings estimationSettings);
    }
}
