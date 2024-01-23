namespace LinearAlgebra;
public partial class Linear
{
    public static Result<T> REF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken token = default) where T : ICoefficient
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
            ClearPivotColumn(matrix, coefficient, currentRow, currentColumn, reduced: false, matrixSteps);
        }
        return new Result<T> { Matrix = matrix, MatrixSteps = matrixSteps?.ToArray() ?? [], Coefficient = coefficient };
    }
    private static void ReOrderMatrix<T>(Fraction[,] matrix, T[] coefficient, int row, List<MatrixStep<T>>? solution) where T : ICoefficient
    {
        var (result, x, y) = CheckPossibleSwap(row, row, matrix);
        if (result)
        {
            matrix = SwapMatrix(x, y, matrix);
            coefficient = SwapCoefficient(x, y, coefficient);
            solution?.Add(new MatrixStep<T>
            {
                Description = $"Swap between R{x + 1} and R{y + 1}",
                Coefficient = (T[])coefficient.Clone(),
                Matrix = (Fraction[,])matrix.Clone(),
            });
        }
    }

    private static void ClearPivotColumn<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int column, bool reduced, List<MatrixStep<T>>? solution) where T : ICoefficient
    {
        int targetedRow = reduced ? 0 : pivotRow;
        for (; targetedRow < matrix.GetLength(0); targetedRow++)
        {
            if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
            Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
            matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);
            coefficient[targetedRow] = (T)((coefficient[pivotRow] * scalar) + coefficient[targetedRow]);
            solution?.Add(new MatrixStep<T>
            {
                Description = $"{scalar}R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                Coefficient = coefficient.Clone() as T[],
                Matrix = (Fraction[,])matrix.Clone(),
            });
        }
    }

    private static void CheckCoherenceForREF<T, S>(T[,] matrix, S[] coefficient)
    {
        //If the matrix and the coefficient matrix has different number of rows throw an exception
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient?.GetLength(0)) throw new ArgumentException(errorMessage);
    }
    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>
    /// Returns the REF of the giving matrix, and it's coefficient as Fraction arraies
    /// <br></br>
    /// **Note**: Fraction is a struct that you can access like this:
    /// <br></br>
    /// LinearAlgebra.Linear.Fraction
    /// </returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (Fraction[,], SpecialString[]) REFAsSpecialString(Fraction[,] matrix)
    {
        //CheckCoherenceForREF(matrix);
        //var result = REF(matrix, solution: true);
        //var special = GetCoefficient(SpecialString.GetVariableMatrix(matrix.GetLength(0)), result.Steps);
        //return (matrix, special);
        return (matrix, []);
    }
    public static (Fraction[,], Fraction[]) REFAsFraction(Fraction[,] matrix, Fraction[] coefficient)
    {
        //CheckCoherenceForREF(matrix, coefficient);
        //var result = REF(matrix, solution: false);
        //var special = GetCoefficient(coefficient, result.Steps);
        //return (matrix, special);
        return (matrix, []);
    }

    public static string[] REFGetCoefficientAsStrings<T>(T[,] matrix)
    {
        var (result, coe) = REFAsSpecialString(matrix.GetFractions());
        return coe.SpecialString2String();
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
}