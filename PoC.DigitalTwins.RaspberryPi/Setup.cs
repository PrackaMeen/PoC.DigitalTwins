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
            DHP11 = new DHP11();
        }

        public DHP11 DHP11 { get; set; }
    }
}
