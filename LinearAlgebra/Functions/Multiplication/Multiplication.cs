namespace LinearAlgebra;

public partial class Linear
{
    private static void CheckCoherenceForMuliplication<T>(T[,] a, T[,] b)
    {
        string errorMessage = $"The number of columns in matrix A must be equal to the number of rows in column B";
        if (a.GetLength(1) != b.GetLength(0)) throw new InvalidOperationException(errorMessage);
    }

    public class Multiplication
    {
        public static Fraction[,] Multiply(Fraction[,] a, Fraction[,] b)
        {
            int m = a.GetLength(0);
            int q = b.GetLength(1);
            Fraction[,] result = new Fraction[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    var row = a.GetRow(i);
                    var column = b.GetColumn(j);
                    result[i, j] = Multiply(row, column);
                }
            }
            return result;
        }

        public static Fraction Multiply(Fraction[] row, Fraction[] column)
        {
            Fraction result = row[0] * column[0];
            for (int i = 1; i < row.Length; i++) 
            {
                result += row[i] * column[i];
            }
            return result;
        }

        public static Fraction[,] Multiply(Fraction[,] matrix, Fraction scalar)
        {
            for (int i = 0; i < matrix.GetLength(0); i++) 
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matrix[i, j] *= scalar;
                }
            }
            return matrix;
        }
    }
}
