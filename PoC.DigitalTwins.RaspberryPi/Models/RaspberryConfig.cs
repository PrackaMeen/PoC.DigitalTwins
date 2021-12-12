namespace PoC.DigitalTwins.RaspberryPi.Models
{
    public interface IRaspberryConfig
    {
        public IMeteoConfig? Meteo { get; }
    }
    public class RaspberryConfig : IRaspberryConfig
    {
        public IMeteoConfig? Meteo { get; set; }
    }
}
