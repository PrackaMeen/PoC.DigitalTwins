namespace PoC.DigitalTwins.RaspberryPi
{
    using PoC.DigitalTwins.RaspberryPi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RaspberryConfigBuilder
    {
        protected RaspberryConfig _raspberryConfig;
        public RaspberryConfigBuilder()
        {
            _raspberryConfig = new RaspberryConfig()
            {
                Meteo = null,
            };
        }
        public RaspberryConfigBuilder SetMeteo(IMeteoConfig meteoConfig)
        {
            _raspberryConfig.Meteo = meteoConfig;
            return this;
        }
        public RaspberryConfigBuilder SetMeteo(int readPin)
        {
            return SetMeteo(new MeteoConfig
            {
                ReadPin = readPin,
            });
        }
        public IRaspberryConfig Build()
        {
            return _raspberryConfig;
        }
    }
}
