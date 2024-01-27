namespace LinearAlgebra;
public partial class Linear
{
    public static REFResult REF(Fraction[,] matrix, CancellationToken token = default)
    {
        int matrixRows = matrix.GetLength(0);
        int matrixColumns = matrix.GetLength(1);
        REFResult result = new() { Matrix = (Fraction[,])matrix.Clone() };
        REFResult current = result;
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            ReOrderMatrix(matrix, currentRow, ref current);
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            ClearPivotColumn(matrix, currentRow, currentColumn, reduced: false, ref current);
        }
        return result;
    }

    private static void ReOrderMatrix(Fraction[,] matrix, int x, ref REFResult solution)
    {
        var y = CheckPossibleSwap(x, x, matrix);
        if (y > 0)
        {
            matrix = SwapMatrix(x, y, matrix);

            solution.NextStep = new REFResult
            {
                Description = $"Swap between R{x + 1} and R{y + 1}",
                Matrix = (Fraction[,])matrix.Clone(),
            };
            solution = solution.NextStep;
        }
    }

    public static void ClearPivotColumn(Fraction[,] matrix, int pivotRow, int column, bool reduced, ref REFResult solution)
    {
        int targetedRow = reduced ? 0 : pivotRow;
        for (; targetedRow < matrix.GetLength(0); targetedRow++)
        {
            if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
            Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
            matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);

            solution.NextStep = new REFResult
            {
                Description = $"{scalar}R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                Matrix = (Fraction[,])matrix.Clone(),
            };
            solution = solution.NextStep;
        }
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>Returns the REF of the giving matrix as decimal array</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static decimal[,] REF<T>(T[,] matrix)
    {
        //Value[,] newMatrix = matrix.GetFractions();
        //var solution = REF(newMatrix, solution: false);
        //return solution.Matrix.Fraction2Decimal();
        return null;
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>
    /// Returns the REF of the giving matrix as Value array
    /// <br></br>
    /// **Note**: Value is a struct that you can access like this:
    /// <br></br>
    /// LinearAlgebra.Linear.Value
    /// </returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static Fraction[,] REFAsFraction<T>(T[,] matrix)
    {
        //var solution = REF(matrix.GetFractions(), solution: false);
        //return solution.Matrix;
        return null;
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>Returns the REF of the giving matrix as string array</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static string[,] REFAsString<T>(T[,] matrix)
    {
        //var solution = REF(matrix.GetFractions(), solution: false);
        //return solution.Matrix.Fraction2String();
        return null;
    }
}