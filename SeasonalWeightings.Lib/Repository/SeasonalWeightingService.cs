using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
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

        public async Task<int> GetSeasonalWeightingForMonthAsync(int monthNum)
        {
            int num = 0;
            using (var conn = new SqlConnection(this._connectionString))
            {
                var sql = "SELECT seasonal_weighting FROM dbo.SeasonWeightings WHERE MonthNumber = @MonthNumber";
                var parameters = new { MonthNumber = monthNum };
                num = await conn.QuerySingleOrDefaultAsync<int>(sql, parameters);
            }

            return num;
        }
    }
}
