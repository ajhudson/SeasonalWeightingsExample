using SeasonalWeightings.Lib.Repository;
using System.Threading.Tasks;

namespace SeasonalWeightingsDemo.Tests.FakeRepositories
{
    public class FakeSeasonalWeightingService : ISeasonalWeightingService
    {
        public async Task<int> GetSeasonalWeightingForMonthAsync(int monthNumber) => await Task.FromResult(21);
    }
}
