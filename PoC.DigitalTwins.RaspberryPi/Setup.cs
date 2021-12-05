using PoC.DigitalTwins.RaspberryPi.Components.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.DigitalTwins.RaspberryPi
{
    public class Setup
    {
        public Setup()
        {
            DHT11 = new DHT11();
        }

        public DHT11 DHT11 { get; set; }
    }
}
