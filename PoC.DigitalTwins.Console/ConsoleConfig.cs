namespace PoC.DigitalTwins.Console
{
    using PoC.DigitalTwins.RaspberryPi.Models;
    using System.Text.Json.Serialization;

    internal class ConsoleConfig
    {
        private string hostName = String.Empty;
        private RaspberryConfig raspberryPi = new();

        [JsonPropertyName("hostName")]
        public string HostName { get => hostName; set => hostName = value; }
        [JsonPropertyName("raspberryPi")]
        public RaspberryConfig RaspberryPi { get => raspberryPi; set => raspberryPi = value; }
    }

    internal class RaspberryConfig : IRaspberryConfig
    {
        [JsonPropertyName("meteo")]
        public MeteoConfig? Meteo { get; set; }

        IMeteoConfig? IRaspberryConfig.Meteo => this.Meteo;
    }

    internal class MeteoConfig : IMeteoConfig
    {
        [JsonPropertyName("readPin")]
        public int ReadPin { get; set; }
    }
}
