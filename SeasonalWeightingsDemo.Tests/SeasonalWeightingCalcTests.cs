using NUnit.Framework;
using SeasonalWeightings.Lib;
using SeasonalWeightings.Lib.Models;
using Shouldly;
using System;
using System.Collections.Generic;

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
            var januaryBillingInfo = new BillingPeriodInfo
            {
                StartDate = new DateTime(2021, 1, 1),
                EndDate = new DateTime(2021, 1, 31),
                SeasonalWeighting = seasonalWeighting
            };

            var estimationSettings = new EstimationSettings
            {
                AnnualQuantity = 36500,
                BillingPeriods = new List<BillingPeriodInfo> { januaryBillingInfo }
            };

            // Act
            decimal result = this._calculator.CalculateSeasonWeighting(estimationSettings);
            // Assert
            result.ShouldBe(expectedResult);
        }

        [Test]
        public void ShouldReturnCorrectResultForScenario3()
        {
            // Arrange
            var januaryBillingInfo = new BillingPeriodInfo
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 1, 31),
                SeasonalWeighting = 20
            };

            var februaryBillingInfo = new BillingPeriodInfo
            {
                StartDate = new DateTime(2020, 2, 1),
                EndDate = new DateTime(2020, 2, 29),
                SeasonalWeighting = 22
            };

            var estimationSettings = new EstimationSettings
            {
                AnnualQuantity = 36500,
                BillingPeriods = new List<BillingPeriodInfo>
                {
                januaryBillingInfo,
                februaryBillingInfo
                }
            };
            // Act
            decimal result = this._calculator.CalculateSeasonWeighting(estimationSettings);

            // Assert
            result.ShouldBe(7258.0m);
        }
    }
}