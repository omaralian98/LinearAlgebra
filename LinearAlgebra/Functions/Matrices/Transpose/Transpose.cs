namespace LinearAlgebra;

public partial class Linear
{
    public static T[,] Transpose<T>(T[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        var transpose = new T[columns, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                transpose[j, i] = matrix[i, j];
            }
        }
        return transpose;
    }
}
