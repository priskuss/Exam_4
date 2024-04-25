using System;

namespace WeatherLog
{
    class UserData
    {
        public static double GetUserTemperature()
        {
            double temperature;
            while (true)
            {
                Console.Write(Constant.EnterTemperature);
                var input = Console.ReadLine();
                if (double.TryParse(input, out temperature))
                {
                    break;
                }
                Console.WriteLine(Constant.InvalidInput);
            }
            return temperature;
        }
    }
}
