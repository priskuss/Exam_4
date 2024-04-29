using WeatherLog;

class Test
{
    public static void TestCalculateTemperatureDifference()
    {
        TestTemperatureDifference(UserInteraction.CalculateTemperatureDifference, 25.0, 20.0, 5.0, Constant.TestCalculateTemperatureDifferencePassed, Constant.TestCalculateTemperatureDifferenceFailed);
    }

    public static void TestCalculateTemperatureDifferenceWithNegativeValues()
    {
        TestTemperatureDifference(UserInteraction.CalculateTemperatureDifference, -10.0, -15.0, 5.0, Constant.TestCalculateTemperatureDifferenceWithNegativeValuesPassed, Constant.TestCalculateTemperatureDifferenceWithNegativeValuesFailed);
    }

    public static void TestCreateDataObject()
    {
        TestCreateData(UserInteraction.CreateDataObject, 25.0, 20.0, 5.0, Constant.TestCreateDataObjectPassed, Constant.TestCreateDataObjectFailed);
    }

    public static void TestTemperatureDifference(Func<double, double, double> calculateTemperatureDifference, double userTemperature, double YRData, double expectedDifference, string successMessage, string failureMessage)
    {
        double actualDifference = calculateTemperatureDifference(userTemperature, YRData);

        if (Math.Abs(actualDifference - expectedDifference) < 0.01)
        {
            Console.WriteLine(successMessage);
        }
        else
        {
            Console.WriteLine(failureMessage, expectedDifference, actualDifference);
        }
    }

    public static void TestCreateData(Func<double, double, double, DateData> createDataObject, double userTemperature, double YRData, double temperatureDifference, string successMessage, string failureMessage)
    {
        DateTime currentDate = DateTime.UtcNow.Date;
        DateData actualData = createDataObject(userTemperature, YRData, temperatureDifference);

        if (actualData.UserTemperature == userTemperature && actualData.YRData == YRData && actualData.TemperatureDifference == temperatureDifference && actualData.Date.Date == currentDate)
        {
            Console.WriteLine(successMessage);
        }
        else
        {
            Console.WriteLine(failureMessage);
        }
    }
}