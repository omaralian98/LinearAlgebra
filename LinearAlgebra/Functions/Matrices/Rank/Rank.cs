namespace LinearAlgebra;

public partial class Linear
{
    /// <summary>
    /// Returns the Rank of a matrix
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to calculate its rank</param>
    /// <returns>The Rank of the matrix</returns>
    /// <exception cref="ArithmeticException"></exception>
    public static int Rank<T>(T[,] matrix) => RankClass.Rank(matrix.GetFractions());

    private class RankClass
    {

        public static int Rank(Fraction[,] matrix)
        {
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
}