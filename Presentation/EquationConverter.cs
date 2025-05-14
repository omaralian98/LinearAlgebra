using LinearAlgebra;
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

    public static string ConvertFractionMatrixToLaTeX(this Fraction[,] matrix)
    {
        int columnsCount = matrix.GetLength(1);
        int rowsCount = matrix.GetLength(0);
        
        var math = new StringBuilder();

        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', columnsCount)}}}");

        for (int i = 0; i < rowsCount; i++)
        {
            math.Append($"{string.Join(" & ", matrix.GetRow(i).Select(f => f.ConvertFractionToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertAugmentedFractionMatrixToLaTeX(this Fraction[,] matrix, Fraction[] coefficient)
    {
        int columnsCount = matrix.GetLength(1);
        int rowsCount = matrix.GetLength(0);

        if (rowsCount != coefficient.Length)
        {
            throw new ArgumentException($"{nameof(matrix)} and {nameof(coefficient)} aren't coherent");
        }

        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', columnsCount)}|r}}");

        for (int i = 0; i < rowsCount; i++)
        {
            math.Append($"{string.Join(" & ", matrix.GetRow(i).Select(f => f.ConvertFractionToLaTeX()))} & {coefficient[i].ConvertFractionToLaTeX()} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertTwoFractionMatricesToLaTeX(this Fraction[,] matrix, Fraction[,] otherMatrix)
    {
        int columnsCount1 = matrix.GetLength(1);
        int rowsCount1 = matrix.GetLength(0);

        int columnsCount2 = otherMatrix.GetLength(1);
        int rowsCount2 = otherMatrix.GetLength(0);

        if (rowsCount1 != rowsCount2)
        {
            throw new ArgumentException($"{nameof(matrix)} and {nameof(otherMatrix)} aren't coherent");
        }
        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', columnsCount1)}|{new string('r', columnsCount2)}}}");

        for (int i = 0; i < rowsCount1; i++)
        {
            math.Append($"{string.Join(" & ", matrix.GetRow(i).Select(f => f.ConvertFractionToLaTeX()))} & {string.Join(" & ", otherMatrix.GetRow(i).Select(f => f.ConvertFractionToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
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

    public static string ConvertMatrixToLaTeX<TMatrix>(this TMatrix[,] matrix)
    {
        if (matrix is Fraction[,] matrixFrac)
        {
            return ConvertFractionMatrixToLaTeX(matrixFrac);
        }

        int columnsCount = matrix.GetLength(1);
        int rowsCount = matrix.GetLength(0);

        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', columnsCount)}}}");

        for (int i = 0; i < rowsCount; i++)
        {
            math.Append($"{string.Join(" & ", matrix.GetRow(i).Select(f => f.ConvertToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertAugmentedMatrixToLaTeX<TMatrix, TCoefficient>(this TMatrix[,] matrix, TCoefficient[] coefficient)
    {
        if (matrix is Fraction[,] matrixFrac && coefficient is Fraction[] coefficientFrac)
        {
            return ConvertAugmentedFractionMatrixToLaTeX(matrixFrac, coefficientFrac);
        }

        int columnsCount = matrix.GetLength(1);
        int rowsCount = matrix.GetLength(0);

        if (rowsCount != coefficient.Length)
        {
            throw new ArgumentException($"{nameof(matrix)} and {nameof(coefficient)} aren't coherent");
        }

        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', columnsCount)}|r}}");

        for (int i = 0; i < rowsCount; i++)
        {
            math.Append($"{string.Join(" & ", matrix.GetRow(i).Select(f => f.ConvertToLaTeX()))} & {coefficient[i].ConvertToLaTeX()} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }

    public static string ConvertTwoMatricesToLaTeX<TMatrix, TOtherMatrix>(this TMatrix[,] matrix, TOtherMatrix[,] otherMatrix)
    {
        if (matrix is Fraction[,] matrixFrac && otherMatrix is Fraction[,] otherMatrixFrac)
        {
            return ConvertTwoFractionMatricesToLaTeX(matrixFrac, otherMatrixFrac);
        }


        int columnsCount1 = matrix.GetLength(1);
        int rowsCount1 = matrix.GetLength(0);

        int columnsCount2 = otherMatrix.GetLength(1);
        int rowsCount2 = otherMatrix.GetLength(0);

        if (rowsCount1 != rowsCount2)
        {
            throw new ArgumentException($"{nameof(matrix)} and {nameof(otherMatrix)} aren't coherent");
        }

        var math = new StringBuilder();
        math.AppendLine(@"\left[");
        math.AppendLine($@"\begin{{array}}{{{new string('r', columnsCount1)}|{new string('r', columnsCount2)}}}");

        for (int i = 0; i < rowsCount1; i++)
        {
            math.Append($"{string.Join(" & ", matrix.GetRow(i).Select(f => f.ConvertToLaTeX()))} & {string.Join(" & ", otherMatrix.GetRow(i).Select(f => f.ConvertToLaTeX()))} \\\\{_spacingBetweenMatrixRows}");
        }

        math.AppendLine(@"\end{array}");
        math.AppendLine(@"\right]");

        return math.ToString();
    }
}