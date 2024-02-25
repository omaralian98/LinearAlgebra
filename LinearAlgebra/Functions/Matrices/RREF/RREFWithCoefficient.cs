namespace LinearAlgebra;

public partial class Linear
{
    public static REF_Result<T, S> RREF<T, S>(T[,] matrix, S[] coefficient, CancellationToken cancellationToken = default)
        => RREF<T, S, T, S>(matrix, coefficient, cancellationToken);
    public static REF_Result<T, string> RREFVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken cancellationToken = default)
        => RREFVariable<T, T>(matrix, coefficient, cancellationToken);
    public static REF_Result<T, S>[] RREFWithResult<T, S>(T[,] matrix, S[] coefficient, CancellationToken cancellationToken = default)
        => RREFWithResult<T, S, T, S>(matrix, coefficient, cancellationToken);
    public static REF_Result<T, string>[] RREFWithVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken cancellationToken = default)
        => RREFWithVariable<T, T>(matrix, coefficient, cancellationToken);


    /// <summary>
    /// Returns the RREF of the given matrix and coefficient.
    /// </summary>
    /// <typeparam name="R1">The type of the returned matrix</typeparam>
    /// <typeparam name="R2">The type of the returned coefficient</typeparam>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's RREF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>2d array of type R1 that represent the matrix and a R2 array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<R1, R2> RREF<R1, R2, T, S>(T[,] matrix, S[] coefficient, CancellationToken cancellationToken = default)
    {
        CheckCoherenceForREF(matrix, coefficient);
        CheckForS(typeof(S));
        var result = Row_Echelon_Form.RREF(matrix.GetFractions(), coefficient.GetFractions(), cancellationToken: cancellationToken).First();
        return new REF_Result<R1, R2> { Matrix = result.Matrix.GetTMatrix<R1>(), Coefficient = result.Coefficient.GetTMatrix<R2>() };
    }

    /// <summary>
    /// Returns the RREF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="R">The type of the returned matrix</typeparam>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's RREF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="cancellationToken"></param>
    /// <returns>2d array of type R that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<R, string> RREFVariable<R, T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken cancellationToken = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var result = Row_Echelon_Form.RREF(matrix.GetFractions(), coefficient, cancellationToken: cancellationToken).First();
        return new REF_Result<R, string> { Matrix = result.Matrix.GetTMatrix<R>(), Coefficient = result.Coefficient.SpecialString2String() };
    }

    /// <summary>
    /// Returns The RREF of the given matrix and coefficient
    /// </summary>
    /// <typeparam name="R1">The type of the returned matrix</typeparam>
    /// <typeparam name="R2">The type of the returned coefficient</typeparam>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's RREF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="cancellationToken"></param>
    /// <returns>2d array of type R1 that represent the matrix and a R2 array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<R1, R2>[] RREFWithResult<R1, R2, T, S>(T[,] matrix, S[] coefficient, CancellationToken cancellationToken = default)
    {
        CheckCoherenceForREF(matrix, coefficient);
        CheckForS(typeof(S));
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), coefficient.GetFractions(), solution: true, cancellationToken: cancellationToken);
        var result = new List<REF_Result<R1, R2>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<R1, R2>
            {
                Matrix = step.Matrix.GetTMatrix<R1>(),
                Coefficient = step.Coefficient.GetTMatrix<R2>(),
                Description = step.Description
            });
        }
        return [.. result];
    }

    /// <summary>
    /// Returns the RREF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="R">The type of the returned matrices</typeparam>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's RREF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="cancellationToken"></param>
    /// <returns>2d array of type R that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<R, string>[] RREFWithVariable<R, T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken cancellationToken = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var solution = Row_Echelon_Form.RREF(matrix.GetFractions(), coefficient, solution: true, cancellationToken: cancellationToken);
        var result = new List<REF_Result<R, string>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<R, string>
            {
                Matrix = step.Matrix.GetTMatrix<R>(),
                Coefficient = step.Coefficient.SpecialString2Decimal(),
                Description = step.Description
            });
        }
        return [.. result];
    }

    private partial class Row_Echelon_Form
    {
        public static IEnumerable<REF_Result<Fraction, T>> RREF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken cancellationToken = default) where T : ICoefficient
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
                REF_Result<Fraction, T>? current = solution ? new() : null;
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = FindPivot(matrix, currentRow);
                if (currentColumn == -1) break;
                ReOrderMatrix(matrix, coefficient, currentRow, currentColumn, ref current);
                if (matrix[currentRow, currentColumn].Quotient == 0) continue;
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

        /// <summary>
        /// Clears the pivot row by multiplying 1/{pivot}
        /// </summary>
        /// <typeparam name="T">The type of the coefficient It should implement the ICoefficient interface</typeparam>
        /// <param name="matrix">The matrix</param>
        /// <param name="coefficient">The coefficient</param>
        /// <param name="pivotRow">Index of pivot row</param>
        /// <param name="pivotColumn">Index of pivot column</param>
        /// <param name="solution">Solution</param>
        private static void ClearPivotRow<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int pivoColumn, ref REF_Result<Fraction, T>? solution) where T : ICoefficient
        {
            //Define the scalar 1/{pivot}
            Fraction scalar = new(matrix[pivotRow, pivoColumn].Denominator, matrix[pivotRow, pivoColumn].Numerator);

            //If the pivot is 1 then we don't have to do anything
            if (scalar.Quotient == 1) return;

            //Else loop through the row and multiply by the scalar
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[pivotRow, column] *= scalar;
            }
            coefficient[pivotRow] = (T)(coefficient[pivotRow] * scalar);

            //If the solution is not null then we assign to it this step
            if (solution is not null)
            {
                solution = new REF_Result<Fraction, T>
                {
                    Description = $"{scalar}R{pivotRow + 1} ----> R{pivotRow + 1}",
                    Coefficient = (T[])coefficient.Clone(),
                    Matrix = (Fraction[,])matrix.Clone(),
                };
            }
        }
    }
}