using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

class Report
{
    public static async Task GenerateDailyReport(string fileName)
    {
        try
        {
            await GenerateReport(fileName, data => data.Where(d => d.Date.Date == DateTime.Now.Date), DisplayDailyData);
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorDisplayingDailyReport + ex.Message);
        }
    }

    public static async Task GenerateWeeklyReport(string fileName)
    {
        try
        {
            await GenerateReport(fileName, data => data.Where(d => d.Date.Date >= DateTime.Now.Date.AddDays(-7)), data => DisplayAverageData(data, Constant.WeeklyReportTitle), Constant.NumberSeven, Constant.NotEnoughDataWeekly);
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorDisplayingWeeklyReport + ex.Message);
        }
    }

    public static async Task GenerateMonthlyReport(string fileName)
    {
        try
        {
            await GenerateReport(fileName, data => data.Where(d => d.Date.Date >= DateTime.Now.Date.AddMonths(-1)), data => DisplayAverageData(data, Constant.MonthlyReportTitle), Constant.NumberTwentyNine, Constant.NotEnoughDataMonthly);
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorDisplayingMonthlyReport + ex.Message);
        }
    }

    private static async Task GenerateReport(string fileName, Func<IEnumerable<DateData>, IEnumerable<DateData>> filter, Func<IEnumerable<DateData>, string> display, int minCount = 0, string notEnoughDataMessage = null)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorGeneratingReport + ex.Message);
        }
    }

    private static string DisplayDailyData(IEnumerable<DateData> data)
    {
        try
        {
            return Constant.DailyReportTitle + Environment.NewLine + string.Join(Environment.NewLine, data.Select(item => string.Format(Constant.UserTemperatureActualTemperature, item.UserTemperature, item.YRData, item.TemperatureDifference)));
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorDisplayingDailyData + ex.Message);
            return string.Empty;
        }
    }

    private static string DisplayAverageData(IEnumerable<DateData> data, string reportTitle)
    {
        try
        {
            return reportTitle + Environment.NewLine + string.Format(Constant.AverageTemperatureReport, Math.Round(data.Average(d => d.UserTemperature), 1), Math.Round(data.Average(d => d.YRData), 1), Math.Round(data.Average(d => d.TemperatureDifference), 1));
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorDisplayingAverageData + ex.Message);
            return string.Empty;
        }
    }
    private static async Task<List<DateData>> ReadDataFromFile(string fileName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                string existingDataJson = await File.ReadAllTextAsync(fileName);
                if (!string.IsNullOrWhiteSpace(existingDataJson))
                {
                    return JsonSerializer.Deserialize<List<DateData>>(existingDataJson);
                }
            }
            return new List<DateData>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorReadingData + ex.Message);
            return new List<DateData>();
        }
    }
}
