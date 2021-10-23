using Dapper;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SeasonalWeightings.Lib.Repository
{
    [ExcludeFromCodeCoverage]
    public class SeasonalWeightingService : ISeasonalWeightingService
    {
        private readonly string _connectionString;

        public SeasonalWeightingService(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<int> GetSeasonalWeightingForMonthAsync(int monthNumber)
        {
            int num = 0;
            using (var conn = new SqlConnection(this._connectionString))
            {
                var sql = "SELECT seasonal_weighting FROM dbo.SeasonWeightings WHERE MonthNumber = @MonthNumber";
                var parameters = new { MonthNumber = monthNumber };
                num = await conn.QuerySingleOrDefaultAsync<int>(sql, parameters);
            }
            return num;
        }
    }
}
