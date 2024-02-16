namespace LinearAlgebra.Classes;

public partial struct Fraction
{
    private static double GCD(double a, double b)
    {
        if (a == 1 || b == 1) return 1;
        while (a > 0 && b > 0)
        {
            if (a > b) a %= b;
            else b %= a;
        }
        if (a == 0) return b;
        return a;
    }

    public readonly Fraction GetAbs() => new (Math.Abs(Numerator), Denominator);
    public readonly decimal GetDecimalAbs() => Math.Abs(Quotient);


    public readonly string ValueToString() => Quotient.ToString();

    public override string ToString()
    {
        if (Numerator == 0) return "0";
        if (Denominator == 1) return Numerator.ToString();
        else if (!Quotient.IsDecimal()) return Quotient.ToString();
        return $"{Numerator}/{Denominator}";
    }

    private const decimal Min = -9;
    private const decimal Max = 9;
    public static Fraction[,] GenerateRandomMatrix(int row, int column = 1, bool IntegersOnly = false, decimal min = Min, decimal max = Max, bool simplify = true, bool preferInteger = false)
    {
        if (max - min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }
        Fraction[,] matrix = new Fraction[row, column];
        Random rand = new();
        int minInt = Convert.ToInt32(min);
        int maxInt = Convert.ToInt32(max);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (IntegersOnly) matrix[i, j] = rand.Next(minInt, maxInt);
                else matrix[i, j] = GenerateRandomFraction(min, max, simplify, preferInteger);
            }
        }
        return matrix;
    }

    public static Fraction GenerateRandomFraction(decimal min = Min, decimal max = Max, bool simplify = true, bool preferInteger = false)
    {
        decimal range = max - min;
        if (range < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }

        Random rand = new();
        var randomValue = rand.NextDouble() * (double)range + (double)min;
        if (preferInteger && rand.Next() % 2 == 0)
        {
            randomValue = Math.Round(randomValue, 0);
        }
        else if (simplify)
        {
            randomValue = Math.Round(randomValue, 1);
        }
        return ConvertDecimalToFraction(randomValue);
    }

    public static Fraction ConvertDecimalToFraction(double value, double accuracy = 0.000001)
    {
        if (accuracy <= 0.0 || accuracy >= 1.0)
        {
            throw new ArgumentOutOfRangeException(nameof(accuracy), "Must be > 0 and < 1.");
        }

        int sign = Math.Sign(value);

        if (sign == -1)
        {
            value = Math.Abs(value);
        }

        // Accuracy is the maximum relative error; convert to absolute maxError
        double maxError = sign == 0 ? accuracy : value * accuracy;

        int n = (int)Math.Floor(value);
        value -= n;

        if (value < maxError)
        {
            return new Fraction(sign * n, 1);
        }

        if (1 - maxError < value)
        {
            return new Fraction(sign * (n + 1), 1);
        }

        // The lower fraction is 0/1
        int lower_n = 0;
        int lower_d = 1;

        // The upper fraction is 1/1
        int upper_n = 1;
        int upper_d = 1;

        while (true)
        {
            // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
            int middle_n = lower_n + upper_n;
            int middle_d = lower_d + upper_d;

            if (middle_d * (value + maxError) < middle_n)
            {
                // real + error < middle : middle is our new upper
                upper_n = middle_n;
                upper_d = middle_d;
            }
            else if (middle_n < (value - maxError) * middle_d)
            {
                // middle < real - error : middle is our new lower
                lower_n = middle_n;
                lower_d = middle_d;
            }
            else
            {
                // Middle is our best fraction
                return new Fraction((n * middle_d + middle_n) * sign, middle_d);
            }
        }
    }

    public static bool TryParse(string value, out Fraction result)
    {
        try
        {
            result = (Fraction)value;
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
    public static bool TryParse(string value, out Fraction result, Fraction defaultValue)
    {
        try
        {
            result = (Fraction)value;
            return true;
        }
        catch
        {
            result = defaultValue;
            return false;
        }
    }

}