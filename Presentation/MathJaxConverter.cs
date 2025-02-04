using LinearAlgebra.Classes;
using Presentation;
using System.Text;

public static class MathJaxConverter
{
    public static string ConvertFractionToMathJax(this Fraction fraction, Display config = Display.Inline)
    {
        if (Settings.MathJaxSettings.DiagonalFractions)
        {
            return WrapWithMathJax(fraction.ToString(), config);
        }

        char sign = ' ';
        if (Settings.MathJaxSettings.ShowNegativeBesideTheFractionBar && fraction.Numerator < 0)
        {
            sign = '-';
            fraction = new Fraction(-fraction.Numerator, fraction.Denominator);
        }

        string math = fraction.Denominator == 1 ? $"{sign}{fraction.Numerator}" : $@"{sign}\frac{{{fraction.Numerator}}}{{{fraction.Denominator}}}";
        return WrapWithMathJax(math, config);
    }

    public static string ConvertFractionMatrixToMathJax(this Fraction[][] matrix, Display display)
    {
        var math = new StringBuilder();
        math.AppendLine($@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToMathJax(Display.None)))}\\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine($@"\right]");

        return WrapWithMathJax(math.ToString(), display);
    }

    public static string ConvertAugmentedFractionMatrixToMathJax(this Fraction[][] matrix, Fraction[] coefficient, Display display)
    {
        var math = new StringBuilder();
        math.AppendLine($@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|r}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToMathJax(Display.None)))} & {coefficient[i].ConvertFractionToMathJax(Display.None)}\\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine($@"\right]");

        return WrapWithMathJax(math.ToString(), display);
    }

    public static string ConvertTwoFractionMatricesToMathJax(this Fraction[][] matrix, Fraction[][] otherMatrix, Display display)
    {
        var math = new StringBuilder();
        math.AppendLine($@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|{new string('r', otherMatrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertFractionToMathJax(Display.None)))} & {string.Join(" & ", otherMatrix[i].Select(f => f.ConvertFractionToMathJax(Display.None)))}\\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine($@"\right]");

        return WrapWithMathJax(math.ToString(), display);
    }


    private static string WrapWithMathJax(string content, Display config)
    {
        string start = GetWrapperStart(config);
        string end = GetWrapperEnd(config);

        return $"{start}{content}{end}";
    }

    private static string GetWrapperStart(Display config) => config switch
    {
        Display.Inline => "\\(",
        Display.Block => "\\[",
        _ => string.Empty,
    };

    private static string GetWrapperEnd(Display config) => config switch
    {
        Display.Inline => "\\)",
        Display.Block => "\\]",
        _ => string.Empty,
    };

    public static string ConvertToMathJax<T>(this T number, Display config = Display.Inline)
    {
        return WrapWithMathJax(number?.ToString() ?? string.Empty, config);
    }

    public static string ConvertMatrixToMathJax<T>(this T[][] matrix, Display display)
    {
        var math = new StringBuilder();
        math.AppendLine($@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToMathJax(Display.None)))}\\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine($@"\right]");

        return WrapWithMathJax(math.ToString(), display);
    }

    public static string ConvertAugmentedMatrixToMathJax<T>(this T[][] matrix, T[] coefficient, Display display)
    {
        var math = new StringBuilder();
        math.AppendLine($@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|r}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToMathJax(Display.None)))} & {coefficient[i].ConvertToMathJax(Display.None)}\\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine($@"\right]");

        return WrapWithMathJax(math.ToString(), display);
    }

    public static string ConvertTwoMatricesToMathJax<T>(this T[][] matrix, T[][] otherMatrix, Display display)
    {
        var math = new StringBuilder();
        math.AppendLine($@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}|{new string('r', otherMatrix[0].Length)}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToMathJax(Display.None)))} & {string.Join(" & ", otherMatrix[i].Select(f => f.ConvertToMathJax(Display.None)))}\\\\");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine($@"\right]");

        return WrapWithMathJax(math.ToString(), display);
    }
}