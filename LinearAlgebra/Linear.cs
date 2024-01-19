namespace LinearAlgebra;

public partial class Linear
{
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