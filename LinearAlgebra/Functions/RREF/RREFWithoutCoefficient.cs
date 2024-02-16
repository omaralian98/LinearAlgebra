// Ignore Spelling: cancellationoken RREF

namespace LinearAlgebra;

public partial class Linear
{
    /// <summary>
    /// Returns the RREF of the given matrix
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>2d array of type Decimal that represent the matrix</returns>
    public static Fraction[,] RREFAsFraction<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), cancellationoken: cancellationoken).First();
        return solution.Matrix;
    }

    /// <summary>
    /// Returns the RREF of the given matrix
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="decimals">The number of decimals you want to round up to</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>2d array of type Fraction that represent the matrix</returns>
    public static decimal[,] RREFAsDecimal<T>(T[,] matrix, int decimals = -1, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), cancellationoken: cancellationoken).First();
        return solution.Matrix.Fraction2Decimal(decimals);
    }

    /// <summary>
    /// Returns the RREF of the given matrix
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>2d array of type String that represent the matrix</returns>
    public static string[,] RREFAsString<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), cancellationoken: cancellationoken).First();
        return solution.Matrix.Fraction2String();
    }



    /// <summary>
    /// Returns the RREF of the given matrix with stpes
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>an array of RREF_Result Fraction every element of this array represent a step in this operation 1st element is the given matrix, 2nd is the next step<br></br> the step is described in a string</returns>
    public static REF_Result<Fraction>[] RREFWithFraction<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), solution: true, cancellationoken: cancellationoken);
        return [.. solution];
    }

    /// <summary>
    /// Returns the RREF of the given matrix with stpes
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>an array of RREF_Result String every element of this array represent a step in this operation 1st element is the given matrix, 2nd is the next step<br></br> the step is described in a string</returns>
    public static REF_Result<string>[] RREFWithString<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), solution: true, cancellationoken: cancellationoken);
        var result = new List<REF_Result<string>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<string>
            {
                Matrix = step.Matrix.Fraction2String(),
                Description = step.Description
            });
        }
        return [.. result];
    }

    /// <summary>
    /// Returns the RREF of the given matrix with stpes
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>an array of RREF_Result Decimal every element of this array represent a step in this operation 1st element is the given matrix, 2nd is the next step<br></br> the step is described in a string</returns>
    public static REF_Result<decimal>[] RREFWithDecimal<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), solution: true, cancellationoken: cancellationoken);
        var result = new List<REF_Result<decimal>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<decimal>
            {
                Matrix = step.Matrix.Fraction2Decimal(),
                Description = step.Description
            });
        }
        return [.. result];
    }


    private partial class Row_Echelon_Form
    {
        public static IEnumerable<REF_Result<Fraction>> RREF(Fraction[,] matrix, bool solution = false, CancellationToken cancellationoken = default)
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
                if (cancellationoken.IsCancellationRequested)
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