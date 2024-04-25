namespace WeatherLog
{
    class UserInteraction
    {
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

        public static object CreateDataObject(double userTemperature, double YRData, double temperatureDifference)
        {
            return new
            {
                Date = DateTime.Now,
                UserTemperature = userTemperature,
                YRData = YRData,
                TemperatureDifference = temperatureDifference
            };
        }
    }
}