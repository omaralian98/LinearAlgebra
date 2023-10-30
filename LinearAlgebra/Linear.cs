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
        private static decimal GCD(decimal a, decimal b)
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
    }
    private static string[,] Fraction2String(this Fraction[,] t)
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
    private static string[] Fraction2String(this Fraction[] t)
    {
        string[] answer = new string[t.GetLength(0)];
        for (int i = 0; i < answer.GetLength(0); i++)
        {
            if (t[i].Denominator == 1)
            {
                answer[i] = String.Format("{0}", t[i].Numerator);
            }
            else if (t[i].Quotient.ToString().Contains(".") == false)
            {
                answer[i] = String.Format("{0}", t[i].Quotient);
            }
            else
            {
                answer[i] = String.Format("{0}/{1}", t[i].Numerator, t[i].Denominator);
            }
        }
        return answer;
    }
    private static decimal[,] Fraction2Decimal(this Fraction[,] t)
    {
        decimal[,] a = new decimal[t.GetLength(0), t.GetLength(1)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                a[i, j] = t[i, j].Quotient;
            }
        }
        return a;
    }
    private static decimal[] Fraction2Decimal(this Fraction[] t)
    {
        decimal[] a = new decimal[t.GetLength(0)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            a[i] = t[i].Quotient;
        }
        return a;
    }
    private static Fraction[,] GetFractions<T>(this T[,] oldMatrix)
    {
        Fraction[,] matrix = new Fraction[oldMatrix.GetLength(0), oldMatrix.GetLength(1)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (oldMatrix[i, j].IsDecimal()) throw new ArithmeticException();
                matrix[i, j] = new Fraction(Convert.ToDecimal(oldMatrix[i, j]));
            }
        }
        return matrix;
    }
    private static Fraction[] GetFractions<T>(this T[] oldMatrix)
    {
        Fraction[] matrix = new Fraction[oldMatrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (oldMatrix[i].IsDecimal()) throw new ArithmeticException();
            matrix[i] = new Fraction(Convert.ToDecimal(oldMatrix[i]));
        }
        return matrix;
    }
    private static bool IsDecimal<T>(this T it)
    {
        string item = it?.ToString() ?? ""; 
        return item.Contains('.');
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
    public static int Rank(this decimal[,] matrix)
    {
        matrix.REF();
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

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>Returns the REF of the giving matrix</returns>
    /// <exception cref="ArithmeticException"></exception>
    public static decimal[,] REF(this decimal[,] matrix)
    {
        Fraction[,] newMatrix = matrix.GetFractions();
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        (newMatrix, coefficient) = REFAsFraction(newMatrix, coefficient);
        return newMatrix.Fraction2Decimal();
    }
    public static Fraction[,] REFAsFraction(this decimal[,] matrix)
    {
        Fraction[,] answer;
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        (answer, coefficient) = REFAsFraction(matrix.GetFractions() ,coefficient);
        return answer;
    }
    public static string[,] REFAsString(this decimal[,] matrix)
    {
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        Fraction[,] answer;
        (answer, coefficient) = matrix.GetFractions().REFAsFraction(coefficient);
        return answer.Fraction2String();
    }

    public static (Fraction[,], Fraction[]) REFAsFraction(this Fraction[,] matrix, Fraction[] coefficient)
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
            Pivot(x, y, ref matrix, ref coefficient);
        }
        return (matrix, coefficient);
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>Returns the REF of the giving matrix, and it's coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    public static (decimal[,], decimal[]) REF(this decimal[,] matrix, decimal[] coefficient)
    {
        Fraction[] coe;
        Fraction[,] result;
        (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2Decimal(), coe.Fraction2Decimal());
    }
    public static (string[,], string[]) REFAsString(this decimal[,] matrix, decimal[] coefficient)
    {
        Fraction[] coe;
        Fraction[,] result;
        (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2String(), coe.Fraction2String());
    }

    private static void Pivot(int x, int y, ref Fraction[,] matrix, ref Fraction[] coefficient)
    {

        for (int i = x + 1; i < matrix.GetLength(0); i++)
        {
            if (matrix[i, y].Quotient == 0) continue;
            decimal divisor = matrix[i, y].Denominator * matrix[x, y].Numerator;
            decimal dividend = matrix[i, y].Numerator * matrix[x, y].Denominator;
            Fraction scalar = new(dividend, divisor);
            Action(pivotRow: x, targetedRow: i, columnStart: y, scalar, ref matrix);
            coefficient[i] = SpecialOp(scalar, coefficient[x], coefficient[i]);
        }
    }

    /// <summary>
    /// Combines three fractions using a specific operation: scalar times pivot, added to the targeted fraction.
    /// <br></br>
    /// The operation can be described as follows:
    /// <br></br>
    /// - Multiply the scalar fraction (a/b) by the pivot fraction (c/d).
    /// <br></br>
    /// - Add the result to the targeted fraction (e/f).
    /// </summary>
    /// <param name="scalar">The scaling factor represented as a fraction (a/b)</param>
    /// <param name="pivot">The pivot fraction (c/d)</param>
    /// <param name="targeted">The target fraction (e/f) to which the result is added</param>
    /// <remarks>- (a/b × c/d) + e/f = (ac/bd) + (e/f) = f(ac/bd) + bd(e/f) = (acf + ebd) / (fbd)</remarks>
    /// <returns>A new fraction resulting from the combination</returns>
    private static Fraction SpecialOp(Fraction scalar, Fraction pivot, Fraction targeted)
    {
        decimal a = scalar.Numerator, b = scalar.Denominator;
        decimal c = pivot.Numerator, d = pivot.Denominator;
        decimal e = targeted.Numerator, f = targeted.Denominator;
        decimal dividend = -a * c * f + e * b * d;
        decimal divisor = f * b * d;
        return new Fraction(dividend, divisor);
    }
    private static void Action(int pivotRow, int targetedRow, int columnStart, Fraction scalar, ref Fraction[,] matrix)
    {
        matrix[targetedRow, columnStart] = new(0);
        for (int y = columnStart + 1; y < matrix.GetLength(1); y++)
        {
            decimal testVal = -scalar.Quotient * matrix[pivotRow, y].Quotient + matrix[targetedRow, y].Quotient;
            if (testVal.IsDecimal())
            {
                matrix[targetedRow, y] = SpecialOp(scalar, matrix[pivotRow, y], matrix[targetedRow, y]);
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
                if (current == 1 || current == -1 || (current != 0 && current.IsDecimal() == false))
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