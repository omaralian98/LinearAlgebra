﻿namespace LinearAlgebra;

public partial class Linear
{
    public static Result<T> RREF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken token = default) where T : ICoefficient
    {
        int matrixRows = matrix.GetLength(0); //Gets the number of rows
        int matrixColumns = matrix.GetLength(1); //Gets the number of columns
        List<MatrixStep<T>>? matrixSteps = solution ? [] : null;
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            ReOrderMatrix(matrix, coefficient, currentRow, matrixSteps);
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            ClearPivotColumn(matrix, coefficient, currentRow, currentColumn, reduced: true, matrixSteps);
            ClearPivotRow(matrix, coefficient, currentRow, currentColumn, matrixSteps);
        }
        return new Result<T> { Matrix = matrix, MatrixSteps = matrixSteps?.ToArray() ?? [] };
    }

    private static void ClearPivotRow<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int pivoColumn, List<MatrixStep<T>>? solution) where T : ICoefficient 
    {
        Fraction scalar = new(matrix[pivotRow, pivoColumn].Denominator, matrix[pivotRow, pivoColumn].Numerator);
        if (scalar.Quotient == 1) return;
        for (int column = 0; column < matrix.GetLength(1); column++)
        {
            matrix[pivotRow, column] *= scalar;
        }
        solution?.Add(new MatrixStep<T>
        {
            Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
            Coefficient = coefficient.Clone() as T[],
            Matrix = matrix.Clone() as Fraction[,],
        });
    }
}