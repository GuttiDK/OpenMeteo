using OpenMeteo.Models;
using System.Text.Json;

namespace OpenMeteo
{

    public interface IDeserializer
    {
        Task<Root> DeserializeRootAsync(Uri baseAddress);
        Task<Root2> DeserializeRoot2Async(Uri baseAddress);
    }

    public class Deserializer : IDeserializer
    {

        public Deserializer() : base()
        {
        }

        public async Task<Root> DeserializeRootAsync(Uri baseAddress)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(baseAddress);
            string stringResult = await response.Content.ReadAsStringAsync();

            var weather = JsonSerializer.Deserialize<Root>(stringResult);
            return weather ?? throw new Exception("Failed to deserialize weather data");
        }

        public async Task<Root2> DeserializeRoot2Async(Uri baseAddress)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(baseAddress);
            string stringResult = await response.Content.ReadAsStringAsync();

            var weather = JsonSerializer.Deserialize<Root2>(stringResult);
            return weather ?? throw new Exception("Failed to deserialize weather data");
        }

    }
}
