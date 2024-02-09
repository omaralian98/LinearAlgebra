namespace LinearAlgebra;

public partial class Linear
{
    public partial class Row_Echelon_Form
    {
        public static IEnumerable<REF_Result<T>> RREF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken token = default) where T : ICoefficient
        {
            int matrixRows = matrix.GetLength(0); //Gets the number of rows
            int matrixColumns = matrix.GetLength(1); //Gets the number of columns
            if (solution) yield return new()
            {
                Matrix = (Fraction[,])matrix.Clone(),
                Coefficient = (T[])coefficient.Clone()
            };
            for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
            {
                REF_Result<T>? current = solution ? new() : null;
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = FindPivot(matrix, currentRow);
                if (currentColumn == -1) continue;
                ReOrderMatrix(matrix, coefficient, currentRow, currentColumn, ref current);
                if (current?.Matrix.GetLength(0) != 0 && solution) yield return current!;
                var cur = ClearPivotColumn(matrix, coefficient, currentRow, currentColumn, reduced: true, solution);
                foreach (var step in cur)
                {
                    yield return step;
                }
                ClearPivotRow(matrix, coefficient, currentRow, currentColumn, ref current);
                if (current?.Matrix.GetLength(0) != 0 && solution) yield return current!;
            }
            if (!solution)
            {
                yield return new() { Matrix = matrix, Coefficient = coefficient };
            }
        }

        private static void ClearPivotRow<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int pivoColumn, ref REF_Result<T>? solution) where T : ICoefficient
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
                solution = new REF_Result<T>
                {
                    Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
                    Coefficient = (T[])coefficient.Clone(),
                    Matrix = (Fraction[,])matrix.Clone(),
                };
            }
        }
    }
}