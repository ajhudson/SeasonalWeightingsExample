using SeasonalWeightings.Lib.Repository;
using System.Threading.Tasks;

namespace SeasonalWeightingsDemo.Tests
{
    public class FakeSeasonalWeightingService : ISeasonalWeightingService
    {
        public async Task<int> GetSeasonalWeightingForMonthAsync(int monthNum) => await Task.FromResult(21);
    }
}
