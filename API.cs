using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace WeatherLog
{
    public class YRData
    {
        public Properties properties { get; set; }
    }
    public class Properties
    {
        public List<Timeseries> timeseries { get; set; }
    }
    public class Timeseries
    {
        public Data data { get; set; }
    }
    public class Data
    {
        public Instant instant { get; set; }
    }
    public class Instant
    {
        public Details details { get; set; }
    }
    public class Details
    {
        public double air_temperature { get; set; }
    }

    class API
    {
        private const string BaseUrl = Constant.BaseUrl;
        private const string UserAgent = Constant.UserAgent;

        public static async Task<double?> GetWeatherDataAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
                var responseMessage = await client.GetAsync(BaseUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var YRData = JsonSerializer.Deserialize<YRData>(response);
                    var temperature = YRData.properties.timeseries[0].data.instant.details.air_temperature;

                    return temperature;
                }
                else
                {
                    var errorMessage = string.Format(Constant.Error, responseMessage.StatusCode, responseMessage.ReasonPhrase);
                    throw new Exception(errorMessage);
                }
            }
        }
    }
}
