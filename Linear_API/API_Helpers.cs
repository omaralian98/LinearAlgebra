using LinearAlgebra.Classes;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace Linear_API;
public static class API_Helpers
{
    private const decimal Min = -9;
    private const decimal Max = 9;
    public static string[] API_GenerateString(int length, bool IntegersOnly = false, decimal min = Min, decimal max = Max, bool simplify = true, bool preferInteger = false)
    {
        if (max - min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }
        string[] matrix = new string[length];
        Random rand = new();
        int minInt = Convert.ToInt32(min);
        int maxInt = Convert.ToInt32(max);
        for (int i = 0; i < length; i++)
        {
            if (IntegersOnly) matrix[i] = rand.Next(minInt, maxInt).ToString();
            else matrix[i] = Fraction.GenerateRandomFraction(min, max, simplify, preferInteger).ToString();
        }
        return matrix;
    }
    public static decimal[] API_GenerateDecimal(int length, bool IntegersOnly = false, decimal min = Min, decimal max = Max, bool simplify = true, bool preferInteger = false)
    {
        if (max - min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }
        decimal[] matrix = new decimal[length];
        Random rand = new();
        int minInt = Convert.ToInt32(min);
        int maxInt = Convert.ToInt32(max);
        for (int i = 0; i < length; i++)
        {
            if (IntegersOnly) matrix[i] = rand.Next(minInt, maxInt);
            else matrix[i] = (decimal)Fraction.GenerateRandomFraction(min, max, simplify, preferInteger);
        }
        return matrix;
    }
}
