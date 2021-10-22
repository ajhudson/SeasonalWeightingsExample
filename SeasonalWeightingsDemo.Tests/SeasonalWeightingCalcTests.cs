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
            // Act
            decimal result = this._calculator.CalculateSeasonWeighting();
            // Assert
            result.ShouldBe(3720.0m);
        }
    }
}