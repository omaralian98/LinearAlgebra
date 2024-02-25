namespace LinearAlgebra;

public partial class Linear
{
    public static T[,] Pow<T>(T[,] matrix, int n) => Pow<T, T>(matrix, n);
    public static R[,] Pow<R, T>(T[,] matrix, int n) => Power.Pow(matrix.GetFractions(), n).GetTMatrix<R>();

    private class Power
    {
        public static Fraction[,] Pow(Fraction[,] matrix, int n)
        {
            if (n == 1) return matrix;
            else if (n == 2) return Multiply<Fraction, Fraction>(matrix, matrix);
            else if ((n & 1) == 0) // If n is even
            {
                var result = Pow(matrix, n / 2);
                return Multiply<Fraction, Fraction>(result, result);
            }
            else if ((n & 1) == 1) //If n is odd
            {
                var result = Pow(matrix, (n - 1) / 2);
                return Multiply<Fraction, Fraction>(result, result, matrix);
            }
            throw new NotImplementedException();
        }
    }
}