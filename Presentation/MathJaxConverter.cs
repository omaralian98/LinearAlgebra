using LinearAlgebra.Classes;
using Presentation;
using System.Text;

public static class MathJaxConverter
{
    public static string ConvertToMathJax(this Fraction fraction, Display config = Display.Inline)
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

    public static string ConvertToMathJax(this Fraction[][] matrix, Display display)
    {
        return ConvertToMathJax(matrix, display, null);
    }

    public static string ConvertToMathJax(this Fraction[][] matrix, Fraction[] coefficient, Display display)
    {
        return ConvertToMathJax(matrix, display, coefficient);
    }

    private static string ConvertToMathJax(this Fraction[][] matrix, Display display, Fraction[]? coefficient = null)
    {
        var math = new StringBuilder();
        math.AppendLine($@"\left[");
        string style = coefficient is not null ? "|r" : string.Empty;
        math.AppendLine($@"\begin{{array}}{{{new string('r', matrix[0].Length)}{style}}}");

        for (int i = 0; i < matrix.Length; i++)
        {
            math.Append($"{string.Join(" & ", matrix[i].Select(f => f.ConvertToMathJax(Display.None)))}");
            string styleToAdd = coefficient is not null ? $" & {coefficient[i].ConvertToMathJax(Display.None)}" : string.Empty;
            math.Append($"{styleToAdd}\\\\");
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
}