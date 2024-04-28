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
            var dataList = await ReadDataFromFile(fileName);
            var todayData = dataList.Where(d => d.Date.Date == DateTime.Now.Date);

            DisplayReport(todayData, Constant.DailyReportTitle);
        }

        public static async Task GenerateWeeklyReport(string fileName)
        {
            var dataList = await ReadDataFromFile(fileName);
            var weekData = dataList.Where(d => d.Date.Date >= DateTime.Now.Date.AddDays(-7));

            if (weekData.Count() < Constant.NumberSeven)
            {
                Console.WriteLine(Constant.NotEnoughDataWeekly);
                return;
            }

            DisplayReport(weekData, Constant.WeeklyReportTitle);
        }

        public static async Task GenerateMonthlyReport(string fileName)
        {
            var dataList = await ReadDataFromFile(fileName);
            var monthData = dataList.Where(d => d.Date.Date >= DateTime.Now.Date.AddMonths(-1));

            if (monthData.Count() < Constant.NumberTwentyNine)
            {
                Console.WriteLine(Constant.NotEnoughDataMonthly);
                return;
            }

            DisplayReport(monthData, Constant.MonthlyReportTitle);
        }

        private static void DisplayReport(IEnumerable<UserInteraction.DateData> data, string reportTitle)
        {
            Console.WriteLine(reportTitle);

            if (reportTitle == Constant.DailyReportTitle)
            {
                foreach (var item in data)
                {
                    Console.WriteLine(Constant.TodayIs, item.Date);
                    Console.WriteLine(string.Format(Constant.UserTemperatureActualTemperature, item.UserTemperature, item.YRData, item.TemperatureDifference));
                }
            }
            else
            {
                Console.WriteLine(string.Format(Constant.UserTemperatureActualTemperature, Math.Round(data.Average(d => d.UserTemperature), 1), Math.Round(data.Average(d => d.YRData), 1), Math.Round(data.Average(d => d.TemperatureDifference), 1)));
            }
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