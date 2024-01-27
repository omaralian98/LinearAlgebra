namespace LinearAlgebra;

public partial class Linear
{
    public static REFResult RREF(Fraction[,] matrix, CancellationToken token = default)
    {
        int matrixRows = matrix.GetLength(0); //Gets the number of rows
        int matrixColumns = matrix.GetLength(1); //Gets the number of columns
        REFResult result = new() { Matrix = (Fraction[,])matrix.Clone() };
        REFResult current = result;
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            ReOrderMatrix(matrix, currentRow, ref current);
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            ClearPivotColumn(matrix, currentRow, currentColumn, true, ref current);
            ClearPivotRow(matrix, currentRow, currentColumn, ref current);
        }
        return result;
    }

    private static void ClearPivotRow(Fraction[,] matrix, int pivotRow, int pivotColumn, ref REFResult solution)
    {
        Fraction scalar = new(matrix[pivotRow, pivotColumn].Denominator, matrix[pivotRow, pivotColumn].Numerator);
        if (scalar.Numerator == 0) return;
        for (int column = 0; column < matrix.GetLength(1); column++)
        {
            matrix[pivotRow, column] *= scalar;
        }
        solution.NextStep = new REFResult
        {
            Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
            Matrix = (Fraction[,])matrix.Clone(),
        };
    }
}
