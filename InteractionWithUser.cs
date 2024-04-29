using System.Text.Json;

class UserInteraction
{
    public static Func<string, Task<bool>> IsDataForTodayAlreadySavedAsync = async (fileName) =>
    {
        List<DateData> dataList = await ReadDataFromFileAsync(fileName);
        return dataList.Any(d => d.Date.Date == DateTime.UtcNow.Date);
    };

    public static Func<string, Task<List<DateData>>> ReadDataFromFileAsync = async (fileName) =>
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
    };

    public static async Task InteractWithUserAsync()
    {
        try
        {
            if (await IsDataForTodayAlreadySavedAsync(Constant.LogFile))
            {
                Console.WriteLine(Constant.DataIsAlreadySavedNoNewData);
                return;
            }

            Console.WriteLine(Constant.LuckyToGuess);

            while (true)
            {
                double userTemperature = GetUserTemperature();
                Console.Clear();

                if (GetUserConfirmation(Constant.SaveData))
                {
                    double? YRData = await API.GetWeatherDataAsync();
                    if (YRData.HasValue)
                    {
                        await ProcessUserTemperatureAsync(userTemperature, YRData.Value);
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorInteractingUser + ex.Message);
        }
    }

    private static async Task ProcessUserTemperatureAsync(double userTemperature, double YRData)
    {
        await DisplayTemperatureData(userTemperature, YRData);
        DateData data = CreateDataObject(userTemperature, YRData, CalculateTemperatureDifference(userTemperature, YRData));
        List<DateData> dataList = await ReadDataFromFileAsync(Constant.LogFile);
        dataList.Add(data);
        await SaveTemperatureDataAsync(userTemperature, YRData, Constant.LogFile);

        double temperatureDifference = CalculateTemperatureDifference(userTemperature, YRData);
        await Task.Delay(5000);
        Console.Clear();
        string prediction = await Predictions.GetPrediction(temperatureDifference);
        Console.WriteLine(prediction);

        await Task.Delay(5000);
        Console.Clear();
        Console.WriteLine(Constant.DoYouWantToSeeTheReport);
        string reportChoice = Console.ReadLine();

        await GenerateReportBasedOnUserChoiceAsync(reportChoice);
    }
    public static async Task SaveTemperatureDataAsync(double userTemperature, double YRData, string fileName)
    {
        DateData data = CreateDataObject(userTemperature, YRData, CalculateTemperatureDifference(userTemperature, YRData));
        await AppendDataToFileAsync(fileName, data);
    }
    private static async Task GenerateReportBasedOnUserChoiceAsync(string reportChoice)
    {
        switch (reportChoice)
        {
            case Constant.DailyReport:
                await Report.GenerateDailyReport(Constant.LogFile);
                break;
            case Constant.WeeklyReport:
                await Report.GenerateWeeklyReport(Constant.LogFile);
                break;
            case Constant.MonthlyReport:
                await Report.GenerateMonthlyReport(Constant.LogFile);
                break;
            case Constant.Exit:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine(Constant.InvalidChoice);
                break;
        }
    }

    public static bool GetUserConfirmation(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine(Constant.Note);
        string saveDataResponse = Console.ReadLine();
        return string.Equals(saveDataResponse, Constant.Yes, StringComparison.OrdinalIgnoreCase);
    }

    public static async Task DisplayTemperatureData(double userTemperature, double YRData)
    {
        Console.Clear();
        Console.WriteLine($"{nameof(Constant.UserTemperature)}: {userTemperature}");
        Console.WriteLine($"{nameof(Constant.YRTemperature)}: {YRData}");

        double temperatureDifference = CalculateTemperatureDifference(userTemperature, YRData);
        Console.WriteLine($"{nameof(Constant.TemperatureDifference)}: {temperatureDifference}");
        CompareTemperatures(userTemperature, YRData);
    }

    public static Func<string, DateData, Task> SaveDataToFileAsync = async (fileName, data) =>
    {
        List<DateData> dataList = await ReadDataFromFileAsync(fileName);
        List<DateData> newDataList = new List<DateData>(dataList) { data };

        string newDataJson = JsonSerializer.Serialize(newDataList);
        await File.WriteAllTextAsync(fileName, newDataJson);
    };

    public static double GetUserTemperature()
    {
        while (true)
        {
            Console.Write(Constant.EnterTemperature);
            string input = Console.ReadLine();
            if (double.TryParse(input, out double temperature))
            {
                return temperature;
            }
            Console.WriteLine(Constant.InvalidInput);
        }
    }

    public static void CompareTemperatures(double userTemperature, double YRData)
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

    public static Func<double, double, double> CalculateTemperatureDifference = (userTemperature, YRData) =>
    {
        double difference = userTemperature - YRData;
        return Math.Round(Math.Abs(difference), 1);
    };

    public static Func<double, double, double, DateData> CreateDataObject = (userTemperature, YRData, temperatureDifference) =>
    {
        return new DateData
        {
            Date = DateTime.UtcNow,
            UserTemperature = userTemperature,
            YRData = YRData,
            TemperatureDifference = temperatureDifference
        };
    };

    public static async Task AppendDataToFileAsync(string fileName, DateData data)
    {
        if (await IsDataForTodayAlreadySavedAsync(fileName))
        {
            Console.WriteLine(Constant.DataSaved);
            return;
        }

        List<DateData> dataList = await ReadDataFromFileAsync(fileName);
        dataList.Add(data);

        string newDataJson = JsonSerializer.Serialize(dataList);
        await File.WriteAllTextAsync(fileName, newDataJson);
    }
}
public class DateData
{
    public DateTime Date { get; set; }
    public double UserTemperature { get; set; }
    public double YRData { get; set; }
    public double TemperatureDifference { get; set; }
}
