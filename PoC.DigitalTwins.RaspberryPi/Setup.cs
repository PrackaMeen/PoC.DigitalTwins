namespace PoC.DigitalTwins.RaspberryPi
{
    using PoC.DigitalTwins.RaspberryPi.Components;
    using PoC.DigitalTwins.RaspberryPi.Components.Abstractions;
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks;
    using PoC.DigitalTwins.RaspberryPi.Models;
    using System.Device.Gpio;

    public class Setup
    {
        protected GpioController? _gpioControllerLazy = null;
        private readonly object _lock = new();

        public Setup(IRaspberryConfig config)
        {
            if (null != config.Meteo)
            {
                Meteostation = new Meteostation(GpioControllerLazy, config.Meteo.ReadPin);
            }

            Meteostation = new MeteostationMock();
        }

        protected GpioController GpioControllerLazy
        {
            get
            {
                if (_gpioControllerLazy == null)
                {
                    lock (_lock)
                    {
                        if (_gpioControllerLazy == null)
                        {
                            _gpioControllerLazy = new GpioController(PinNumberingScheme.Logical);
                        }
                    }
                }

                return _gpioControllerLazy;
            }
        }
        public IMeteostation Meteostation { get; }
    }
}
