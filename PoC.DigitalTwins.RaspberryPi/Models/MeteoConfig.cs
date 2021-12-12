namespace PoC.DigitalTwins.RaspberryPi.Models
{
    public interface IMeteoConfig
    {
        public int ReadPin { get; }
    }
    public class MeteoConfig : IMeteoConfig
    {
        public int ReadPin { get; set; }
    }
}
