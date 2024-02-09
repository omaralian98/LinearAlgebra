namespace LinearAlgebra;

public partial class Linear
{
    private static void CheckCoherenceForREF<T, S>(T[,] matrix, S[] coefficient)
    {
        //If the matrix and the coefficient matrix has different number of rows throw an exception
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient?.GetLength(0)) throw new ArgumentException(errorMessage);
    }

    /// <summary>
    /// Returns the REF of the given matrix and the coefficient.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>
    /// Returns the REF of the giving matrix, and it's coefficient as Fraction arraies
    /// <br></br>
    /// **Note**: Fraction is a struct that you can access like this:
    /// <br></br>
    /// LinearAlgebra.Linear.Fraction
    /// </returns>
    /// <remarks>Aka: Row Echelon Form.</remarks>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (Fraction[,], SpecialString[]) REFAsSpecialString<T>(T[,] matrix, SpecialString[]? coefficient = null)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        REF_Result<SpecialString> result = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient).First();
        return (result.Matrix, result.Coefficient);
    }
    
    /// <summary>
    /// Returns the REF of the given matrix and the coefficient.
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>2d array of type Fraction that represent the matrix and a Fraction array that represent the coefficient</returns>
    /// <exception cref="ArgumentException"></exception>
    public static (Fraction[,], Fraction[]) REFAsFraction<T, S>(T[,] matrix, S[] coefficient)
    {
        if (typeof(S) == typeof(SpecialString))
        {
            throw new ArgumentException($"The coefficient can't be {typeof(S)}", nameof(coefficient));
        }
        CheckCoherenceForREF(matrix, coefficient);
        var result = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient.GetFractions()).First();
        return (result.Matrix, result.Coefficient);
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>Returns the REF of the giving matrix, and it's coefficient as decimal arraies</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (decimal[,], decimal[]) REF<T>(T[,] matrix, T[] coefficient)
    {
        CheckCoherenceForREF(matrix, coefficient);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2Decimal(), coe.Fraction2Decimal());
    }
   
    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>Returns the REF of the giving matrix, and it's coefficient as string arraies</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (string[,], string[]) REFAsString<T>(T[,] matrix, T[] coefficient)
    {
        CheckCoherenceForREF(matrix, coefficient);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2String(), coe.Fraction2String());
    }

    public partial class Row_Echelon_Form
    {
        public static IEnumerable<REF_Result<T>> REF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken token = default) where T : ICoefficient
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
                var cur = ClearPivotColumn(matrix, coefficient, currentRow, currentColumn, reduced: false, solution);
                foreach (var step in cur)
                {
                    yield return step;
                }
            }
            if (!solution)
            {
                yield return new() { Matrix = matrix, Coefficient = coefficient };
            }
        }
        private static void ReOrderMatrix<T>(Fraction[,] matrix, T[] coefficient, int x, int y, ref REF_Result<T>? solution) where T : ICoefficient
        {
            y = CheckPossibleSwap(x, y, matrix);
            if (y > 0)
            {
                matrix = SwapMatrix(x, y, matrix);
                coefficient = SwapCoefficient(x, y, coefficient);

                if (solution is not null)
                {
                    solution = new REF_Result<T>
                    {
                        Description = $"Swap between R{x + 1} and R{y + 1}",
                        Coefficient = (T[])coefficient.Clone(),
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                }
            }
        }

        private static IEnumerable<REF_Result<T>> ClearPivotColumn<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int column, bool reduced, bool solution= false) where T : ICoefficient
        {
            int targetedRow = reduced ? 0 : pivotRow;
            for (; targetedRow < matrix.GetLength(0); targetedRow++)
            {
                if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
                Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
                matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);
                coefficient[targetedRow] = (T)((coefficient[pivotRow] * scalar) + coefficient[targetedRow]);

                if (solution)
                {
                    yield return new REF_Result<T>
                    {
                        Description = $"{scalar}R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                        Coefficient = (T[])coefficient.Clone(),
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                }
            }
        }
    }

}