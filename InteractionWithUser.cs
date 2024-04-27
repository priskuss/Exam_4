using System.Text.Json;

namespace WeatherLog
{
    class UserInteraction
    {
        public static async Task<bool> IsDataForTodayAlreadySaved(string fileName)
        {
            List<DateData> dataList = await ReadDataFromFile(fileName);
            return dataList.Any(d => d.Date.Date == DateTime.Now.Date);
        }

        public static async Task<List<DateData>> ReadDataFromFile(string fileName)
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

        public static async Task InteractWithUserAsync()
        {
            if (await IsDataForTodayAlreadySaved(Constant.LogFile))
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
                        DisplayTemperatureData(userTemperature, YRData.Value);
                        await SaveTemperatureData(userTemperature, YRData.Value, Constant.LogFile);
                    }
                    break;
                }
            }
        }

        public static bool GetUserConfirmation(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(Constant.Note);
            string saveDataResponse = Console.ReadLine();
            return saveDataResponse.ToLower() == Constant.Yes;
        }

        public static void DisplayTemperatureData(double userTemperature, double YRData)
        {
            Console.Clear();
            Console.WriteLine($"{nameof(Constant.UserTemperature)}: {userTemperature}");
            Console.WriteLine($"{nameof(Constant.YRTemperature)}: {YRData}");

            double temperatureDifference = CalculateTemperatureDifference(userTemperature, YRData);
            Console.WriteLine($"{nameof(Constant.TemperatureDifference)}: {temperatureDifference}");
            CompareTemperatures(userTemperature, YRData);
            Thread.Sleep(5000);

            Console.Clear();
            string prediction = Predictions.GetPrediction(temperatureDifference);
            Console.WriteLine(prediction);
        }

        public static async Task SaveTemperatureData(double userTemperature, double YRData, string fileName)
        {
            DateData data = CreateDataObject(userTemperature, YRData, CalculateTemperatureDifference(userTemperature, YRData));
            await AppendDataToFileAsync(fileName, data);
        }

        public static double GetUserTemperature()
        {
            double temperature;
            while (true)
            {
                Console.Write(Constant.EnterTemperature);
                string input = Console.ReadLine();
                if (double.TryParse(input, out temperature))
                {
                    break;
                }
                Console.WriteLine(Constant.InvalidInput);
            }
            return temperature;
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

        public static double CalculateTemperatureDifference(double userTemperature, double YRData)
        {
            double difference = userTemperature - YRData;
            return Math.Round(Math.Abs(difference), 1);
        }

        public static DateData CreateDataObject(double userTemperature, double YRData, double temperatureDifference)
        {
            return new DateData
            {
                Date = DateTime.Now,
                UserTemperature = userTemperature,
                YRData = YRData,
                TemperatureDifference = temperatureDifference
            };
        }

        public static async Task AppendDataToFileAsync(string fileName, DateData data)
        {
            if (await IsDataForTodayAlreadySaved(fileName))
            {
                Console.WriteLine(Constant.DataSaved);
                return;
            }

            List<DateData> dataList = await ReadDataFromFile(fileName);
            dataList.Add(data);

            string newDataJson = JsonSerializer.Serialize(dataList);
            await File.WriteAllTextAsync(fileName, newDataJson);
        }

        public class DateData
        {
            public DateTime Date { get; set; }
            public double UserTemperature { get; set; }
            public double YRData { get; set; }
            public double TemperatureDifference { get; set; }
        }
    }
}
