using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace WeatherLog
{
    class Program
    {
        private const string UserAgent = "MyTestApp/0.1";
        private const string BaseUrl = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat=60.10&lon=9.58";

        static async Task Main(string[] args)
        {
            var userTemperature = WeatherLog.UserData.GetUserTemperature();
            Console.Clear();
            Console.WriteLine($"User temperature: {userTemperature}");

            var YRData = await API.GetWeatherDataAsync();
            if (YRData.HasValue)
            {
                Console.WriteLine($"Current temperature: {YRData.Value}");

                CompareTemperatures(userTemperature, YRData.Value);

                var data = CreateDataObject(userTemperature, YRData.Value);

                var existingData = await File.ReadAllTextAsync("log.json");

                var dataList = string.IsNullOrEmpty(existingData)
                    ? new List<object>()
                    : new List<object> { JsonSerializer.Deserialize<dynamic>(existingData) };

                dataList.Add(data);

                var json = JsonSerializer.Serialize(dataList);

                await File.WriteAllTextAsync("log.json", json);
            }
        }
        static void CompareTemperatures(double userTemperature, double YRData)
        {
            if (userTemperature > YRData)
            {
                Console.WriteLine("Your temperature is higher than YR temperature.");
            }
            else if (userTemperature < YRData)
            {
                Console.WriteLine("Your temperature is lower than YR temperature.");
            }
            else
            {
                Console.WriteLine("Your temperature is the same as YR temperature.");
            }
        }

        static object CreateDataObject(double userTemperature, double YRData)
        {
            return new
            {
                Date = DateTime.Now,
                UserTemperature = userTemperature,
                YRData = YRData
            };
        }
    }
}