using System.Threading.Tasks;

namespace SeasonalWeightings.Lib.Repository
{
    public interface ISeasonalWeightingService
    {
        Task<int> GetSeasonalWeightingForMonthAsync(int monthNumber);
    }
}
