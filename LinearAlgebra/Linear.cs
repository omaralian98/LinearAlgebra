using System.Runtime.InteropServices;
using static LinearAlgebra.Linear;

namespace LinearAlgebra;
public static class Linear
{
    
    private class MatrixStep
    {
        public string? StepDescription { get; set; }
        public Fraction[,]? Matrix { get; set; }
    }
    private static List<MatrixStep> steps = new List<MatrixStep>();
    public struct Fraction
    {
        public decimal Numerator;
        public decimal Denominator;
        public Fraction(decimal num, decimal den = 1)
        {
            if (den == 0) throw new DivideByZeroException();
            decimal gcd = GCD(Math.Abs(num), Math.Abs(den));
            Denominator = den / gcd;
            Numerator = num / gcd;
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
        public static Fraction operator *(Fraction a, Fraction b) => 
            new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        public static Fraction operator /(Fraction a, Fraction b) => 
            new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        public static Fraction operator +(Fraction a, Fraction b)
        {
            decimal dividend = (a.Numerator * b.Denominator) + (b.Numerator * a.Denominator);
            decimal divisor = a.Denominator * b.Denominator;
            return new Fraction(dividend, divisor);
        }
        public static Fraction operator -(Fraction a, Fraction b)
        {
            decimal dividend = (a.Numerator * b.Denominator) - (b.Numerator * a.Denominator);
            decimal divisor = a.Denominator * b.Denominator;
            return new Fraction(dividend, divisor);
        }
        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator -(Fraction a) => new Fraction(-a.Numerator, a.Denominator);

        public override string ToString()
        {
            if (Denominator == 1) return Numerator.ToString();
            else if (!Quotient.IsDecimal()) return Quotient.ToString();
            return $"{Numerator}/{Denominator}";
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
    /// <returns>Returns the REF of the giving matrix as decimal array</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static decimal[,] REF<T>(this T[,] matrix)
    {
        Fraction[,] newMatrix = matrix.GetFractions();
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        (newMatrix, coefficient) = REFAsFraction(newMatrix, coefficient);
        return newMatrix.Fraction2Decimal();
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>
    /// Returns the REF of the giving matrix as Fraction array
    /// <br></br>
    /// **Note**: Fraction is a struct that you can access like this:
    /// <br></br>
    /// LinearAlgebra.Linear.Fraction
    /// </returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static Fraction[,] REFAsFraction<T>(this T[,] matrix)
    {
        Fraction[,] answer;
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        (answer, coefficient) = REFAsFraction(matrix.GetFractions() ,coefficient);
        return answer;
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>Returns the REF of the giving matrix as string array</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static string[,] REFAsString<T>(this T[,] matrix)
    {
        Fraction[,] answer;
        Fraction[] coefficient = Enumerable.Repeat(new Fraction(0), matrix.GetLength(0)).ToArray();
        (answer, coefficient) = matrix.GetFractions().REFAsFraction(coefficient);
        return answer.Fraction2String();
    }
    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>
    /// Returns the REF of the giving matrix, and it's coefficient as Fraction arraies
    /// <br></br>
    /// **Note**: Fraction is a struct that you can access like this:
    /// <br></br>
    /// LinearAlgebra.Linear.Fraction
    /// </returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (Fraction[,], Fraction[]) REFAsFraction(this Fraction[,] matrix, Fraction[] coefficient)
    {
        //If the matrix and the coefficient matrix has different number of rows throw an exception
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient.GetLength(0)) throw new ArgumentException(errorMessage);
        int row = matrix.GetLength(0); //Gets the number of rows
        int col = matrix.GetLength(1); //Gets the number of columns
        for (int x = 0; x < Math.Min(row, col); x++) //we are getting the min becuase this is the number of the piviots
        {// if we have 2×4 matrix or 4×2 the number of piviots is 2 aka the min(2, 4)
            int y = x;
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
    /// <returns>Returns the REF of the giving matrix, and it's coefficient as decimal arraies</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (decimal[,], decimal[]) REF<T>(this T[,] matrix, T[] coefficient)
    {
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient.GetLength(0)) throw new ArgumentException(errorMessage);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2Decimal(), coe.Fraction2Decimal());
    }
    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>Returns the REF of the giving matrix, and it's coefficient as string arraies</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (string[,], string[]) REFAsString<T>(this T[,] matrix, T[] coefficient)
    {
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient.GetLength(0)) throw new ArgumentException(errorMessage);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2String(), coe.Fraction2String());
    }

    private static void Pivot(int x, int y, ref Fraction[,] matrix, ref Fraction[] coefficient)
    {

        for (int i = x + 1; i < matrix.GetLength(0); i++)
        {
            if (matrix[i, y].Quotient == 0) continue;
            Fraction scalar = matrix[i, y] / matrix[x, y];
            Action(pivotRow: x, targetedRow: i, columnStart: y, scalar, ref matrix);
            coefficient[i] = -scalar * coefficient[x] + coefficient[i];
        }
    }

    private static void Action(int pivotRow, int targetedRow, int columnStart, Fraction scalar, ref Fraction[,] matrix)
    {
        matrix[targetedRow, columnStart] = new(0);
        for (int y = columnStart + 1; y < matrix.GetLength(1); y++)
        {
            var testVal = -scalar * matrix[pivotRow, y] + matrix[targetedRow, y];
            if (testVal.Quotient.IsDecimal()) matrix[targetedRow, y] = testVal;
            else matrix[targetedRow, y] = new Fraction(testVal.Quotient);
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
                a[i, j] = t[i, j].Quotient;
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
                    matrix[i, j] = new Fraction(Convert.ToDecimal(t));
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
                matrix[i] = new Fraction(Convert.ToDecimal(t));
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
        decimal dividend = Convert.ToDecimal(a[0..indexOfSlash]);
        decimal divisor = Convert.ToDecimal(a[(indexOfSlash + 1)..a.Length]);
        return new Fraction(dividend, divisor);
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
    public static void PrintMatrix<T>(this T[] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.WriteLine(" {0}", matrix[i]);
        }
    }
}