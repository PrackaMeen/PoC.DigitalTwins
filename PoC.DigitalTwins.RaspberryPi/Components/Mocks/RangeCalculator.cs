namespace PoC.DigitalTwins.RaspberryPi.Components.Mocks
{
    using PoC.DigitalTwins.RaspberryPi.Models;

    public abstract class RangeCalculator : IRangeCalculator
    {
        public abstract double GetCoeficientBetween0and1 { get; }
        public double GetValueFromRangeBy(RangeConfig rangeConfiguration)
        {
            double randomCoeficient = GetCoeficientBetween0and1;
            return rangeConfiguration.Min +
                randomCoeficient * (rangeConfiguration.Max - rangeConfiguration.Min);
        }
    }
}
