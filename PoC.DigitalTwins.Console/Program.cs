// See https://aka.ms/new-console-template for more information
using PoC.DigitalTwins.RaspberryPi;
using PoC.DigitalTwins.RaspberryPi.Models;

Console.WriteLine("Welcome in PoC - DigitalTwins");
Console.WriteLine("Press any key for next line or ESC for termination");

IRaspberryConfig config = new RaspberryConfigBuilder().Build();
var raspberryPi = new Setup(config);

bool isESC = false;
while (!isESC)
{
    Console.WriteLine(raspberryPi.Meteostation);
    var key = Console.ReadKey();
    isESC = ConsoleKey.Escape == key.Key;
}

Console.WriteLine("Thank you for usage...");
