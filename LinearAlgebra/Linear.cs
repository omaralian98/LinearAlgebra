using static LinearAlgebra.Linear;

namespace LinearAlgebra;
public class Linear
{
    public struct MatrixStep
    {
        public string? StepDescription { get; set; }
        public Fraction[,]? Matrix { get; set; }
        public SpecialString[]? Coefficient { get; set; }
    }
    public static List<MatrixStep> steps = new List<MatrixStep>();
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
        public static SpecialString operator *(Fraction a, SpecialString b) => b * a.Numerator / a.Denominator;
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
        public override string ToString() => str;
    }
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
        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator -(Fraction a) => new Fraction(-a.Numerator, a.Denominator);
        public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        public static Fraction operator /(Fraction a, Fraction b) => new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        public static Fraction operator +(Fraction a, object b)
        {
            if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invaild.");
            decimal num = a.Numerator + (decimal)b * a.Denominator;
            decimal den = a.Denominator;
            return new Fraction(num, den);
        }
        public static Fraction operator -(Fraction a, object b)
        {
            if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invaild.");
            decimal num = a.Numerator - (decimal)b * a.Denominator;
            decimal den = a.Denominator;
            return new Fraction(num, den);
        }
        public static Fraction operator *(Fraction a, object b)
        {
            if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invaild.");
            return new Fraction(a.Numerator * (decimal)b, a.Denominator);
        }
        public static Fraction operator /(Fraction a, object b)
        {
            if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invaild.");
            return new Fraction(a.Numerator, a.Denominator * (decimal)b);
        }
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
        //public static bool operator ==(Fraction a, Fraction b) => a.Numerator == b.Numerator && b.Numerator == a.Denominator;
        //public static bool operator !=(Fraction a, Fraction b) => !(a == b);

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
    public static int Rank(decimal[,] matrix)
    {
        steps.Add(new MatrixStep
        {
            StepDescription = "We get the Row Echelon Form(REF) of our matrix\nThen we count every non-zero row\nThe result is the rank of this matrix",
            Matrix = matrix.GetFractions()
        });
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

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>Returns the REF of the giving matrix as decimal array</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static decimal[,] REF<T>(T[,] matrix)
    {
        Fraction[,] newMatrix = matrix.GetFractions();
        (newMatrix,var coefficient) = REFAsFraction(newMatrix, null);
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
    public static Fraction[,] REFAsFraction<T>(T[,] matrix)
    {
        var (answer, coefficient) = REFAsFraction(matrix.GetFractions(), null);
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
    public static string[,] REFAsString<T>(T[,] matrix)
    {
        var (answer, coefficient) = REFAsFraction(matrix.GetFractions(), null);
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
    public static (Fraction[,], SpecialString[]?) REFAsSpecialString(Fraction[,] matrix, Fraction[]? coefficient = null)
    {
        if (coefficient is not null)
        {
            //If the matrix and the coefficient matrix has different number of rows throw an exception
            string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
            if (matrix.GetLength(0) != coefficient?.GetLength(0)) throw new ArgumentException(errorMessage);
        }
        SpecialString[]? specialStrings = coefficient?.GetFraction() ?? null;
        MatrixHelpers.Pivot(reduced: false, ref matrix, ref specialStrings);
        return (matrix, specialStrings);
    }
    public static (Fraction[,], Fraction[]?) REFAsFraction(Fraction[,] matrix, Fraction[]? coefficient = null)
    {
        var (result, coe) = REFAsSpecialString(matrix, coefficient);
        return (result, coe is null ? null : SpecialString.Solve(coe));
    }

    public static string[] REFGetCoefficientAsStrings<T>(T[,] matrix, T[] coefficient)
    {
        var (result, coe) = REFAsSpecialString(matrix.GetFractions(), coefficient.GetFractions());
        return coe.SpecialString2String();
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
    public static (decimal[,], decimal[]) REF<T>(T[,] matrix, T[] coefficient)
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
    public static (string[,], string[]) REFAsString<T>(T[,] matrix, T[] coefficient)
    {
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient.GetLength(0)) throw new ArgumentException(errorMessage);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2String(), coe.Fraction2String());
    }
    private class MatrixHelpers
    {
        public static void Pivot(bool reduced, ref Fraction[,] matrix, ref SpecialString[]? coefficient)
        {
            int matrixRows = matrix.GetLength(0); //Gets the number of rows
            int matrixColumns = matrix.GetLength(1); //Gets the number of columns
            for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
            {
                ReOrderMatrix(currentRow, ref matrix, ref coefficient);
                int currentColumn = FindPivot(currentRow, matrix);
                if (currentColumn == -1) continue;
                ClearPivotColumn(currentRow, currentColumn, reduced, ref matrix, ref coefficient);
            }
        }
        public static void ReOrderMatrix(int row, ref Fraction[,] matrix, ref SpecialString[]? coefficient)
        {
            var (result, x, y) = MatrixHelpers.CheckPossibleSwap(row, row, matrix);
            if (result)
            {
                MatrixHelpers.SwapMatrix(x, y, ref matrix);
                if (coefficient is not null) MatrixHelpers.SwapCoefficient(x, y, ref coefficient);
                steps.Add(new MatrixStep
                {
                    StepDescription = $"Swap between R{x + 1} and R{y + 1}",
                    Matrix = (Fraction[,])matrix.Clone(),
                    Coefficient = coefficient
                });
            }
        }
        public static int FindPivot(int row, Fraction[,] matrix)
        {
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                if (matrix[row, column].Quotient != 0) return column;
                var elements = Enumerable.Range(0, matrix.GetLength(0)).SkipWhile(x => x <= row)
                    .Select(x => matrix[x, column]).ToArray();
                if (elements.All(x => x.Quotient == 0)) continue;
                return column;
            }
            return -1;
        }
        public static void ClearPivotColumn(int pivotRow, int column, bool reduced, ref Fraction[,] matrix, ref SpecialString[]? coefficient)
        {
            int targetedRow = reduced ? 0 : pivotRow;
            for (; targetedRow < matrix.GetLength(0); targetedRow++)
            {
                if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
                Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
                ClearRow(pivotRow, targetedRow, column, scalar, ref matrix);
                if (coefficient is not null) coefficient[targetedRow] = scalar * coefficient[pivotRow] + coefficient[targetedRow];
                steps.Add(new MatrixStep
                {
                    StepDescription = $"{scalar}R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                    Matrix = (Fraction[,])matrix.Clone(),
                    Coefficient = coefficient
                });

            }
        }

        public static void ClearRow(int pivotRow, int targetedRow, int columnStart, Fraction scalar, ref Fraction[,] matrix)
        {
            matrix[targetedRow, columnStart] = new(0);
            for (int y = columnStart + 1; y < matrix.GetLength(1); y++)
            {
                var testVal = scalar * matrix[pivotRow, y] + matrix[targetedRow, y];
                if (testVal.Quotient.IsDecimal()) matrix[targetedRow, y] = testVal;
                else matrix[targetedRow, y] = new Fraction(testVal.Quotient);
            }
        }

        public static (bool, int, int) CheckPossibleSwap(int x, int y, Fraction[,] matrix)
        {//if the piviot is 0 than there is a sawp 
            if (matrix[x, y].Quotient == 0)
            {
                int num = -1;
                for (int i = x + 1; i < matrix.GetLength(0); i++)
                {//Loops through all the row to find a suitable row to swap
                    decimal current = matrix[i, y].Quotient;
                    //If we finds 1 or -1 or any number that's not 0.
                    if (current == 1 || current == -1 || current != 0)
                    {
                        num = i - x;
                        break;
                    }
                }//If num is still -1 that means all this column is 0 so we return false and -1, -1
                if (num == -1) return (false, -1, -1);
                return (true, x, y + num); //Else we return true and the coordinate of the row.
            }//If not we return false because we don't have too swap
            return (false, 0, 0);
        }
        public static void SwapMatrix<T>(int x, int y, ref T[,] matrix)
        {
            int columns = matrix.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                (matrix[x, i], matrix[y, i]) = (matrix[y, i], matrix[x, i]);
            }
        }
        public static void SwapCoefficient<T>(int x, int y, ref T[] coefficient)
        {
            (coefficient[x], coefficient[y]) = (coefficient[y], coefficient[x]);
        }
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
                matrix[i] = new(SpecialString.vari[i], new(Convert.ToDecimal(t)));
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
    public static string GetMatix<T>(this T[,] matrix)
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
    public static string GetMatix<T>(this T[] matrix)
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
    public static string GetMatix<T, S>(this (T[,] matrix, S[] coefficient) c)
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
        Console.WriteLine(matrix.GetMatix());
    }
    public static void Print<T>(this T[] matrix)
    {
        Console.WriteLine(matrix.GetMatix());
    }
    public static void Print<T, S>(this (T[,] matrix, S[] coefficient) c)
    {
        Console.WriteLine(c.GetMatix());
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
    public static char[] opeartions = { '*', '/', '+', '-' };
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
            if (opeartions.Contains(exp[i][0]) && exp[i].Length == 1)
            {
                decimal y = Convert.ToDecimal(stack.Pop());
                if (first)
                {
                    answer = new(y);
                    first = false;
                    y = Convert.ToDecimal(stack.Pop());
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

    public static List<string> InfixToPostfix(string exp)
    {
        List<string> result = new List<string>();
        Stack<string> stack = new Stack<string>();
        string[] ex = exp.Split();
        for (int i = 0; i < ex.Length; i++)
        {
            string cu = ex[i];
            if (decimal.TryParse(cu, out _)) result.Add(cu);
            else if (cu[0] == '(') stack.Push(cu);
            else if (cu[0] == ')')
            {
                while (stack.Count > 0 && stack.Peek() != "(") result.Add(stack.Pop());
                if (stack.Count > 0 && stack.Peek() != "(") throw new Exception("Invalid Expression");
                else stack.Pop();
            }
            else
            {
                while (stack.Count > 0 && Prec(cu[0]) <= Prec(stack.Peek()[0]))
                    result.Add(stack.Pop());
                stack.Push(cu);
            }
        }
        while (stack.Count > 0) result.Add(stack.Pop());
        return result;
    }
    private static int Prec(char c)
    {
        if (c == '-' || c == '+') return 1;
        if (c == '*' || c == '/') return 2;
        if (c == '^') return 3;
        return -1;
    }
}