using System;
using System.Collections.Generic;

public static class Predictions
{
    private static readonly Random Random = new Random();

    private static readonly Dictionary<int, List<string>> PredictionsByTemperatureDifference = new Dictionary<int, List<string>>
    {
        { Constant.NumberOne, new List<string> { Constant.PositivePredictionOne, Constant.PositivePredictionTwo, Constant.PositivePredictionThree, Constant.PositivePredictionFour, Constant.PositivePredictionFive, Constant.PositivePredictionSix } },
        { Constant.NumberThree, new List<string> { Constant.FunnyPredictionOne, Constant.FunnyPredictionTwo, Constant.FunnyPredictionThree, Constant.FunnyPredictionFour, Constant.FunnyPredictionFive } },
        { Constant.NumberFive, new List<string> { Constant.NeutralPredictionOne, Constant.NeutralPredictionTwo, Constant.NeutralPredictionThree, Constant.NeutralPredictionFour, Constant.NeutralPredictionFive, Constant.NeutralPredictionSix, Constant.NeutralPredictionSeven } },
        { Constant.NumberHundred, new List<string> { Constant.WarningPredictionOne, Constant.WarningPredictionTwo, Constant.WarningPredictionThree, Constant.WarningPredictionFour, Constant.WarningPredictionFive, Constant.WarningPredictionSix } }
    };

    public static Func<double, Task<string>> GetPrediction = async (temperatureDifference) =>
    {
        try
        {
            foreach (var prediction in PredictionsByTemperatureDifference)
            {
                if (temperatureDifference < prediction.Key)
                {
                    Console.WriteLine(Constant.OpeningLine);
                    await Task.Delay(5000);
                    Console.Clear();
                    return prediction.Value[Random.Next(prediction.Value.Count)];
                }
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine(Constant.ErrorGettingPrediction + ex.Message);
            return string.Empty;
        }
    };
}
