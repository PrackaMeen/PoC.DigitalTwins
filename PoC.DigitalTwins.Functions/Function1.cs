// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
namespace PoC.DigitalTwins.Functions
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Host;
    using Microsoft.Azure.EventGrid.Models;
    using Microsoft.Azure.WebJobs.Extensions.EventGrid;
    using Microsoft.Extensions.Logging;

    // new using outside of template:
    using Azure.DigitalTwins.Core;
    using Azure.Identity;
    using System.Net.Http;
    using Azure.Core.Pipeline;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Azure;
    using System.Threading.Tasks;

    public static class Function1
    {
        const string ENVIRONMENT_VARIABLE_ADT_SERVICE_URL = "ADT_SERVICE_URL";
        private static readonly string adtInstanceUrl = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_ADT_SERVICE_URL);
        private static readonly HttpClient httpClient = HttpClientFactory.Create();

        [FunctionName("IoTHub2DigitalTwins")]
        public static async Task Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            if (adtInstanceUrl == null)
            {
                log.LogError($"Application settings \"{ENVIRONMENT_VARIABLE_ADT_SERVICE_URL}\"");
            }

            try
            {
                log.LogInformation(eventGridEvent.Data.ToString());
                var credentials = new ManagedIdentityCredential("https://DigitalTwins.azure.net");

                var newDigitalTwinsClient = new DigitalTwinsClient(
                    new Uri(adtInstanceUrl),
                    credentials,
                    new DigitalTwinsClientOptions()
                    {
                        Transport = new HttpClientTransport(httpClient)
                    }
                );

                log.LogInformation("ADT service client connection created.");

                if (eventGridEvent != null && eventGridEvent.Data != null)
                {
                    var jsonDataObject = eventGridEvent.Data.ToString();
                    log.LogInformation(jsonDataObject);

                    // convert the message into a json object
                    JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(jsonDataObject);

                    // get our device id, temperature and humidity from the object
                    string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                    var temperature = deviceMessage["body"]["Temperature"];
                    var humidity = deviceMessage["body"]["Humidity"];

                    // log the temperature and humidity
                    log.LogInformation($"Device: {deviceId} Temperature: {temperature} Humidity: {humidity}");

                    // update twin with temperature and humidity from our RaspberryPi
                    var updateTwinData = new JsonPatchDocument();
                    updateTwinData.AppendReplace("/Temperature", temperature.Value<double>());
                    updateTwinData.AppendReplace("/Humidity", humidity.Value<double>());
                    await newDigitalTwinsClient.UpdateDigitalTwinAsync(deviceId, updateTwinData);
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
            }
        }
    }
}
