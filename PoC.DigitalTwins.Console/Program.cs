// See https://aka.ms/new-console-template for more information
using PoC.DigitalTwins.Console.Models;
using PoC.DigitalTwins.RaspberryPi;
using System.Text.Json;

Console.WriteLine("Welcome in PoC - DigitalTwins");
Console.WriteLine("Press any key for next line or ESC for termination");

ConsoleConfig? config = null;
try
{
    using (StreamReader sr = new(Path.GetFullPath("./config.json")))
    {
        config = await JsonSerializer.DeserializeAsync<ConsoleConfig>(sr.BaseStream);
    }
}
catch (JsonException ex) { Console.WriteLine($"Invalid Json Format: {ex.Message}"); }
catch (FileNotFoundException ex) { Console.WriteLine($"FileNotFoundException: {ex.Message}"); }
finally
{
    config ??= new ConsoleConfig
    {
        RaspberryPi = new RaspberryConfigBuilder().Build()
    };

    Console.WriteLine($"Values used for config:");
    var serializedJson = JsonSerializer.Serialize(config, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    });
    Console.WriteLine(serializedJson);
}

Setup raspberryPi = new(config.RaspberryPi);

bool shouldShowValues(DateTime lastTimeCheck)
{
    return lastTimeCheck + new TimeSpan(0, 0, config.DataReadIntervalInSeconds) < DateTime.UtcNow;
}

async Task DisplayValuesAsync(CancellationTokenSource cancellationTokenSource)
{
    var lastTimeCheck = DateTime.UtcNow;
    while (!cancellationTokenSource.IsCancellationRequested)
    {
        if (shouldShowValues(lastTimeCheck))
        {
            Console.WriteLine(raspberryPi.Meteostation);
            lastTimeCheck = DateTime.UtcNow;
        }
    }

    return;
}

var token = new CancellationTokenSource();
Task.Run(() => DisplayValuesAsync(token));

Console.WriteLine("Click any key to exit...");
var key = Console.ReadKey(true);
token.Cancel();
Console.WriteLine("Thank you for usage...");
