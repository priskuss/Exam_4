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
            await UserInteraction.InteractWithUserAsync();
        }
    }
}
