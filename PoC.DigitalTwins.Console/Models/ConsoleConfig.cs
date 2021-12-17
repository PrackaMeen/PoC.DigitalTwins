namespace PoC.DigitalTwins.Console.Models;
using System.Text.Json.Serialization;
using PoC.DigitalTwins.RaspberryPi.Models;

internal class ConsoleConfig
{
    [JsonPropertyName("digitalTwins")]
    public DigitalTwinsConfig DigitalTwins { get; set; } = new();

    [JsonPropertyName("raspberryPi")]
    public RaspberryConfig RaspberryPi { get; set; } = new RaspberryConfig();
}

