using Moq;
using NUnit.Framework;
using SeasonalWeightings.Lib;
using SeasonalWeightings.Lib.Models;
using SeasonalWeightings.Lib.Repository;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeasonalWeightingsDemo.Tests
{
    [TestFixture]
    public class Tests
    {
        private ISeasonalWeightingCalculator _calculator;

        private Mock<ISeasonalWeightingService> _seasonalWeightingService;

        [SetUp]
        public void Setup()
        {
            this._seasonalWeightingService = new Mock<ISeasonalWeightingService>();
            this._calculator = new SeasonalWeightingCalculator(this._seasonalWeightingService.Object);
        }

        [Test]
        [TestCase(20, 3720)]
        [TestCase(-20, 2480)]
        public async Task ShouldReturnCorrectResultForScenario1And2(int seasonalWeighting, decimal expectedResult)
        {
            // Arrange
            this._seasonalWeightingService.Setup(x => x.GetSeasonalWeightingForMonthAsync(It.IsAny<int>())).ReturnsAsync(seasonalWeighting);

            var januaryBillingInfo = CreateBillingInfo(new DateTime(2021, 1, 1), new DateTime(2021, 1, 31));
            var estimationSettings = new EstimationSettings
            {
                AnnualQuantity = 36500,
                BillingPeriods = new List<BillingPeriodInfo> { januaryBillingInfo }
            };

            // Act
            decimal result = await this._calculator.CalculateSeasonWeightingAsync(estimationSettings);
            // Assert
            result.ShouldBe(expectedResult);
        }

        [Test]
        public async Task ShouldReturnCorrectResultForScenario3()
        {
            // Arrange
            this._seasonalWeightingService.SetupSequence(x => x.GetSeasonalWeightingForMonthAsync(It.IsAny<int>()))
                                            .ReturnsAsync(20)
                                            .ReturnsAsync(22);

            var januaryBillingInfo = CreateBillingInfo(new DateTime(2020, 1, 1), new DateTime(2020, 1, 31));
            var februaryBillingInfo = CreateBillingInfo(new DateTime(2020, 2, 1), new DateTime(2020, 2, 29));
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
            decimal result = await this ._calculator.CalculateSeasonWeightingAsync(estimationSettings);

            // Assert
            result.ShouldBe(7258.0m);
        }

        [Test]
        public async Task ShouldReturnCorrectResultForScenario4()
        {
            // Arrange 
            this._seasonalWeightingService.SetupSequence(x => x.GetSeasonalWeightingForMonthAsync(It.IsAny<int>()))
                                            .ReturnsAsync(20)
                                            .ReturnsAsync(22)
                                            .ReturnsAsync(24);

            var januaryBillingInfo = CreateBillingInfo(new DateTime(2020, 1, 26), new DateTime(2020, 1, 31));
            var februaryBillingInfo = CreateBillingInfo(new DateTime(2020, 2, 1), new DateTime(2020, 2, 29));
            var marchBillingInfo = CreateBillingInfo(new DateTime(2020, 3, 1), new DateTime(2020, 3, 31));
            var estimationSettings = new EstimationSettings
            {
                AnnualQuantity = 36500,
                BillingPeriods = new List<BillingPeriodInfo>
                {
                    januaryBillingInfo,
                    februaryBillingInfo,
                    marchBillingInfo
                }
            };

            // Act 
            decimal result = await this._calculator.CalculateSeasonWeightingAsync(estimationSettings);

            // Assert 
            result.ShouldBe(8102.0m);
        }

        private static BillingPeriodInfo CreateBillingInfo(DateTime start, DateTime end)
        {
            return new BillingPeriodInfo
            {
                StartDate = start,
                EndDate = end
            };
        }
    }
}