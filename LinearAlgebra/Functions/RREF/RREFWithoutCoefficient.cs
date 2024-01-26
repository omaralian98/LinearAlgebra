namespace LinearAlgebra;

public partial class Linear
{
    //public static REFResult RREF(Fraction[,] matrix, bool solution = false, CancellationToken token = default)
    //{
    //    int matrixRows = matrix.GetLength(0); //Gets the number of rows
    //    int matrixColumns = matrix.GetLength(1); //Gets the number of columns
    //    List<MatrixStepForREF>? matrixSteps = solution ? [] : null;
    //    for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
    //    {
    //        if (token.IsCancellationRequested)
    //        {
    //            throw new TaskCanceledException("Task was canceled.");
    //        }
    //        ReOrderMatrix(matrix, currentRow, matrixSteps);
    //        int currentColumn = FindPivot(matrix, currentRow);
    //        if (currentColumn == -1) continue;
    //        ClearPivotColumn(matrix, currentRow, currentColumn, true, matrixSteps);
    //        ClearPivotRow(matrix, currentRow, currentColumn, matrixSteps);
    //    }
    //    return new REFResult { Matrix = matrix, NextStep = matrixSteps?.ToArray() ?? [] };
    //}

    //private static void ClearPivotRow(Fraction[,] matrix, int pivotRow, int pivotColumn, List<MatrixStepForREF>? solution)
    //{
    //    Fraction scalar = new (matrix[pivotRow, pivotColumn].Denominator, matrix[pivotRow, pivotColumn].Numerator);
    //    if (scalar.Numerator == 0) return;
    //    for (int column = 0; column < matrix.GetLength(1); column++)
    //    {
    //        matrix[pivotRow, column] *= scalar;
    //    }
    //    solution?.Add(new MatrixStepForREF
    //    {
    //        Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
    //        Matrix = (Fraction[,])matrix.Clone(),
    //    });
    //}
}
