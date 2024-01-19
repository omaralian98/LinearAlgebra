﻿namespace LinearAlgebra;

public static class Extensions
{
    public static string[,] Fraction2String(this Fraction[,] t)
    {
        string[,] answer = new string[t.GetLength(0), t.GetLength(1)];
        for (int i = 0; i < answer.GetLength(0); i++)
        {
            for (int j = 0; j < answer.GetLength(1); j++)
            {
                answer[i, j] = t[i, j].ToString();
            }
        }
        return answer;
    }

    public static string[] Fraction2String(this Fraction[] t)
    {
        string[] answer = new string[t.GetLength(0)];
        for (int i = 0; i < answer.GetLength(0); i++)
        {
            answer[i] = t[i].ToString();
        }
        return answer;
    }

    public static decimal[,] Fraction2Decimal(this Fraction[,] t)
    {
        decimal[,] a = new decimal[t.GetLength(0), t.GetLength(1)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                a[i, j] = (decimal)t[i, j].Quotient;
            }
        }
        return a;
    }

    public static decimal[] Fraction2Decimal(this Fraction[] t)
    {
        decimal[] a = new decimal[t.GetLength(0)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            a[i] = (decimal)t[i].Quotient;
        }
        return a;
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
                string t = oldMatrix[i, j]?.ToString() ?? "0";
                if (t.Contains('/'))
                {
                    Fraction fraction = String2Fraction(t);
                    if (fraction.Denominator == 0) throw new DivideByZeroException("You can't divide by zero");
                    matrix[i, j] = fraction;
                }
                else
                {
                    if (t.IsDecimal()) throw new ArithmeticException("We don't support decimal numbers try passing it as a string matrix separating the numerator from the denominator by slash('/') a/b");
                    matrix[i, j] = new Fraction(Convert.ToDouble(t));
                }
            }
        }
        return matrix;
    }

    public static Fraction[] GetFractions<T>(this T[] oldMatrix)
    {
        Fraction[] matrix = new Fraction[oldMatrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            string t = oldMatrix[i]?.ToString() ?? "0";
            if (t.Contains('/'))
            {
                Fraction fraction = String2Fraction(t);
                if (fraction.Denominator == 0) throw new DivideByZeroException("You can't divide by zero");
                matrix[i] = fraction;
            }
            else
            {
                if (t.IsDecimal()) throw new ArithmeticException("We don't support decimal numbers try passing it as a string matrix separating the numerator from the denominator by slash('/') a/b");
                matrix[i] = new Fraction(Convert.ToDouble(t));
            }
        }
        return matrix;
    }

    public static bool IsDecimal<T>(this T it)
    {
        string item = it?.ToString() ?? "";
        return item.Contains('.');
    }

    public static Fraction String2Fraction(this string a)
    {
        int indexOfSlash = a.IndexOf('/');
        double dividend = Convert.ToDouble(a[0..indexOfSlash]);
        double divisor = Convert.ToDouble(a[(indexOfSlash + 1)..a.Length]);
        return new Fraction(dividend, divisor);
    }

    public static string GetMatrix<T>(this T[,] matrix)
    {
        string[] Lines = new string[matrix.GetLength(0) + 2];
        AddBeginningBrackets(ref Lines);
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            int index = 1;
            int vart = GetPad(matrix.GetColumn(j)) + 2;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Lines[index++] += String.Format(" {0, " + vart + "}", matrix[i, j]);
            }
        }
        AddEndBrackets(ref Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static string GetMatrix<T>(this T[] matrix)
    {
        string[] Lines = new string[matrix.GetLength(0) + 2];
        AddBeginningBrackets(ref Lines);
        int vart = GetPad(matrix);
        int index = 1;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Lines[index++] += String.Format(" {0, " + vart + "} ", matrix[i]);
        }
        AddEndBrackets(ref Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static string GetMatrix<T, S>(this (T[,] matrix, S[] coefficient) c)
    {
        string[] Lines = new string[c.matrix.GetLength(0) + 2];
        AddBeginningBrackets(ref Lines);
        int index = 1;
        for (int j = 0; j < c.matrix.GetLength(1); j++)
        {
            index = 1;
            int vart = GetPad(c.matrix.GetColumn(j)) + 2;
            for (int i = 0; i < c.matrix.GetLength(0); i++)
            {
                Lines[index++] += String.Format(" {0, " + vart + "} ", c.matrix[i, j]);
            }
        }
        index = 1;
        for (int i = 0; i < c.coefficient.Length; i++)
        {
            int vart = GetPad(c.coefficient);
            Lines[index++] += String.Format("| {0, " + vart + "}", c.coefficient[i]);
        }
        AddEndBrackets(ref Lines);
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

    private static T[] GetColumn<T>(this T[,] matrix, int columnNumber) => Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber]).ToArray();
    private static T[] GetRow<T>(this T[,] matrix, int rowNumber) => Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x]).ToArray();
    private static void AddBeginningBrackets(ref string[] Lines)
    {
        Lines[0] += "┌";
        for (int i = 1; i < Lines.Length - 1; i++)
        {
            Lines[i] += "|";
        }
        Lines[^1] += "└";
    }

    private static void AddEndBrackets(ref string[] Lines)
    {
        Lines[0] += String.Format(" {0, " + Lines[1].Length + "}", "┐");
        for (int i = 1; i < Lines.Length - 1; i++)
        {
            Lines[i] += " |";
        }
        Lines[^1] += String.Format("{0, " + (Lines[1].Length - 1) + "}", "┘");
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