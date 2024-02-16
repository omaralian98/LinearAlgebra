// Ignore Spelling: cancellationoken

using LinearAlgebra.Classes;

namespace LinearAlgebra;

public partial class Linear
{
    /// <summary>
    /// Returns the REF of the given matrix
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>2d array of type Decimal that represent the matrix</returns>
    public static Fraction[,] REFAsFraction<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), cancellationoken: cancellationoken).First();
        return solution.Matrix;
    }

    /// <summary>
    /// Returns the REF of the given matrix
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="decimals">The number of decimals you want to round up to</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>2d array of type Fraction that represent the matrix</returns>
    public static decimal[,] REFAsDecimal<T>(T[,] matrix, int decimals = -1, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), cancellationoken: cancellationoken).First();
        return solution.Matrix.Fraction2Decimal(decimals);
    }

    /// <summary>
    /// Returns the REF of the given matrix
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>2d array of type String that represent the matrix</returns>
    public static string[,] REFAsString<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), cancellationoken: cancellationoken).First();
        return solution.Matrix.Fraction2String();
    }



    /// <summary>
    /// Returns the REF of the given matrix with stpes
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>an array of REF_Result Fraction every element of this array represent a step in this operation 1st element is the given matrix, 2nd is the next step<br></br> the step is described in a string</returns>
    public static REF_Result<Fraction>[] REFWithFraction<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), solution: true, cancellationoken: cancellationoken);
        return [.. solution];
    }

    /// <summary>
    /// Returns the REF of the given matrix with stpes
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>an array of REF_Result String every element of this array represent a step in this operation 1st element is the given matrix, 2nd is the next step<br></br> the step is described in a string</returns>
    public static REF_Result<string>[] REFWithString<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), solution: true, cancellationoken: cancellationoken);
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
    /// Returns the REF of the given matrix with stpes
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <param name="cancellationToken">The token that can be used to cancel the operation</param>
    /// <returns>an array of REF_Result Decimal every element of this array represent a step in this operation 1st element is the given matrix, 2nd is the next step<br></br> the step is described in a string</returns>
    public static REF_Result<decimal>[] REFWithDecimal<T>(T[,] matrix, CancellationToken cancellationoken = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), solution: true, cancellationoken: cancellationoken);
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
        public static IEnumerable<REF_Result<Fraction>> REF(Fraction[,] matrix, bool solution = false, CancellationToken cancellationoken = default)
        {
            int matrixRows = matrix.GetLength(0);
            int matrixColumns = matrix.GetLength(1);
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
                var cur = ClearPivotColumn(matrix, currentRow, currentColumn, reduced: false, solution);
                foreach (var step in cur)
                {
                    yield return step;
                }
            }
            if (solution == false)
            {
                yield return new REF_Result<Fraction> { Matrix = matrix };
            }
        }

        /// <summary>
        /// Reorder a matrix accordingly
        /// </summary>
        /// <param name="matrix">The matrix</param>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        /// <param name="solution">Solution</param>
        public static void ReOrderMatrix(Fraction[,] matrix, int x, int y, ref REF_Result<Fraction>? solution)
        {
            //Gets the Index of a possible swap
            y = CheckPossibleSwap(x, y, matrix);
            //If there is a swap
            if (y > 0)
            {
                //Swap the matrix
                matrix = SwapMatrix(x, y, matrix);

                //If the solution is not null then we assign to it this step
                if (solution is not null)
                {
                    solution = new REF_Result<Fraction>
                    {
                        Description = $"Swap between R{x + 1} and R{y + 1}",
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                }
            }
        }

        /// <summary>
        /// Clears a column making all elements equal to 0 except for the pivot
        /// </summary>
        /// <param name="matrix">The matrix</param>
        /// <param name="pivotRow">Index of pivot row</param>
        /// <param name="column">Index of the targeted column</param>
        /// <param name="reduced">Whether to apply REF Or RREF</param>
        /// <param name="solution">Whether to return the steps of this operation or not</param>
        /// <returns>IEnumerable of REF_Result of type Fraction<br></br><br></br>Note: If the solution is not required nothing will be returned</returns>
        public static IEnumerable<REF_Result<Fraction>> ClearPivotColumn(Fraction[,] matrix, int pivotRow, int column, bool reduced, bool solution = false)
        {
            //If we want REF we start our operation from the pivot row
            //If we want RREF we start from the beginning
            int targetedRow = reduced ? 0 : pivotRow;

            //Looping through this column
            for (; targetedRow < matrix.GetLength(0); targetedRow++)
            {//If the current row is the pivot or it's already 0 skip it
                if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;

                //Else define a scalar = -{Target}/{Pivot}
                Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];

                //Apply this scalar to the entire row
                matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);

                if (solution)
                {//If we want the solution step by step return this step
                    yield return new REF_Result<Fraction>
                    {
                        Description = $"{scalar} R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                }
            }
        }
    }
}