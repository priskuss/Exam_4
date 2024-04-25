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
            double userTemperature = UserInteraction.GetUserTemperature();
            Console.Clear();
            Console.WriteLine(Constant.UserTemperature, userTemperature);

            Nullable<double> YRData = await API.GetWeatherDataAsync();
            if (YRData.HasValue)
            {
                Console.WriteLine(Constant.YRTemperature, YRData.Value);

                double temperatureDifference = UserInteraction.CalculateTemperatureDifference(userTemperature, YRData.Value);
                Console.WriteLine(Constant.TemperatureDifference, temperatureDifference);

                UserInteraction.CompareTemperatures(userTemperature, YRData.Value);

                object data = UserInteraction.CreateDataObject(userTemperature, YRData.Value, temperatureDifference);

                await AppendDataToFileAsync(Constant.LogFile, data);
            }
        }

        static async Task AppendDataToFileAsync(string fileName, object data)
        {
            string existingData = await File.ReadAllTextAsync(fileName);

            List<object> dataList = string.IsNullOrEmpty(existingData)
                ? new List<object>()
                : JsonSerializer.Deserialize<List<object>>(existingData);

            dataList.Add(data);

            string json = JsonSerializer.Serialize(dataList);

            await File.WriteAllTextAsync(fileName, json);
        }
    }
}
