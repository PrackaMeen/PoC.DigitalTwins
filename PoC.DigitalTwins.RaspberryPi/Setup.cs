namespace PoC.DigitalTwins.RaspberryPi
{
    using PoC.DigitalTwins.RaspberryPi.Components.Mocks;

    public class Setup
    {
        public Setup()
        {
            DHT11 = new DHT11();
        }

        public DHT11 DHT11 { get; }
    }
}
