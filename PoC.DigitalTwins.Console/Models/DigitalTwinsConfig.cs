namespace PoC.DigitalTwins.Console.Models;
using System.Text.Json.Serialization;

internal class DigitalTwinsConfig
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("hostName")]
    public string HostName { get; set; } = "";

    [JsonPropertyName("resourceGroupName")]
    public string ResourceGroupName { get; set; } = "";
}
