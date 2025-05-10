using LinearAlgebra.Classes;
using System.Text;

namespace Presentation;

public static class EquationConverter
{
    private const string _spacingBetweenMatrixRows = "[1ex]";
    public static string ConvertFractionToLaTeX(this Fraction fraction)
    {
        if (Settings.EquationSettings.DiagonalFractions)
        {
            return fraction.ToString();
        }

        char sign = ' ';
        if (Settings.EquationSettings.ShowNegativeBesideTheFractionBar && fraction.Numerator < 0)
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
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
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
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToLaTeX()))} & {coefficient[i].ConvertFractionToLaTeX()} \\\\{_spacingBetweenMatrixRows}");
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
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToLaTeX()))} & {string.Join(" & ", otherMatrix[i].Select(f => f.ConvertFractionToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertToLaTeX<T>(this T number)
    {
        if (number is Fraction fraction)
        {
            return fraction.ConvertFractionToLaTeX();
        }

        string str = number?.ToString() ?? string.Empty;
        return str.Replace("[", "\\lbrack").Replace("]", "\\rbrack").Replace("*", "\\times");
    }

    public static string ConvertMatrixToLaTeX<T>(this T[][] matrix)
    {
        if (matrix is Fraction[][] matrixFrac)
        {
            return ConvertFractionMatrixToLaTeX(matrixFrac);
        }

        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertAugmentedMatrixToLaTeX<T>(this T[][] matrix, T[] coefficient)
    {
        if (matrix is Fraction[][] matrixFrac && coefficient is Fraction[] coefficientFrac)
        {
            return ConvertAugmentedFractionMatrixToLaTeX(matrixFrac, coefficientFrac);
        }

        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|r}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToLaTeX()))} & {coefficient[i].ConvertToLaTeX()} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertTwoMatricesToLaTeX<T>(this T[][] matrix, T[][] otherMatrix)
    {
        if (matrix is Fraction[][] matrixFrac && otherMatrix is Fraction[][] otherMatrixFrac)
        {
            return ConvertTwoMatricesToLaTeX(matrixFrac, otherMatrixFrac);
        }

        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|{new string('r', otherMatrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToLaTeX()))} & {string.Join(" & ", otherMatrix[i].Select(f => f.ConvertToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }
}