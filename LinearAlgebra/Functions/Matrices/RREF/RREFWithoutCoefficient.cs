// Ignore Spelling: cancellationToken RREF

using LinearAlgebra.Classes.SolutionSteps;

namespace LinearAlgebra;

public partial class Linear
{
    public static T[,] RREF<T>(T[,] matrix, CancellationToken cancellationToken = default) 
        => RREF<T, T>(matrix, cancellationToken);
    public static REF_Result<T>[] RREFWithDecimal<T>(T[,] matrix, CancellationToken cancellationToken = default)
        => RREFWithResult<T, T>(matrix, cancellationToken);


    /// <summary>
    /// Returns the RREF of the given matrix
    /// </summary>
    /// <typeparam name="R">Type of the returned matrix</typeparam>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>2d array of type R that represent the matrix</returns>
    public static R[,] RREF<R, T>(T[,] matrix, CancellationToken cancellationToken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), cancellationToken: cancellationToken).First();
        return solution.Matrix.GetTMatrix<R>();
    }

    /// <summary>
    /// Returns the RREF of the given matrix with stpes
    /// </summary>
    /// <typeparam name="R">Type of the returned matrices</typeparam>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>an array of RREF_Result R every element of this array represent a step in this operation 1st element is the given matrix, 2nd is the next step<br></br> the step is described in a string</returns>
    public static REF_Result<R>[] RREFWithResult<R, T>(T[,] matrix, CancellationToken cancellationToken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), solution: true, cancellationToken: cancellationToken);
        var result = new List<REF_Result<R>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<R>
            {
                Matrix = step.Matrix.GetTMatrix<R>(),
                Description = step.Description
            });
        }
        return [.. result];
    }


    private partial class Row_Echelon_Form
    {
        public static IEnumerable<REF_Result<Fraction>> RREF(Fraction[,] matrix, bool solution = false, CancellationToken cancellationToken = default)
        {
            int matrixRows = matrix.GetLength(0); //Gets the number of rows
            int matrixColumns = matrix.GetLength(1); //Gets the number of columns
            if (solution)
            {
                yield return new REF_Result<Fraction> { Matrix = (Fraction[,])matrix.Clone() };
            }
            for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
            {
                REF_Result<Fraction>? current = solution ? new() : null;
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = FindPivot(matrix, currentRow);
                if (currentColumn == -1) break;
                ReOrderMatrix(matrix, currentRow, currentColumn, ref current);
                if (matrix[currentRow, currentColumn].Quotient == 0) continue;
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
                yield return new REF_Result<Fraction> { Matrix = matrix };
            }
        }

        /// <summary>
        /// Clears the pivot row by multiplying 1/{pivot}
        /// </summary>
        /// <param name="matrix">The matrix</param>
        /// <param name="pivotRow">Index of pivot row</param>
        /// <param name="pivotColumn">Index of pivot column</param>
        /// <param name="solution">Solution</param>
        private static void ClearPivotRow(Fraction[,] matrix, int pivotRow, int pivotColumn, ref REF_Result<Fraction>? solution)
        {
            //Define the scalar 1/{pivot}
            Fraction scalar = new(matrix[pivotRow, pivotColumn].Denominator, matrix[pivotRow, pivotColumn].Numerator);

            //If the pivot is 1 then we don't have to do anything
            if (scalar.Quotient == 1) return;

            //Else loop through the row and multiply by the scalar
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[pivotRow, column] *= scalar;
            }

            //If the solution is not null then we assign to it this step
            if (solution is not null)
            {
                solution = new REF_Result<Fraction>
                {
                    Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
                    Matrix = (Fraction[,])matrix.Clone(),
                };
            }
        }
    }
}