using NUnit.Framework;
using SeasonalWeightings.Lib;
using Shouldly;

namespace SeasonalWeightingsDemo.Tests
{
    [TestFixture]
    public class Tests
    {
        private ISeasonalWeightingCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            this._calculator = new SeasonalWeightingCalculator();
        }

        [Test]
        public void ShouldCalcWhenSeasonalWeightingIsPercentageIncreaseOfAnnualQuantity()
        {
            // Arrange
            const int annualQty = 36500;
            const int seasonalWeighting = 20;

            // Act
            decimal result = this._calculator.CalculateSeasonWeighting(annualQty, seasonalWeighting);
            // Assert
            result.ShouldBe(3720.0m);
        }

        [Test]
        public void ShouldReturnCorrectResultForScenario2()
        {
            // Arrange
            int annualQty = 36500;
            int seasonalWeighting = -20;

            // Act
            decimal result = this._calculator.CalculateSeasonWeighting(annualQty, seasonalWeighting);

            // Assert
            result.ShouldBe(2480m);
        }
    }
}