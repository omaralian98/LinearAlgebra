using System.Linq;
using static LinearAlgebra.Linear;

namespace LinearAlgebra;
public partial class Linear
{
    public struct SpecialString
    {
        public static char[] vari = { 'x', 'y', 'z', 't', 'd', 's', 'h', 'k', 'p', 'v', 'e', 'l', 'a', 'b', 'c', 'f', 'g', 'i', 'j', 'm', 'n', 'o', 'q', 'r', 'u', 'w' };
        public string str;
        public SpecialString(char str, Fraction value)
        {
            this.str = str.ToString();
            variables.Add(str, value);
        }
        public SpecialString(string str) => this.str = str;
        public static Dictionary<char, Fraction> variables = new Dictionary<char, Fraction>();
        public static SpecialString operator +(SpecialString a) => a;
        public static SpecialString operator -(SpecialString a) => new($"-({a})");
        public static SpecialString operator +(SpecialString a, SpecialString b) => new($"({a} + {b})");
        public static SpecialString operator +(SpecialString a, object b) => new($"({a} + {b})");
        public static SpecialString operator +(object a, SpecialString b) => new($"({a} + {b})");
        public static SpecialString operator +(SpecialString a, Fraction b) =>
            (a * b.Denominator + b.Numerator) / b.Denominator;
        public static SpecialString operator +(Fraction a, SpecialString b) =>
            (b * a.Denominator + a.Numerator) / a.Denominator;
        public static SpecialString operator -(SpecialString a, SpecialString b) => new($"{a} - {b}");
        public static SpecialString operator -(SpecialString a, object b) => new($"({a} - {b})");
        public static SpecialString operator -(object a, SpecialString b) => new($"({a} - {b})");
        public static SpecialString operator -(SpecialString a, Fraction b) =>
            (a * b.Denominator - b.Numerator) / b.Denominator;
        public static SpecialString operator -(Fraction a, SpecialString b) =>
            (b * a.Denominator - a.Numerator) / a.Denominator;
        public static SpecialString operator *(SpecialString a, SpecialString b) => new($"{a} * {b}");
        public static SpecialString operator *(SpecialString a, object b) => new($"{b} * {a}");
        public static SpecialString operator *(object a, SpecialString b) => new($"({a} * {b})");
        public static SpecialString operator *(SpecialString a, Fraction b) => a * b.Numerator / b.Denominator;
        //public static SpecialString operator *(Fraction a, SpecialString b) => b * a.Numerator / a.Denominator;
        public static SpecialString operator *(Fraction a, SpecialString b)
        {
            if (a.Quotient.IsDecimal()) return new SpecialString($"({a}) * {b}");
            return new SpecialString($"{a} * {b}");
        }
        public static SpecialString operator /(SpecialString a, SpecialString b) => new($"{a} / {b}");
        public static SpecialString operator /(SpecialString a, object b) => new($"({a} / {b})");
        public static SpecialString operator /(object a, SpecialString b) => new($"({a} / {b})");
        public static SpecialString operator /(SpecialString a, Fraction b) => a * b.Denominator / b.Numerator;
        public static SpecialString operator /(Fraction a, SpecialString b) => b * a.Denominator / a.Numerator;


        public static Fraction[] Solve(SpecialString[] t)
        {
            Fraction[] answer = new Fraction[t.Length];
            for (int i = 0; i < t.Length; i++)
            {
                answer[i] = ExpressionHelpers.EvaluateAsFraction(t[i].str, variables);
            }
            return answer;
        }

        public SpecialString Simplifiy()
        {
            foreach (var it in str.Split(' '))
            {
            }
            return new SpecialString(str);
        }

        public override string ToString() => str;
    }

    /// <summary>
    /// Returns the Rank of a matrix
    /// </summary>
    /// <param name="matrix">The matrix you want to calculate its rank</param>
    /// <returns> 
    /// An integer number x, such as 0 ≤ x ≤ number of rows. 
    /// <br></br>
    /// -1 if there was an error.
    /// </returns>
    /// <exception cref="ArithmeticException"></exception>
    public static int Rank(decimal[,] matrix)
    {
        //steps.Add(new MatrixStep
        //{
        //    StepDescription = "We get the Row Echelon Form(REF) of our matrix\nThen we count every non-zero row\nThe result is the rank of this matrix",
        //    Matrix = matrix.GetFractions()
        //});
        REF(matrix);
        int rank = 0;
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                if (matrix[x, y] != 0)
                {
                    rank++;
                    break;
                }
            }
        }
        return rank;
    }
}
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
    public static decimal[] SpecialString2Decimal(this SpecialString[] t)
    {
         return SpecialString.Solve(t).Fraction2Decimal();
    }
    public static string[] SpecialString2StringWSolve(this SpecialString[] t)
    {
        return SpecialString.Solve(t).Fraction2String();
    }
    public static string[] SpecialString2String(this SpecialString[] t)
    {
        var strings = new string[t.Length];
        for (int i = 0; i < t.Length; i++)
        {
            strings[i] = t[i].str;
        }
        return strings;
    }
    public static Fraction[] SpecialString2Fraction(this SpecialString[] t)
    {
        return SpecialString.Solve(t);
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

    public static SpecialString[] GetFraction<T>(this T[] oldMatrix)
    {
        SpecialString[] matrix = new SpecialString[oldMatrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            string t = oldMatrix[i]?.ToString() ?? "0";
            if (t.Contains('/'))
            {
                Fraction fraction = String2Fraction(t);
                SpecialString sp = new(SpecialString.vari[i], fraction);
                if (fraction.Denominator == 0) throw new DivideByZeroException("You can't divide by zero");
                matrix[i] = sp;
            }
            else
            {
                if (t.IsDecimal()) throw new ArithmeticException("We don't support decimal numbers try passing it as a string matrix separating the numerator from the denominator by slash('/') a/b");
                matrix[i] = new(SpecialString.vari[i], new(Convert.ToDouble(t)));
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
    public static T[] GetColumn<T>(this T[,] matrix, int columnNumber) => Enumerable.Range(0, matrix.GetLength(0))
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
public static class ExpressionHelpers
{
    public static char[] operations = { '*', '/', '+', '-' };
    public static Fraction EvaluateAsFraction(string expression, Dictionary<char, Fraction> variables)
    {
        string replaced = ReplaceVariables(expression, variables);
        List<string> postFix = InfixToPostfix(replaced);
        return CalCulate(postFix);
    }
    public static decimal EvaluateAsDecimal(string expression, Dictionary<char, Fraction> variables)
    {
        return EvaluateAsFraction(expression, variables).Quotient;
    }
    public static string EvaluateAsString(string expression, Dictionary<char, Fraction> variables)
    {
        return EvaluateAsFraction(expression, variables).ToString();
    }
    private static Fraction CalCulate(List<string> exp)
    {
        Stack<string> stack = new Stack<string>();
        Fraction answer = new(0);
        bool first = true;
        for (int i = 0; i < exp.Count; i++)
        {
            if (operations.Contains(exp[i][0]) && exp[i].Length == 1)
            {
                double y = Convert.ToDouble(stack.Pop());
                if (first)
                {
                    answer = new(y);
                    first = false;
                    y = Convert.ToDouble(stack.Pop());
                }
                if (exp[i][0] == '*') answer *= y;
                else if (exp[i][0] == '/') answer /= y;
                else if (exp[i][0] == '+') answer += y;
                else answer -= y;
            }
            else stack.Push(exp[i].ToString());
        }
        return answer;
    }
    private static string ReplaceVariables(string expression, Dictionary<char, Fraction> variables)
    {
        expression = expression.Replace("(", "( ");
        expression = expression.Replace(")", " )");
        foreach (var variable in variables)
        {
            expression = expression.Replace(variable.Key.ToString(), $"( {variable.Value.Numerator} / {variable.Value.Denominator} )");
        }
        return expression;
    }

    public static List<string> InfixToPostfix(string expression)
    {
        List<string> result = new List<string>();
        Stack<char> stack = new Stack<char>();
        expression = expression.Replace("(", "( ");
        expression = expression.Replace(")", " )");
        expression = expression.Replace(" / ", "/");
        expression = expression.Replace("/", " / ");
        foreach (var item in expression.Split(' '))
        {
            string current = item.Trim();
            if (current[0] == '(')
            {
                stack.Push(current[0]);
            }
            else if (current[0] == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    result.Add(stack.Pop().ToString());
                }
                stack.Pop();
            }
            else if (IsOperator(current))
            {
                while (stack.Count > 0 && Prec(stack.Peek()) >= Prec(current[0]))
                {
                    result.Add(stack.Pop().ToString());
                }
                stack.Push(current[0]);
            }
            else
            {
                result.Add(current.ToString());
            }
        }

        while (stack.Count > 0)
        {
            result.Add(stack.Pop().ToString());
        }

        return result;
    }

    private static bool IsOperator(string c)
    {
        return c == "+" || c == "-" || c == "*" || c == "/" || c == "^";
    }

    private static int Prec(char c)
    {
        if (c == '-' || c == '+') return 1;
        if (c == '*' || c == '/') return 2;
        if (c == '^') return 3;
        return -1;
    }
}

public class Special
{
    Dictionary<string, Fraction> values = new();
    const string None = "No";
    public Special()
    {

    }
    public Special(string variable)
    {
        values.Add(variable, new Fraction(1));
    }
    public Special(Fraction fraction)
    {
        values.Add(None, new Fraction(1));
    }
    public Special(Fraction fraction, string variable)
    {
        values.Add(variable, fraction);
    }

    public static Special operator +(Special a, Special b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (var item in b.values)
        {
            if (a.values.ContainsKey(item.Key))
            {
                newValues[item.Key] = a.values[item.Key] + item.Value;
            }
            else
            {
                newValues[item.Key] = item.Value;
            }
        }
        return new Special { values = newValues };
    }
    public static Special operator -(Special a, Special b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (var item in b.values)
        {
            if (newValues.ContainsKey(item.Key))
            {
                newValues[item.Key] -= item.Value;
            }
            else
            {
                newValues[item.Key] = -item.Value;
            }
        }
        return new Special { values = newValues };
    }
    public static Special operator *(Special a, Special b)
    {
        Dictionary<string, Fraction> newValues = new();
        foreach (var item in a.values)
        {
            foreach (var item1 in b.values)
            {
                newValues.Add($"{item.Key}{item1.Key}", item.Value * item1.Value);
            }
        }
        return new Special { values = newValues };
    }
    public static Special operator /(Special a, Special b)
    {
        Dictionary<string, Fraction> newValues = new();
        foreach (var item in a.values)
        {
            foreach (var item1 in b.values)
            {
                newValues.Add($"{item.Key}{item1.Key}", item.Value / item1.Value);
            }
        }
        return new Special { values = newValues };
    }
    public static Special operator +(Special a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value + b);
        return new Special { values = newValues };
    }
    public static Special operator -(Special a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value - b);
        return new Special { values = newValues };
    }
    public static Special operator *(Special a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value * b);
        return new Special { values = newValues };
    }
    public static Special operator /(Special a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value / b);
        return new Special { values = newValues };
    }
    public override string ToString()
    {
        string result = "";
        foreach (var item in values)
        {
            result += $"{item.Value} * {item.Key} + ";
        }
        return result[..^3];
    }
}