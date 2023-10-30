using System.Runtime.InteropServices;

namespace LinearAlgebra;
public static class Linear
{
    public struct Fraction
    {
        public decimal Numerator;
        public decimal Denominator;
        public Fraction(decimal num, decimal den = 1)
        {
            Numerator = num;
            Denominator = den;
            if ((den == 0 && Numerator == 0) || Numerator == 0 || den == 0)
            {
                Numerator = 0;
                Denominator = 1;
            }
            else
            {
                decimal gcd = GCD(Math.Abs(Numerator), Math.Abs(den));
                Denominator = den / gcd;
                Numerator = Numerator / gcd;
            }
            if (Numerator < 0 && Denominator < 0)
            {
                Numerator = Math.Abs(Numerator);
            }
            else if (Numerator > 0 && Denominator < 0)
            {
                Numerator = -Numerator;
            }
            Denominator = Math.Abs(Denominator);
        }

        public decimal Quotient
        {
            get { return Numerator / Denominator; }
        }
        public static decimal GCD(decimal a, decimal b)
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
        public void Print()
        {
            Console.WriteLine("{0}/{1}", Numerator, Denominator);
        }
        public static string[,] Fraction2String(Fraction[,] t)
        {
            string[,] answer = new string[t.GetLength(0), t.GetLength(1)];
            for (int i = 0; i < answer.GetLength(0); i++)
            {
                for (int j = 0; j < answer.GetLength(1); j++)
                {
                    if (t[i, j].Denominator == 1)
                    {
                        answer[i, j] = String.Format("{0}", t[i, j].Numerator);
                    }
                    else if (t[i, j].Quotient.ToString().Contains(".") == false)
                    {
                        answer[i, j] = String.Format("{0}", t[i, j].Quotient);
                    }
                    else
                    {
                        answer[i, j] = String.Format("{0}/{1}", t[i, j].Numerator, t[i, j].Denominator);
                    }
                }
            }
            return answer;
        }
        public static decimal[,] Fraction2Decimal(Fraction[,] t)
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
        public static Fraction[,] GetFractions<T>(T[,] a)
        {
            Fraction[,] t = new Fraction[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < t.GetLength(0); i++)
            {
                for (int j = 0; j < t.GetLength(1); j++)
                {
                    t[i, j] = new Fraction(Convert.ToDecimal(a[i, j]));
                }
            }
            return t;
        }
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
    public static int Rank(this decimal[,] matrix)
    {
        string a = "Mr_sure21";
        return 0;
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// <br></br>
    /// Returns the REF of the giving matrix, and it's coefficient if provided.
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="coefficient"></param>
    /// <returns></returns>
    public static decimal[,] REF(this decimal[,] matrix)
    {
        Fraction[,] newMatrix = Fraction.GetFractions(matrix);
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        (newMatrix, coefficient) = newMatrix.REF(coefficient);
        return Fraction.Fraction2Decimal(newMatrix);
    }
    public static Fraction[,] REFAsFraction(this decimal[,] matrix)
    {
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        Fraction[,] answer;
        (answer, coefficient) = Fraction.GetFractions(matrix).REF(coefficient);
        return answer;
    }
    public static string[,] REFAsString(this decimal[,] matrix)
    {
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        Fraction[,] answer;
        (answer, coefficient) = Fraction.GetFractions(matrix).REF(coefficient);
        return Fraction.Fraction2String(answer);
    }

    public static (Fraction[,], Fraction[]) REF(this Fraction[,] matrix, Fraction[] coefficient)
    {
        int row = matrix.GetLength(0);
        int col = matrix.GetLength(1);
        int y = 0;
        for (int x = 0; x < Math.Min(row, col); x++)
        {
            y = x;
            bool result; int xx, yy;
            (result, xx, yy) = CheckPossibleSwap(x, x, matrix);
            if (result)
            {
                SwapMatrix(xx, yy, ref matrix);
                SwapCoefficient(xx, yy, ref coefficient);
            }
            else if (result == false && xx == -1) y++;
            Piviot(x, y, ref matrix);
        }
        return (matrix, coefficient);
    }
    public static void PrintMatrix<T>(this T[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (j != matrix.GetLength(1) - 1)
                    Console.Write(" {0} |", matrix[i, j]);
                else
                    Console.Write(" {0}", matrix[i, j]);
            }
            Console.WriteLine("");
        }
    }

    private static void Piviot(int x, int y, ref Fraction[,] matrix)
    {

        for (int i = x + 1; i < matrix.GetLength(0); i++)
        {
            if (matrix[i, y].Quotient == 0) continue;
            Action(piviotRow: x, targetedRow: i, columnStart: y, ref matrix);
        }
    }

    private static void Action(int piviotRow, int targetedRow, int columnStart, ref Fraction[,] matrix)
    {
        decimal divisor = matrix[targetedRow, columnStart].Denominator * matrix[piviotRow, columnStart].Numerator;
        decimal dividend = matrix[targetedRow, columnStart].Numerator * matrix[piviotRow, columnStart].Denominator;
        Fraction scalar = new(dividend, divisor);
        matrix[targetedRow, columnStart] = new(0);
        for (int y = columnStart + 1; y < matrix.GetLength(1); y++)
        {
            decimal testVal = -scalar.Quotient * matrix[piviotRow, y].Quotient + matrix[targetedRow, y].Quotient;
            if (testVal.ToString().Contains('.'))
            {
                /// -a/b × c/d + e/f = -ac/bd + e/f = f(-ac/bd) + bd(e/f) = -acf + ebd / fbd 
                /// Since we miltple the scalar with the piviot and add it to target cell. 
                /// We can calculate this formula using the example above.
                decimal a = scalar.Numerator, b = scalar.Denominator;
                decimal c = matrix[piviotRow, y].Numerator, d = matrix[piviotRow, y].Denominator;
                decimal e = matrix[targetedRow, y].Numerator, f = matrix[targetedRow, y].Denominator;
                dividend = -a * c * f + e * b * d;
                divisor = f * b * d;
                matrix[targetedRow, y] = new Fraction(dividend, divisor);
            }
            else
            {
                matrix[targetedRow, y] = new Fraction(testVal);
            }
        }

    }

    private static (bool, int, int) CheckPossibleSwap(int x, int y, Fraction[,] matrix)
    {
        if (matrix[x, y].Quotient == 0)
        {
            int num = -1;
            for (int i = x + 1; i < matrix.GetLength(0); i++)
            {
                decimal current = matrix[i, y].Quotient;
                if (current == 1 || current == -1 || (current != 0 && current.ToString().Contains('.') == false))
                {
                    num = i - x;
                    break;
                }
            }
            if (num == -1) return (false, -1, -1);
            return (true, x, y + num);
        }
        return (false, 0, 0);
    }
    private static void SwapMatrix<T>(int x, int y, ref T[,] matrix)
    {
        int columns = matrix.GetLength(1);
        for (int i = 0; i < columns; i++)
        {
            (matrix[x, i], matrix[y, i]) = (matrix[y, i], matrix[x, i]);
        }
    }
    private static void SwapCoefficient<T>(int x, int y, ref T[] coefficient)
    {
        if (coefficient is not null) (coefficient[x], coefficient[y]) = (coefficient[y], coefficient[x]);
    }
}