namespace LinearAlgebra;

public partial class Linear
{
    //public static REFResult<T> RREF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken token = default) where T : ICoefficient
    //{
    //    int matrixRows = matrix.GetLength(0); //Gets the number of rows
    //    int matrixColumns = matrix.GetLength(1); //Gets the number of columns
    //    List<MatrixStepForREF<T>>? matrixSteps = solution ? [] : null;
    //    for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
    //    {
    //        if (token.IsCancellationRequested)
    //        {
    //            throw new TaskCanceledException("Task was canceled.");
    //        }
    //        ReOrderMatrix(matrix, coefficient, currentRow, matrixSteps);
    //        int currentColumn = FindPivot(matrix, currentRow);
    //        if (currentColumn == -1) continue;
    //        ClearPivotColumn(matrix, coefficient, currentRow, currentColumn, reduced: true, matrixSteps);
    //        ClearPivotRow(matrix, coefficient, currentRow, currentColumn, matrixSteps);
    //    }
    //    return new REFResult<T> { Matrix = matrix, NextStep = matrixSteps?.ToArray() ?? [], Coefficient = coefficient };
    //}

    //private static void ClearPivotRow<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int pivoColumn, List<MatrixStepForREF<T>>? solution) where T : ICoefficient 
    //{
    //    Fraction scalar = new(matrix[pivotRow, pivoColumn].Denominator, matrix[pivotRow, pivoColumn].Numerator);
    //    if (scalar.Quotient == 1) return;
    //    for (int column = 0; column < matrix.GetLength(1); column++)
    //    {
    //        matrix[pivotRow, column] *= scalar;
    //    }
    //    coefficient[pivotRow] = (T)(coefficient[pivotRow] * scalar);
    //    solution?.Add(new MatrixStepForREF<T>
    //    {
    //        Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
    //        Coefficient = coefficient.Clone() as T[],
    //        Matrix = matrix.Clone() as Fraction[,],
    //    });
    //}
}