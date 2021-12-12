namespace PoC.DigitalTwins.RaspberryPi
{
    using PoC.DigitalTwins.RaspberryPi.Components;
    using PoC.DigitalTwins.RaspberryPi.Components.Abstractions;
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks;
    using PoC.DigitalTwins.RaspberryPi.Models;
    using System.Device.Gpio;

    public class Setup
    {
        public Setup(IRaspberryConfig config)
        {
            if (null != config.Meteo)
            {
                var gpioController = new GpioController(PinNumberingScheme.Logical);
                Meteostation = new Meteostation(gpioController, config.Meteo.ReadPin);
            }

            Meteostation = new MeteostationMock();
        }

        public IMeteostation Meteostation { get; }
    }
}
