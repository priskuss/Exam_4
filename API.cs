using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace WeatherLog
{
    class API
    {
        private const string BaseUrl = Constant.BaseUrl;
        private const string UserAgent = Constant.UserAgent;

        public static async Task<double?> GetWeatherDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
                HttpResponseMessage responseMessage = await client.GetAsync(BaseUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    string response = await responseMessage.Content.ReadAsStringAsync();
                    JsonDocument data = JsonDocument.Parse(response);
                    JsonElement root = data.RootElement;
                    List<JsonElement> timeseries = root.GetProperty("properties").GetProperty("timeseries").EnumerateArray().ToList();

                    JsonElement currentWeatherData = timeseries[0];
                    double temperature = currentWeatherData.GetProperty("data").GetProperty("instant").GetProperty("details").GetProperty("air_temperature").GetDouble();

                    return temperature;
                }
                else
                {
                    string errorMessage = string.Format(Constant.Error, responseMessage.StatusCode, responseMessage.ReasonPhrase);
                    throw new Exception(errorMessage);
                }
            }
        }
    }
}
