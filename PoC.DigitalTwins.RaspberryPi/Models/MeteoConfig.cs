namespace PoC.DigitalTwins.RaspberryPi.Models;
using System.Text.Json.Serialization;

public class MeteoConfig
{
    [JsonPropertyName("readPin")]
    public int ReadPin { get; set; } = -1;

    [JsonPropertyName("useMock")]
    public bool UseMock { get; set; } = true;
}
