namespace LinearAlgebra;

public partial class Linear
{
    public static REFResult<T> RREF<T>(Fraction[,] matrix, T[] coefficient, CancellationToken token = default) where T : ICoefficient
    {
        int matrixRows = matrix.GetLength(0); //Gets the number of rows
        int matrixColumns = matrix.GetLength(1); //Gets the number of columns
        REFResult<T> result = new()
        {
            Matrix = (Fraction[,])matrix.Clone(),
            Coefficient = (T[])coefficient.Clone()
        };
        REFResult<T> current = result;
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            ReOrderMatrix(matrix, coefficient, currentRow, ref current);
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            ClearPivotColumn(matrix, coefficient, currentRow, currentColumn, reduced: true, ref current);
            ClearPivotRow(matrix, coefficient, currentRow, currentColumn, ref current);
        }
        return result;
    }

    private static void ClearPivotRow<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int pivoColumn, ref REFResult<T> solution) where T : ICoefficient
    {
        Fraction scalar = new(matrix[pivotRow, pivoColumn].Denominator, matrix[pivotRow, pivoColumn].Numerator);
        if (scalar.Quotient == 1) return;
        for (int column = 0; column < matrix.GetLength(1); column++)
        {
            matrix[pivotRow, column] *= scalar;
        }
        coefficient[pivotRow] = (T)(coefficient[pivotRow] * scalar);

        solution.NextStep = new REFResult<T>
        {
            Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
            Coefficient = (T[])coefficient.Clone(),
            Matrix = (Fraction[,])matrix.Clone(),
        };
    }
}