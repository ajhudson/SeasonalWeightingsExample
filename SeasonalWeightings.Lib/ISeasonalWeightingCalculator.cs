namespace SeasonalWeightings.Lib
{
    public interface ISeasonalWeightingCalculator
    {
        decimal CalculateSeasonWeighting(int annualQuantity, int seasonalWeighting);
    }
}
