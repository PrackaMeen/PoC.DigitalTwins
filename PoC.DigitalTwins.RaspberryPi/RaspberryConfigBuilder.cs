namespace PoC.DigitalTwins.RaspberryPi;
using PoC.DigitalTwins.RaspberryPi.Models;

public class RaspberryConfigBuilder
{
    protected RaspberryConfig _raspberryConfig;
    public RaspberryConfigBuilder()
    {
        _raspberryConfig = new RaspberryConfig();
    }
    public RaspberryConfigBuilder SetMeteo(MeteoConfig meteoConfig)
    {
        _raspberryConfig.Meteo = meteoConfig;
        return this;
    }
    public RaspberryConfigBuilder SetMeteo(int readPin, bool useMock = false)
    {
        return SetMeteo(new MeteoConfig
        {
            ReadPin = readPin,
            UseMock = useMock
        });
    }
    public RaspberryConfig Build()
    {
        return _raspberryConfig;
    }
}
