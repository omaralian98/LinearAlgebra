namespace LinearAlgebra;

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

    public static string GetDeterminantMatrix<T>(this T[,] matrix)
    {
        if (matrix.GetLength(0) == 1 && matrix.GetLength(1) == 1)
        {
            return matrix[0, 0]?.ToString() ?? "";
        }
        string[] Lines = new string[matrix.GetLength(0)];
        AddBeginningLine(Lines);
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            int index = 0;
            int vart = GetPad(matrix.GetColumn(j)) + 2;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Lines[index++] += String.Format(" {0, " + vart + "}", matrix[i, j]);
            }
        }
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
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            int index = 1;
            int vart = GetPad(matrix.GetColumn(j)) + 2;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Lines[index++] += String.Format(" {0, " + vart + "}", matrix[i, j]);
            }
        }
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
        int vart = GetPad(matrix);
        int index = 1;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Lines[index++] += String.Format(" {0, " + vart + "} ", matrix[i]);
        }
        AddEndBrackets(Lines);
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
        AddBeginningBrackets(Lines);
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
        AddEndBrackets(Lines);
        string result = "";
        foreach (var it in Lines)
        {
            result += it + "\n";
        }
        return result;
    }

    public static string GetMatrix<T, S>(this (T[] matrix, S[] coefficient) c)
    {
        T[,] matrix = new T[c.matrix.Length, 1];
        for (int i = 0;i < c.matrix.Length;i++)
        {
            matrix[i, 0] = c.matrix[i];
        }
        return (matrix, c.coefficient).GetMatrix();
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