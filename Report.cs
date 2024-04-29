using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherLog
{
    class Report
    {
        public static async Task GenerateDailyReport(string fileName)
        {
            await GenerateReport(fileName, data => data.Where(d => d.Date.Date == DateTime.Now.Date), DisplayDailyData);
        }

        public static async Task GenerateWeeklyReport(string fileName)
        {
            await GenerateReport(fileName, data => data.Where(d => d.Date.Date >= DateTime.Now.Date.AddDays(-7)), DisplayAverageData, Constant.NumberSeven, Constant.NotEnoughDataWeekly);
        }

        public static async Task GenerateMonthlyReport(string fileName)
        {
            await GenerateReport(fileName, data => data.Where(d => d.Date.Date >= DateTime.Now.Date.AddMonths(-1)), DisplayAverageData, Constant.NumberTwentyNine, Constant.NotEnoughDataMonthly);
        }

        private static async Task GenerateReport(string fileName, Func<IEnumerable<UserInteraction.DateData>, IEnumerable<UserInteraction.DateData>> filter, Func<IEnumerable<UserInteraction.DateData>, string> display, int minCount = 0, string notEnoughDataMessage = null)
        {
            var dataList = await ReadDataFromFile(fileName);
            var filteredData = filter(dataList);

            if (filteredData.Count() < minCount)
            {
                Console.WriteLine(notEnoughDataMessage);
                return;
            }
            Console.WriteLine(display(filteredData));
        }

        private static string DisplayDailyData(IEnumerable<UserInteraction.DateData> data)
        {
            return string.Join(Environment.NewLine, data.Select(item => string.Format(Constant.UserTemperatureActualTemperature, item.UserTemperature, item.YRData, item.TemperatureDifference)));
        }

        private static string DisplayAverageData(IEnumerable<UserInteraction.DateData> data)
        {
            return string.Format(Constant.AverageTemperatureReport, Math.Round(data.Average(d => d.UserTemperature), 1), Math.Round(data.Average(d => d.YRData), 1), Math.Round(data.Average(d => d.TemperatureDifference), 1));
        }

        private static async Task<List<UserInteraction.DateData>> ReadDataFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string existingDataJson = await File.ReadAllTextAsync(fileName);
                if (!string.IsNullOrWhiteSpace(existingDataJson))
                {
                    return JsonSerializer.Deserialize<List<UserInteraction.DateData>>(existingDataJson);
                }
            }
            return new List<UserInteraction.DateData>();
        }
    }
}
