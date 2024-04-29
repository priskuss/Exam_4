using System;
using System.Collections.Generic;

public static class Predictions
{
    private static readonly Random Random = new Random();

    private static readonly Dictionary<int, List<string>> PredictionsByTemperatureDifference = new Dictionary<int, List<string>>
    {
        { Constant.NumberOne, new List<string> { Constant.PositivePredictionOne, Constant.PositivePredictionTwo, Constant.PositivePredictionThree } },
        { Constant.NumberThree, new List<string> { Constant.FunnyPredictionOne, Constant.FunnyPredictionTwo, Constant.FunnyPredictionThree, Constant.FunnyPredictionFour } },
        { Constant.NumberFive, new List<string> { Constant.NeutralPredictionOne, Constant.NeutralPredictionTwo, Constant.NeutralPredictionThree, Constant.NeutralPredictionFour } },
        { Constant.NumberHundred, new List<string> { Constant.WarningPredictionOne, Constant.WarningPredictionTwo, Constant.WarningPredictionThree, Constant.WarningPredictionFour } }
    };

    public static string GetPrediction(double temperatureDifference)
    {
        foreach (var prediction in PredictionsByTemperatureDifference)
        {
            if (temperatureDifference < prediction.Key)
            {
                return prediction.Value[Random.Next(prediction.Value.Count)];
            }
        }
        return string.Empty;
    }
}
