namespace PoC.DigitalTwins.RaspberryPi.Models;
using System.Text.Json.Serialization;

public class RaspberryConfig
{
    [JsonPropertyName("meteo")]
    public MeteoConfig Meteo { get; set; } = new();
}
