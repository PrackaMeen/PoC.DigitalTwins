namespace PoC.DigitalTwins.RaspberryPi.Components.Mocks
{
    using PoC.DigitalTwins.RaspberryPi.Components.Abstractions;
    using PoC.DigitalTwins.RaspberryPi.Models;

    public class DHT11 : IThermometer, IHygroscope
    {
        const double MINIMAL_TEMPERATURE = -35;
        const double MAXIMAL_TEMPERATURE = 45;

        const double MINIMAL_HUMIDITY = 20;
        const double MAXIMAL_HUMIDITY = 95;

        private readonly IRangeCalculator _rangeCalculator;
        private readonly RangeConfig _temperatureRange;
        private readonly RangeConfig _humidityRange;

        public DHT11() : this(
            new RandomRangeCalculator(Random.Shared),
            new RangeConfig
            {
                Max = MAXIMAL_TEMPERATURE,
                Min = MINIMAL_TEMPERATURE,
            },
            new RangeConfig
            {
                Max = MAXIMAL_HUMIDITY,
                Min = MINIMAL_HUMIDITY,
            }
            )
        { }

        public DHT11(
            IRangeCalculator rangeCalculator,
            RangeConfig temperatureRange,
            RangeConfig humidityRange
            )
        {
            _temperatureRange = temperatureRange;
            _humidityRange = humidityRange;
            _rangeCalculator = rangeCalculator;
        }

        public double GetTemperatureInCelsius()
        {
            return _rangeCalculator.GetValueFromRangeBy(_temperatureRange);
        }

        public double GetHumidityInPercent()
        {
            return _rangeCalculator.GetValueFromRangeBy(_humidityRange);
        }

        public double GetHumidity()
        {
            return GetHumidityInPercent();
        }

        public double GetTemperature()
        {
            return GetTemperatureInCelsius();
        }

        public override string ToString()
        {
            var formattedTemperature = String.Format("{0:0.00}", GetTemperature());
            var formattedHumidity = String.Format("{0:0.00}", GetHumidity());
            return $"Temperature: {formattedTemperature}°C, Humidity: {formattedHumidity}%";
        }
    }
}