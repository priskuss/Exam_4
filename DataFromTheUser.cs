namespace WeatherLog
{
    class UserData
    {
        public static double GetUserTemperature()
        {
            Console.Write("Enter your temperature measurement: ");
            var input = Console.ReadLine();
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");
            }
            return double.Parse(input);
        }
    }
}