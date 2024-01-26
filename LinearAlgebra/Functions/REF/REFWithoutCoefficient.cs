using static MathNet.Symbolics.Linq;

namespace LinearAlgebra;
public partial class Linear
{
    public static REFResult REF(Fraction[,] matrix, bool solution = false, CancellationToken token = default)
    {
        int matrixRows = matrix.GetLength(0); //Gets the number of rows
        int matrixColumns = matrix.GetLength(1); //Gets the number of columns
        REFResult? result = solution ? new() { Matrix = (Fraction[,])matrix.Clone() } : null;
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            //ReOrderMatrix(matrix, currentRow, result);
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            ClearPivotColumn(matrix, currentRow, currentColumn, reduced: false, result?.GetAllChildren().Last());
        }
        return result is not null ? result : new REFResult { Matrix = matrix };
    }

    private static void ReOrderMatrix(Fraction[,] matrix, int row, REFResult? solution = null)
    {
        var (result, x, y) = CheckPossibleSwap(row, row, matrix);
        if (result)
        {
            matrix = SwapMatrix(x, y, matrix);
            if (solution is not null)
            {
                solution.NextStep = new REFResult
                {
                    Description = $"Swap between R{x + 1} and R{y + 1}",
                    Matrix = (Fraction[,])matrix.Clone(),
                };
                solution = solution?.NextStep;
            }
        }
    }

    public static void ClearPivotColumn(Fraction[,] matrix, int pivotRow, int column, bool reduced, REFResult? solution = null)
    {
        int targetedRow = reduced ? 0 : pivotRow;
        REFResult? result = solution;
        for (; targetedRow < matrix.GetLength(0); targetedRow++)
        {
            //Console.WriteLine($"Matrix:\n{result}");
            //Console.WriteLine($"Nextmatrix: \n{result?.NextStep}");
            if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
            Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
            matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);
            if (result is not null)
            {
                result.NextStep = new REFResult
                {
                    Description = $"{scalar}R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                    Matrix = (Fraction[,])matrix.Clone(),
                };
            }
            result = result?.NextStep;
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
        //var result = REF(newMatrix, solution: false);
        //return result.Matrix.Fraction2Decimal();
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
        //var result = REF(matrix.GetFractions(), solution: false);
        //return result.Matrix;
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
        //var result = REF(matrix.GetFractions(), solution: false);
        //return result.Matrix.Fraction2String();
        return null;
    }
}