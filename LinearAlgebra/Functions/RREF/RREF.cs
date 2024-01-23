namespace LinearAlgebra;

public partial class Linear
{
    public static (Fraction[,], Steps[]) RREF(Fraction[,] matrix, CancellationToken token = default)
    {
        int matrixRows = matrix.GetLength(0); //Gets the number of rows
        int matrixColumns = matrix.GetLength(1); //Gets the number of columns
        List<Steps> steps = [];
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            (matrix, steps) = ReOrderMatrix(matrix, steps, currentRow, new List<MatrixSteps>());
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            (matrix, steps) = ClearPivotColumn(matrix, steps, currentRow, currentColumn, true, new List<MatrixSteps>());
            (matrix, steps) = ClearPivotRow(matrix, steps, currentRow, currentColumn);
        }
        return (matrix, steps.ToArray());
    }

    private static (Fraction[,], List<Steps>) ClearPivotRow(Fraction[,] matrix, List<Steps> steps, int pivotRow, int pivoColumn)
    {
        Fraction scalar = new (matrix[pivotRow, pivoColumn].Denominator, matrix[pivotRow, pivoColumn].Numerator);
        if (scalar.Numerator == 0) return (matrix, steps);
        for (int column = 0; column < matrix.GetLength(1); column++)
        {
            matrix[pivotRow, column] *= scalar;
        }
        steps.Add(new Steps { PivotRow = pivotRow, EffectedRow = pivotRow, Scalar = scalar, Operation = Operations.Scale });
        return (matrix, steps);
    }
}
