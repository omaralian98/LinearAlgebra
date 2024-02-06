namespace LinearAlgebra;

public partial class Linear
{
    internal class Power
    {
        public static Fraction[,] Pow(Fraction[,] matrix, int n)
        {
            if (n == 1) return matrix;
            return new Fraction[0,0];
        }
    }
}