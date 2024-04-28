using System;
using System.Collections.Generic;

public static class Predictions
{
    private static readonly Random Random = new Random();

    private static readonly List<string> PositivePredictions = new List<string>
        {
           Constant.PositivePredictionOne,
           Constant.PositivePredictionTwo,
           Constant.PositivePredictionThree
        };

    private static readonly List<string> FunnyPredictions = new List<string>
        {
            Constant.FunnyPredictionOne,
            Constant.FunnyPredictionTwo,
            Constant.FunnyPredictionThree,
            Constant.FunnyPredictionFour
        };

    private static readonly List<string> NeutralPredictions = new List<string>
        {
            Constant.NeutralPredictionOne,
            Constant.NeutralPredictionTwo,
            Constant.NeutralPredictionThree,
            Constant.NeutralPredictionFour
        };


    private static readonly List<string> WarningPredictions = new List<string>
        {
            Constant.WarningPredictionOne,
            Constant.WarningPredictionTwo,
            Constant.WarningPredictionThree,
            Constant.WarningPredictionFour
        };

    public static string GetPrediction(double temperatureDifference)
    {
        string prediction;

        if (temperatureDifference < Constant.NumberOne)
        {
            prediction = PositivePredictions[Random.Next(PositivePredictions.Count)];
        }
        else if (temperatureDifference < Constant.NumberThree)
        {
            prediction = FunnyPredictions[Random.Next(FunnyPredictions.Count)];
        }
        else if (temperatureDifference < Constant.NumberFive)
        {
            prediction = NeutralPredictions[Random.Next(NeutralPredictions.Count)];
        }
        else
        {
            prediction = WarningPredictions[Random.Next(WarningPredictions.Count)];
        }

        Console.WriteLine(Constant.OpeningLine);
        System.Threading.Thread.Sleep(4000);
        Console.Clear();
        return prediction;
    }
}