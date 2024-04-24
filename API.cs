using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;

namespace WeatherLog
{
    class API
    {
        private const string BaseUrl = "https://api.met.no/weatherapi/nowcast/2.0/complete?lat=59.9333&lon=10.7166";
        private const string UserAgent = "MyTestApp/0.1";

        public static async Task<double?> GetWeatherDataAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
                var responseMessage = await client.GetAsync(BaseUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var data = JsonDocument.Parse(response);
                    var root = data.RootElement;
                    var timeseries = root.GetProperty("properties").GetProperty("timeseries").EnumerateArray().ToList();

                    var currentWeatherData = timeseries[0];
                    var temperature = currentWeatherData.GetProperty("data").GetProperty("instant").GetProperty("details").GetProperty("air_temperature").GetDouble();

                    return temperature;
                }
                else
                {
                    throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
                }
            }
        }
    }
}
