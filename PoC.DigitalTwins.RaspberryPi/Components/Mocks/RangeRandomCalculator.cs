namespace PoC.DigitalTwins.RaspberryPi.Components.Mocks
{
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks.Models;

    public interface IRangeCalculator
    {
        double GetValueFromRange(RangeConfig rangeConfiguration);
    }

    public abstract class RangeCalculator : IRangeCalculator
    {
        public virtual double GetCoeficientBetween0and1 { get; }
        public double GetValueFromRange(RangeConfig rangeConfiguration)
        {
            double randomCoeficient = GetCoeficientBetween0and1;
            return rangeConfiguration.Min +
                randomCoeficient * (rangeConfiguration.Max - rangeConfiguration.Min);
        }
    }

    public class RandomRangeCalculator : RangeCalculator
    {
        private readonly Random _random;
        public RandomRangeCalculator(Random random)
        {
            _random = random;
        }
        public override double GetCoeficientBetween0and1 => _random.NextDouble();
    }
}
