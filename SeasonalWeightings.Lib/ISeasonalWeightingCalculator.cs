namespace SeasonalWeightings.Lib
{
    public interface ISeasonalWeightingCalculator
    {
        decimal CalculateSeasonWeighting(EstimationSettings estimationSettings);
    }
}
