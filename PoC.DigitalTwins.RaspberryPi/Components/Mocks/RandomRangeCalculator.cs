namespace PoC.DigitalTwins.RaspberryPi.Components.Mocks
{
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
