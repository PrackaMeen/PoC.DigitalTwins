// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome in PoC - DigitalTwins");
Console.WriteLine("Press any key for next line or ESC for termination");

var raspberryPi = new PoC.DigitalTwins.RaspberryPi.Setup();

bool isESC = false;
while (!isESC)
{
    Console.WriteLine(raspberryPi.DHT11);
    var key = Console.ReadKey();
    isESC = ConsoleKey.Escape == key.Key;
}

Console.WriteLine("Thank you for usage...");
