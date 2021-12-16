// See https://aka.ms/new-console-template for more information
using PoC.DigitalTwins.Console;
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

var raspberryConfig = config?.RaspberryPi ?? new RaspberryConfigBuilder().Build();

Setup raspberryPi = new(raspberryConfig);

bool isESC = false;
while (!isESC)
{
    Console.WriteLine(raspberryPi.Meteostation);
    var key = Console.ReadKey();
    isESC = ConsoleKey.Escape == key.Key;
}

Console.WriteLine("Thank you for usage...");
