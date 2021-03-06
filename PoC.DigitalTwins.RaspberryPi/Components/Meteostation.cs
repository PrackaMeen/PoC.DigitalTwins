namespace PoC.DigitalTwins.RaspberryPi.Components;
using PoC.DigitalTwins.RaspberryPi.Components.Abstractions;
using System;
using System.Device.Gpio;
using Iot.Device.DHTxx;
using UnitsNet.Units;

internal class Meteostation : IMeteostation
{
    protected Dht11 _dht11;

    public Meteostation(GpioController gpioController, int pinDht11)
    {
        _dht11 = new Dht11(
            pinDht11,
            pinNumberingScheme: PinNumberingScheme.Logical,
            gpioController,
            shouldDispose: false);
    }

    public double GetHumidity()
    {
        return _dht11.Humidity.As(RelativeHumidityUnit.Percent);
    }

    public double GetTemperature()
    {
        return _dht11.Temperature.As(TemperatureUnit.DegreeCelsius);
    }

    public override string ToString()
    {
        var formattedTemperature = String.Format("{0:0.00}", GetTemperature());
        var formattedHumidity = String.Format("{0:0.00}", GetHumidity());
        return $"Temperature: {formattedTemperature}°C, Humidity: {formattedHumidity}%";
    }
}
