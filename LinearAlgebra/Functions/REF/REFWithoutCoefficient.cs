namespace LinearAlgebra;

public partial class Linear
{

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>Returns the REF of the giving matrix as decimal array</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static decimal[,] REF<T>(T[,] matrix, int decimals = -1, CancellationToken token = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), token: token).First();
        return solution.Matrix.Fraction2Decimal(decimals);
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
    public static Fraction[,] REFAsFraction<T>(T[,] matrix, CancellationToken token = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), token: token);
        var result = solution.First();
        return result.Matrix;
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>Returns the REF of the giving matrix as string array</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static string[,] REFAsString<T>(T[,] matrix, CancellationToken token = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), token: token);
        var result = solution.First();
        return result.Matrix.Fraction2String();
    }

    private partial class Row_Echelon_Form
    {
        public static IEnumerable<REF_Result> REF(Fraction[,] matrix, bool solution = false, CancellationToken token = default)
        {
            int matrixRows = matrix.GetLength(0);
            int matrixColumns = matrix.GetLength(1);
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
                var cur = ClearPivotColumn(matrix, currentRow, currentColumn, reduced: false, solution);
                foreach (var step in cur)
                {
                    yield return step;
                }
            }
            if (solution == false)
            {
                yield return new REF_Result { Matrix = matrix };
            }
        }

        private static void ReOrderMatrix(Fraction[,] matrix, int x, int y, ref REF_Result? solution)
        {
            y = CheckPossibleSwap(x, y, matrix);
            if (y > 0)
            {
                matrix = SwapMatrix(x, y, matrix);

                if (solution is not null)
                {
                    solution = new REF_Result
                    {
                        Description = $"Swap between R{x + 1} and R{y + 1}",
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                }
            }
        }

        public static IEnumerable<REF_Result> ClearPivotColumn(Fraction[,] matrix, int pivotRow, int column, bool reduced, bool solution = false)
        {
            int targetedRow = reduced ? 0 : pivotRow;
            for (; targetedRow < matrix.GetLength(0); targetedRow++)
            {
                if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
                Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
                matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);
                if (solution)
                {
                    yield return new REF_Result
                    {
                        Description = $"{scalar} R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                }
            }
        }
    }
}