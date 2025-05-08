using Presentation;
using System.Text;

public static class EquationConverter
{
    public static string ConvertFractionToLaTeX(this Fraction fraction)
    {
        if (Settings.MathJaxSettings.DiagonalFractions)
        {
            return fraction.ToString();
        }

        char sign = ' ';
        if (Settings.MathJaxSettings.ShowNegativeBesideTheFractionBar && fraction.Numerator < 0)
        {
            sign = '-';
            fraction = new Fraction(-fraction.Numerator, fraction.Denominator);
        }

        return fraction.Denominator == 1
            ? $"{sign}{fraction.Numerator}"
            : $@"{sign}\frac{{{fraction.Numerator}}}{{{fraction.Denominator}}}";
    }

    public static string ConvertFractionMatrixToLaTeX(this Fraction[][] matrix)
    {
        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToLaTeX()))} \\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertAugmentedFractionMatrixToLaTeX(this Fraction[][] matrix, Fraction[] coefficient)
    {
        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|r}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToLaTeX()))} & {coefficient[i].ConvertFractionToLaTeX()} \\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertTwoFractionMatricesToLaTeX(this Fraction[][] matrix, Fraction[][] otherMatrix)
    {
        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|{new string('r', otherMatrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToLaTeX()))} & {string.Join(" & ", otherMatrix[i].Select(f => f.ConvertFractionToLaTeX()))} \\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertToLaTeX<T>(this T number)
    {
        if (number is Fraction fraction)
        {
            return ConvertFractionToLaTeX(fraction);
        }

        string str = number?.ToString() ?? string.Empty;
        return str.Replace("[", "\\lbrack").Replace("]", "\\rbrack").Replace("*", "\\times");
    }

    public static string ConvertMatrixToLaTeX<T>(this T[][] matrix)
    {
        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToLaTeX()))} \\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertAugmentedMatrixToLaTeX<T>(this T[][] matrix, T[] coefficient)
    {
        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|r}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToLaTeX()))} & {coefficient[i].ConvertToLaTeX()} \\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertTwoMatricesToLaTeX<T>(this T[][] matrix, T[][] otherMatrix)
    {
        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|{new string('r', otherMatrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToLaTeX()))} & {string.Join(" & ", otherMatrix[i].Select(f => f.ConvertToLaTeX()))} \\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }
}
