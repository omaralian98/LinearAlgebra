namespace LinearAlgebra;

public partial class Linear
{
    public partial class Row_Echelon_Form
    {
        public static IEnumerable<REF_Result> RREF(Fraction[,] matrix, bool solution = false, CancellationToken token = default)
        {
            int matrixRows = matrix.GetLength(0); //Gets the number of rows
            int matrixColumns = matrix.GetLength(1); //Gets the number of columns
            if (solution)
            {
                yield return new REF_Result { Matrix = (Fraction[,])matrix.Clone() };
            }
            for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
            {
                REF_Result? current = solution ? new() : null;
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = FindPivot(matrix, currentRow);
                if (currentColumn == -1) continue;
                ReOrderMatrix(matrix, currentRow, currentColumn, ref current);
                if (current?.Matrix.GetLength(0) != 0 && solution) yield return current!;
                var cur = ClearPivotColumn(matrix, currentRow, currentColumn, reduced: true, solution);
                foreach (var step in cur)
                {
                    yield return step;
                }
                ClearPivotRow(matrix, currentRow, currentColumn, ref current);
                if (current?.Matrix.GetLength(0) != 0 && solution) yield return current!;
            }
            if (solution == false)
            {
                yield return new REF_Result { Matrix = matrix };
            }
        }

        private static void ClearPivotRow(Fraction[,] matrix, int pivotRow, int pivotColumn, ref REF_Result? solution)
        {
            Fraction scalar = new(matrix[pivotRow, pivotColumn].Denominator, matrix[pivotRow, pivotColumn].Numerator);
            if (scalar.Quotient == 1) return;
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[pivotRow, column] *= scalar;
            }

            if (solution is not null)
            {
                solution = new REF_Result
                {
                    Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
                    Matrix = (Fraction[,])matrix.Clone(),
                };
            }
        }
    }

}