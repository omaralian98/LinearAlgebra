// Ignore Spelling: cancellationoken

using MathNet.Numerics;

namespace LinearAlgebra;

public partial class Linear
{
    private partial class DeterminantClass
    {
        public static IEnumerable<REF_Result<Fraction>> DeterminantUsingREF(Fraction[,] matrix, out Fraction determinant, bool solution = false)
        {
            var refSteps = Row_Echelon_Form.REF(matrix, solution: true);
            List<REF_Result<Fraction>> steps = [];
            int opt = 0;
            string description = "";
            determinant = new(1);
            foreach (var step in refSteps)
            {
                if (step.Description.Contains("Swap")) opt++;
                if (solution) steps.Add(step);
            }
            var result = solution ?  steps[^1] : new REF_Result<Fraction>()
            {
                Matrix = matrix,
                Description = description
            };
            for (int i = 0; i < result.Matrix.GetLength(0); i++)
            {
                determinant *= result.Matrix[i, i];
                description += $" {result.Matrix[i, i]} *";
            }
            determinant *= opt.IsEven() ? 1 : -1;
            description = description[1..];
            description += $" (-1)^{opt} = {determinant}";
            result.Description += solution ? $"\n{description}": description;
            steps.Add(result);
            return steps;
        }

        public static Fraction DeterminantUsingREF(Fraction[,] matrix, CancellationToken cancellationoken = default)
        {
            int matrixRows = matrix.GetLength(0);
            int matrixColumns = matrix.GetLength(1);
            REF_Result<Fraction>? current = null;
            Fraction determinant = new(1);
            for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
            {
                if (cancellationoken.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = Row_Echelon_Form.FindPivot(matrix, currentRow);
                if (currentColumn == -1) break;
                if (matrix[currentRow, currentColumn].Quotient == 0) determinant *= -1;
                Row_Echelon_Form.ReOrderMatrix(matrix, currentRow, currentColumn, ref current);
                if (matrix[currentRow, currentColumn].Quotient == 0) continue;
                Row_Echelon_Form.ClearPivotColumn(matrix, currentRow, currentColumn, reduced: false, solution: false).Count();
                determinant *= matrix[currentRow, currentColumn];
            }
            return determinant;
        }
    }
}
