using MathNet.Symbolics;
using Microsoft.FSharp.Core;
using System;
using System.Numerics;

namespace LinearAlgebra;

public static class Extensions
{
    public static string?[,] ConvertToStringMatrix<T>(this T[,] matrix)
    {
        string?[,] answer = new string?[matrix.GetLength(0), matrix.GetLength(1)];
        for (int i = 0; i < answer.GetLength(0); i++)
        {
            for (int j = 0; j < answer.GetLength(1); j++)
            {
                answer[i, j] = matrix[i, j]?.ToString();
            }
        }
        return answer;
    }

    public static string[] ConvertToStringMatrix<T>(this T[] matrix)
    {
        string[] answer = new string[matrix.Length];
        for (int i = 0; i < answer.Length; i++)
        {
            answer[i] = matrix[i]?.ToString() ?? "";
        }
        return answer;
    }

    public static string ConvertToString<T>(this T[] matrix)
    {
        return $"[{string.Join(", ", matrix)}]";
    }

    public static decimal[,] Fraction2Decimal(this Fraction[,] t, int decimals = -1)
    {
        decimal[,] a = new decimal[t.GetLength(0), t.GetLength(1)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                if (decimals > -1) a[i, j] = Math.Round(t[i, j].Quotient, decimals);
                else a[i, j] = t[i, j].Quotient;
            }
        }
        return a;
    }

    public static decimal[] Fraction2Decimal(this Fraction[] t)
    {
        decimal[] a = new decimal[t.GetLength(0)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            a[i] = t[i].Quotient;
        }
        return a;
    }

    public static SpecialString[] String2SpecialString(this string[] t)
    {
        var result = new SpecialString[t.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = t[i];
        }
        return result;
    }

    public static decimal[] SpecialString2Decimal(this SpecialString[] t, Dictionary<string, Fraction> variablesValue)
    {
        return SpecialString.Solve(t, variablesValue).Fraction2Decimal();
    }

    public static string[] SpecialString2String(this SpecialString[] t)
    {
        var strings = new string[t.Length];
        for (int i = 0; i < t.Length; i++)
        {
            strings[i] = t[i].ToString();
        }
        return strings;
    }

    public static string[] SpecialString2Decimal(this SpecialString[] t)
    {
        var strings = new string[t.Length];
        for (int i = 0; i < t.Length; i++)
        {
            strings[i] = t[i].ToDecimalString();
        }
        return strings;
    }

    public static Fraction[] SpecialString2Fraction(this SpecialString[] t, Dictionary<string, Fraction> variablesValue)
    {
        return SpecialString.Solve(t, variablesValue);
    }

    public static Fraction[,] GetFractions<T>(this T[,] oldMatrix)
    {
        Fraction[,] matrix = new Fraction[oldMatrix.GetLength(0), oldMatrix.GetLength(1)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                string value = oldMatrix[i, j]?.ToString() ?? "0";
                matrix[i, j] = (Fraction)value;
            }
        }
        return matrix;
    }

    public static Fraction[] GetFractions<T>(this T[] oldMatrix)
    {
        Fraction[] matrix = new Fraction[oldMatrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            string value = oldMatrix[i]?.ToString() ?? "0";
            matrix[i] = (Fraction)value;
        }
        return matrix;
    }

    public static T[,] GetTMatrix<T>(this Fraction[,] matrix)
    {
        if (typeof(T) == typeof(Fraction)) return (matrix as T[,])!;
        var result = new T[matrix.GetLength(0), matrix.GetLength(1)];
        var type = typeof(T);
        for (int i = 0;i < matrix.GetLength(0); i++)
        {
            for(int j = 0;j < matrix.GetLength(1); j++)
            {
                object test = matrix[i, j].ToType(type, null);
                
                result[i, j] = (T)test;
            }
        }
        return result;
    }

    public static T[] GetTMatrix<T>(this Fraction[] matrix)
    {
        if (typeof(T) == typeof(Fraction)) return (matrix as T[])!;
        var result = new T[matrix.Length];
        var type = typeof(T);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            object test = matrix[i].ToType(type, null);

            result[i] = (T)test;
        }
        return result;
    }

    public static bool IsDecimal<T>(this T it)
    {
        if (typeof(double) == typeof(T))
        {
            return !double.IsInteger(Convert.ToDouble(it));
        }

        string str = it?.ToString() ?? "";
        return str.Contains('.');
    }

    public static string GetDeterminantMatrix<T>(this T[,] matrix)
    {
        if (matrix.GetLength(0) == 1 && matrix.GetLength(1) == 1)
        {
            return matrix[0, 0]?.ToString() ?? "";
        }
        string[] Lines = new string[matrix.GetLength(0)];
        AddBeginningLine(Lines);
        AddMatrix(matrix, Lines);
        AddEnddingLine(Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static string GetMatrix<T>(this T[,] matrix)
    {
        if (matrix.GetLength(0) == 1 && matrix.GetLength(1) == 1)
        {
            return matrix[0, 0]?.ToString() ?? "";
        }
        string[] Lines = new string[matrix.GetLength(0) + 2];
        AddBeginningBrackets(Lines);
        AddMatrix(matrix, Lines);
        AddEndBrackets(Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static string GetMatrix<T>(this T[] matrix)
    {
        if (matrix.Length == 1)
        {
            return matrix[0]?.ToString() ?? "";
        }
        string[] Lines = new string[matrix.GetLength(0) + 2];
        AddBeginningBrackets(Lines);
        AddMatrix(matrix, Lines);
        AddEndBrackets(Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static string GetMatrix<T, S>(this (T[,]? matrix, S[]? coefficient) c)
    {
        if (c.coefficient is null && c.matrix is not null) return GetMatrix(c.matrix);
        else if (c.matrix is null && c.coefficient is not null) return GetMatrix(c.coefficient);
        else if (c.matrix is null && c.coefficient is null) return "";
        if (c.matrix!.GetLength(0) != c.coefficient!.GetLength(0)) throw new InvalidOperationException();
        string[] Lines = new string[c.matrix.GetLength(0) + 2];
        AddBeginningBrackets(Lines);
        AddMatrix(c.matrix, Lines, true);
        AddMatrix(c.coefficient, Lines);
        AddEndBrackets(Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static string GetMatrix<T, S>(this (T[,]? matrix, S[,]? coefficient) c)
    {
        if (c.coefficient is null && c.matrix is not null) return GetMatrix(c.matrix);
        else if (c.matrix is null && c.coefficient is not null) return GetMatrix(c.coefficient);
        else if (c.matrix is null && c.coefficient is null) return "";
        if (c.matrix!.GetLength(0) != c.coefficient!.GetLength(0)) throw new InvalidOperationException();
        string[] Lines = new string[c.matrix.GetLength(0) + 2];
        AddBeginningBrackets(Lines);
        AddMatrix(c.matrix, Lines, true);
        AddMatrix(c.coefficient, Lines);
        AddEndBrackets(Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    private static string[] AddMatrix<T>(this T[,] matrix, string[]? Lines = null,bool addLine = false)
    {
        Lines = Lines is null ? new string[matrix.GetLength(0)] : Lines;
        int index;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            index = 1;
            int vart = GetPad(matrix.GetColumn(j)) + 2;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Lines[index++] += String.Format(" {0, " + vart + "} ", matrix[i, j]);
                if (j + 1 == matrix.GetLength(1) && addLine) Lines[index - 1] += "|";
            }
        }
        return Lines;
    }
    private static string[] AddMatrix<T>(this T[] vector, string[]? Lines = null, bool addLine = false)
    {
        Lines = Lines is null ? new string[vector.Length] : Lines;
        int index = 1;
        for (int i = 0; i < vector.Length; i++)
        {
            int vart = GetPad(vector) + 2;
            Lines[index++] += String.Format("{1} {0, " + vart + "}", vector[i], (addLine?"|":""));
        }
        return Lines;
    }

    public static string GetMatrix<T, S>(this (T[] matrix, S[] coefficient) c)
    {
        string[] Lines = new string[c.matrix.GetLength(0) + 2];
        AddBeginningBrackets(Lines);
        int vart = GetPad(c.matrix);
        int index = 1;
        for (int i = 0; i < c.matrix.Length; i++)
        {
            Lines[index++] += String.Format(" {0, " + vart + "} ", c.matrix[i]);
        }
        index = 1;
        for (int i = 0; i < c.coefficient.Length; i++)
        {
            vart = GetPad(c.coefficient);
            Lines[index++] += String.Format("| {0, " + vart + "}", c.coefficient[i]);
        }
        AddEndBrackets(Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static void Print<T>(this T[,] matrix)
    {
        Console.WriteLine(matrix.GetMatrix());
    }

    public static void Print<T>(this T[] matrix)
    {
        Console.WriteLine(matrix.GetMatrix());
    }

    public static void Print<T, S>(this (T[,] matrix, S[] coefficient) c)
    {
        Console.WriteLine(c.GetMatrix());
    }

    private static int GetPad<T>(this T[] matrix)
    {
        int max = 0;
        foreach (var it in matrix)
        {
            max = Math.Max(max, it?.ToString()?.Length ?? 0);
        }
        return max;
    }
    /// <summary>
    /// Get's a specific column from a 2d array.
    /// </summary>
    /// <typeparam name="T">Any type of array</typeparam>
    /// <param name="matrix">The original 2d array</param>
    /// <param name="columnIndex">The index of the required column</param>
    /// <param name="startFromIndex">The index of the start row</param>
    /// <returns>The required column as an array</returns>
    public static T[] GetColumn<T>(this T[,] matrix, int columnIndex, int startFromIndex = 0) =>
        Enumerable.Range(startFromIndex, matrix.GetLength(0) - startFromIndex)
            .Select(x => matrix[x, columnIndex])
             .ToArray();

    /// <summary>
    /// Get's a specific row from a 2d array.
    /// </summary>
    /// <typeparam name="T">Any type of array</typeparam>
    /// <param name="matrix">The original 2d array</param>
    /// <param name="rowIndex">The index of the required row</param>
    /// <param name="startFromIndex">The index of the start column</param>
    /// <returns>The required row as an array</returns>
    public static T[] GetRow<T>(this T[,] matrix, int rowIndex, int startFromIndex = 0) =>
        Enumerable.Range(startFromIndex, matrix.GetLength(1) - startFromIndex)
            .Select(y => matrix[rowIndex, y])
             .ToArray();

    public static T[,] ConvertTo2D<T>(this T[] a, int columns = 0, int rows = 0)
    {
        if (columns == 0 && rows == 0)
        {
            columns = Convert.ToInt32(Math.Sqrt(a.Length));
            rows = columns;
        }
        else if (rows == 0) rows = a.Length / columns;
        else if (columns == 0) columns = a.Length / rows;
        if (rows * columns != a.Length) throw new ArgumentException();
        int counter = 0;
        T[,] result = new T[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = a[counter++];
            }
        }
        return result;
    }

    public static T[] ConvertTo1D<T>(this T[,] a) => a.Cast<T>().Select(c => c).ToArray();

    private static void AddBeginningBrackets(string[] Lines)
    {
        Lines[0] += "┌";
        for (int i = 1; i < Lines.Length - 1; i++)
        {
            Lines[i] += "|";
        }
        Lines[^1] += "└";
    }

    private static void AddEndBrackets(string[] Lines)
    {
        Lines[0] += String.Format(" {0, " + Lines[1].Length + "}", "┐");
        for (int i = 1; i < Lines.Length - 1; i++)
        {
            Lines[i] += " |";
        }
        Lines[^1] += String.Format("{0, " + (Lines[1].Length - 1) + "}", "┘");
    }

    private static void AddBeginningLine(string[] Lines)
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i] += "|";
        }
    }

    private static void AddEnddingLine(string[] Lines)
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i] += " |";
        }
    }

    public static bool IsNumber(this object value)
    {
        return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
    }
}