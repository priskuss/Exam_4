using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherLog
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userTemperature = WeatherLog.UserData.GetUserTemperature();
            Console.Clear();
            Console.WriteLine(Constant.UserTemperature, userTemperature);

            var YRData = await API.GetWeatherDataAsync();
            if (YRData.HasValue)
            {
                Console.WriteLine(Constant.YRTemperature, YRData.Value);

                var temperatureDifference = CalculateTemperatureDifference(userTemperature, YRData.Value);
                Console.WriteLine(Constant.TemperatureDifference, temperatureDifference);

                CompareTemperatures(userTemperature, YRData.Value);

                var data = CreateDataObject(userTemperature, YRData.Value, temperatureDifference);

                await AppendDataToFileAsync(Constant.LogFile, data);
            }
        }

        static void CompareTemperatures(double userTemperature, double YRData)
        {
            if (userTemperature > YRData)
            {
                Console.WriteLine(Constant.UserHigher);
            }
            else if (userTemperature < YRData)
            {
                Console.WriteLine(Constant.UserLower);
            }
            else
            {
                Console.WriteLine(Constant.UserSame);
            }
        }
        static double CalculateTemperatureDifference(double userTemperature, double YRData)
        {
            var difference = userTemperature - YRData;
            return Math.Round(Math.Abs(difference), 1);
        }

        static object CreateDataObject(double userTemperature, double YRData, double temperatureDifference)
        {
            return new
            {
                Date = DateTime.Now,
                UserTemperature = userTemperature,
                YRData = YRData,
                TemperatureDifference = temperatureDifference
            };
        }

        static async Task AppendDataToFileAsync(string fileName, object data)
        {
            var existingData = await File.ReadAllTextAsync(fileName);

            var dataList = string.IsNullOrEmpty(existingData)
                ? new List<object>()
                : JsonSerializer.Deserialize<List<object>>(existingData);

            dataList.Add(data);

            var json = JsonSerializer.Serialize(dataList);

            await File.WriteAllTextAsync(fileName, json);
        }
    }
}
