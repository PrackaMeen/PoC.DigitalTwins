namespace PoC.DigitalTwins.RaspberryPi.Tests
{
    using Xunit;
    using NSubstitute;
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks;
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks.Models;

    public class RangeCalculatorTests
    {
        [Fact]
        public void TestMinRange()
        {
            var rangeCalculator = Substitute.For<RangeCalculator>();
            rangeCalculator.GetCoeficientBetween0and1.Returns(0);
            var rangeConfig = new RangeConfig()
            {
                Min = 15,
                Max = 85
            };

            var result = rangeCalculator.GetValueFromRange(rangeConfig);
            Assert.Equal(rangeConfig.Min, result);
        }

        [Fact]
        public void TestMaxRange()
        {
            var rangeCalculator = Substitute.For<RangeCalculator>();
            rangeCalculator.GetCoeficientBetween0and1.Returns(1);
            var rangeConfig = new RangeConfig()
            {
                Min = 15,
                Max = 85
            };

            var result = rangeCalculator.GetValueFromRange(rangeConfig);
            Assert.Equal(rangeConfig.Max, result);
        }

        [InlineData(0)]
        [InlineData(0.25)]
        [InlineData(0.50)]
        [InlineData(0.75)]
        [InlineData(1)]
        [Theory]
        public void TestMiddleRange(double coeficient)
        {
            var min = 1;
            var max = 5;
            var rangeConfig = new RangeConfig()
            {
                Min = min,
                Max = max
            };
            var expected = min + coeficient * (max - min);

            var rangeCalculator = Substitute.For<RangeCalculator>();
            rangeCalculator.GetCoeficientBetween0and1.Returns(coeficient);

            var result = rangeCalculator.GetValueFromRange(rangeConfig);
            Assert.Equal(expected, result);
        }
    }
}
