namespace LinearAlgebra.Functions;

public partial class Row_Echelon_Form
{
    public static REFResult<T> RREF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken token = default) where T : ICoefficient
    {
        int matrixRows = matrix.GetLength(0); //Gets the number of rows
        int matrixColumns = matrix.GetLength(1); //Gets the number of columns
        REFResult<T>? result = solution ? new()
        {
            Matrix = (Fraction[,])matrix.Clone(),
            Coefficient = (T[])coefficient.Clone()
        } : null;
        REFResult<T>? current = result;
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            ReOrderMatrix(matrix, coefficient, currentRow, currentColumn, ref current);
            ClearPivotColumn(matrix, coefficient, currentRow, currentColumn, reduced: true, ref current);
            ClearPivotRow(matrix, coefficient, currentRow, currentColumn, ref current);
        }
        return result is not null ? result : new REFResult<T> { Matrix = matrix, Coefficient = coefficient };
    }

    private static void ClearPivotRow<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int pivoColumn, ref REFResult<T>? solution) where T : ICoefficient
    {
        Fraction scalar = new(matrix[pivotRow, pivoColumn].Denominator, matrix[pivotRow, pivoColumn].Numerator);
        if (scalar.Quotient == 1) return;
        for (int column = 0; column < matrix.GetLength(1); column++)
        {
            matrix[pivotRow, column] *= scalar;
        }
        coefficient[pivotRow] = (T)(coefficient[pivotRow] * scalar);

        if (solution is not null)
        {
            solution.NextStep = new REFResult<T>
            {
                Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
                Coefficient = (T[])coefficient.Clone(),
                Matrix = (Fraction[,])matrix.Clone(),
            };
            solution = solution.NextStep;
        }
    }
}