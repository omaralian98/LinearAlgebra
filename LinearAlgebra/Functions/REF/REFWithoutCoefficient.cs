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
    public static decimal[,] REF<T>(T[,] matrix)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions());
        solution.Matrix.Print();
        return solution.Matrix.Fraction2Decimal();
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
        var solution = Row_Echelon_Form.REF(matrix.GetFractions());
        return solution.Matrix;
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
        var solution = Row_Echelon_Form.REF(matrix.GetFractions());
        return solution.Matrix.Fraction2String();
    }
    public partial class Row_Echelon_Form
    {
        public static REF_Result REF(Fraction[,] matrix, bool solution = false, CancellationToken token = default)
        {
            int matrixRows = matrix.GetLength(0);
            int matrixColumns = matrix.GetLength(1);
            REF_Result? result = solution ? new() { Matrix = (Fraction[,])matrix.Clone() } : null;
            REF_Result? current = result;
            for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
            {
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = FindPivot(matrix, currentRow);
                if (currentColumn == -1) continue;
                ReOrderMatrix(matrix, currentRow, currentColumn, ref current);
                ClearPivotColumn(matrix, currentRow, currentColumn, reduced: false, ref current);
            }
            return result is not null ? result : new REF_Result { Matrix = matrix };
        }

        private static void ReOrderMatrix(Fraction[,] matrix, int x, int y, ref REF_Result? solution)
        {
            y = CheckPossibleSwap(x, y, matrix);
            if (y > 0)
            {
                matrix = SwapMatrix(x, y, matrix);

                if (solution is not null)
                {
                    solution.NextStep = new REF_Result
                    {
                        Description = $"Swap between R{x + 1} and R{y + 1}",
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                    solution = solution.NextStep;
                }
            }
        }

        public static void ClearPivotColumn(Fraction[,] matrix, int pivotRow, int column, bool reduced, ref REF_Result? solution)
        {
            int targetedRow = reduced ? 0 : pivotRow;
            for (; targetedRow < matrix.GetLength(0); targetedRow++)
            {
                if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
                Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
                matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);

                if (solution is not null)
                {
                    solution.NextStep = new REF_Result
                    {
                        Description = $"{scalar}R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                    solution = solution.NextStep;
                }
            }
        }
    }
}