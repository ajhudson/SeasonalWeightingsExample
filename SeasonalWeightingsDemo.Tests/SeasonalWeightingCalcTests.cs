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
        [TestCase(20, 3720)]
        [TestCase(-20, 2480)]
        public void ShouldReturnCorrectResultForScenario1And2(int seasonalWeighting, decimal expectedResult)
        {
            // Arrange
            int annualQty = 36500;
            // Act
            decimal result = this._calculator.CalculateSeasonWeighting(annualQty, seasonalWeighting);
            // Assert
            result.ShouldBe(expectedResult);
        }
    }
}