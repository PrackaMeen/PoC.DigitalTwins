namespace PoC.DigitalTwins.Console
{
    using PoC.DigitalTwins.RaspberryPi.Models;
    using System.Text.Json.Serialization;

    internal class ConsoleConfig
    {
        [JsonPropertyName("digitalTwins")]
        public DigitalTwinsConfig DigitalTwins { get; set; } = new();

        [JsonPropertyName("raspberryPi")]
        public RaspberryConfig RaspberryPi { get; set; } = new();
    }

    internal class DigitalTwinsConfig
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("hostName")]
        public string HostName { get; set; } = "";

        [JsonPropertyName("resourceGroupName")]
        public string ResourceGroupName { get; set; } = "";
    }

    internal class RaspberryConfig : IRaspberryConfig
    {
        [JsonPropertyName("meteo")]
        public MeteoConfig? Meteo { get; set; } = null;

        IMeteoConfig? IRaspberryConfig.Meteo => this.Meteo;
    }

    internal class MeteoConfig : IMeteoConfig
    {
        [JsonPropertyName("readPin")]
        public int ReadPin { get; set; }
    }
}
