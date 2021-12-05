namespace PoC.DigitalTwins.RaspberryPi.Tests.Components.Mocks
{
    using NSubstitute;
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks;
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks.Models;
    using Xunit;

    [Trait("UnitTest", "UnitTest")]
    public class DHP11Tests
    {
        [InlineData(0, 0, 100, 0, 0)]
        [InlineData(0.25, 0, 100, 25, 25)]
        [InlineData(0.50, 0, 100, 50, 50)]
        [InlineData(0.75, 0, 100, 75, 75)]
        [InlineData(1, 0, 100, 100, 100)]
        [Theory]
        public void Test(double coeficient, double min, double max, double expectedTemperature, double expectedHumidity)
        {
            var temperatureRangeConfig = new RangeConfig()
            {
                Max = max,
                Min = min
            };
            var humidityRangeConfig = new RangeConfig()
            {
                Max = max,
                Min = min
            };
            var rangeCalculator = Substitute.For<RangeCalculator>();
            rangeCalculator.GetCoeficientBetween0and1.Returns(coeficient);

            var dhp11 = new DHP11(
                rangeCalculator, 
                temperatureRange: temperatureRangeConfig, 
                humidityRange: humidityRangeConfig
            );

            Assert.Equal(expectedTemperature, dhp11.GetTemperature());
            Assert.Equal(expectedHumidity, dhp11.GetHumidity());
        }
    }
}