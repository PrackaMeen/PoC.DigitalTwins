namespace PoC.DigitalTwins.RaspberryPi.Components.Mocks
{
    using PoC.DigitalTwins.RaspberryPi.Models;

    public interface IRangeCalculator
    {
        double GetValueFromRangeBy(RangeConfig rangeConfiguration);
    }
}
