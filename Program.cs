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
            //Test.TestCalculateTemperatureDifference();
            //Test.TestCalculateTemperatureDifferenceWithNegativeValues();
            //Test.TestCreateDataObject();
            await UserInteraction.InteractWithUserAsync();
        }

    }
}
