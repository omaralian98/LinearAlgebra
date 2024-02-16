using LinearAlgebra.Classes;

namespace LinearAlgebra;

public partial class Linear
{
    private static void CheckCoherenceForREF<T, S>(T[,] matrix, S[] coefficient, CancellationToken token = default)
    {
        //If the matrix and the coefficient matrix has different number of rows throw an exception
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient?.GetLength(0)) throw new ArgumentException(errorMessage);
    }




    /// <summary>
    /// Returns the REF of the given matrix and coefficient.
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>2d array of type Fraction that represent the matrix and a Fraction array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (Fraction[,], Fraction[]) REFAsFraction<T, S>(T[,] matrix, S[] coefficient, CancellationToken token = default)
    {
        CheckCoherenceForREF(matrix, coefficient);
        var result = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient.GetFractions(), token: token).First();
        return (result.Matrix, result.Coefficient);
    }

    /// <summary>
    /// Returns the REF of the given matrix and coefficient.
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>2d array of type decimal that represent the matrix and a decimal array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (decimal[,], decimal[]) REFAsDecimal<T, S>(T[,] matrix, S[] coefficient, CancellationToken token = default)
    {
        CheckCoherenceForREF(matrix, coefficient);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions(), token: token);
        return (result.Fraction2Decimal(), coe.Fraction2Decimal());
    }

    /// <summary>
    /// Returns the REF of the given matrix and coefficient.
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <returns>2d array of type string that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (string[,], string[]) REFAsString<T, S>(T[,] matrix, S[] coefficient, CancellationToken token = default)
    {
        CheckCoherenceForREF(matrix, coefficient);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions(), token:token);
        return (result.Fraction2String(), coe.Fraction2String());
    }




    /// <summary>
    /// Returns the REF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type string that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (string[,], string[]) REFAsStringVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken token = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var result = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient, token: token).First();
        return (result.Matrix.Fraction2String(), result.Coefficient.SpecialString2String());
    }

    /// <summary>
    /// Returns the REF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type decimal that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (decimal[,], string[]) REFAsDecimalVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken token = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var result = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient, token: token).First();
        return (result.Matrix.Fraction2Decimal(), result.Coefficient.SpecialString2Decimal());
    }

    /// <summary>
    /// Returns the REF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type Fraction that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static (Fraction[,], string[]) REFAsFractionVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken token = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var result = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient, token: token).First();
        return (result.Matrix, result.Coefficient.SpecialString2String());
    }




    /// <summary>
    /// Returns The REF of the given matrix and coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type Fraction that represent the matrix and a Fraction array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<Fraction, Fraction>[] REFWithFraction<T, S>(T[,] matrix, S[] coefficient, CancellationToken token = default)
    {
        if (typeof(S) == typeof(SpecialString))
        {
            throw new ArgumentException($"The coefficient can't be {typeof(S)}", nameof(coefficient));
        }
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient.GetFractions(), solution: true, token: token);
        return [.. solution];
    }

    /// <summary>
    /// Returns The REF of the given matrix and coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type Decimal that represent the matrix and a Decimal array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<decimal, decimal>[] REFWithDecimal<T, S>(T[,] matrix, S[] coefficient, CancellationToken token = default)
    {
        if (typeof(S) == typeof(SpecialString))
        {
            throw new ArgumentException($"The coefficient can't be {typeof(S)}", nameof(coefficient));
        }
        else
        {
            var solution = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient.GetFractions(), solution: true, token: token);
            var result = new List<REF_Result<decimal, decimal>>();
            foreach (var step in solution)
            {
                result.Add(new REF_Result<decimal, decimal>
                {
                    Matrix = step.Matrix.Fraction2Decimal(),
                    Coefficient = step.Coefficient.Fraction2Decimal(),
                    Description = step.Description
                });
            }
            return [.. result];
        }
    }

    /// <summary>
    /// Returns The REF of the given matrix and coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <typeparam name="S">The type of the coefficient</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type string that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<string, string>[] REFWithString<T, S>(T[,] matrix, S[] coefficient, CancellationToken token = default)
    {
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient.GetFractions(), solution: true, token: token);
        var result = new List<REF_Result<string, string>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<string, string>
            {
                Matrix = step.Matrix.Fraction2String(),
                Coefficient = step.Coefficient.Fraction2String(),
                Description = step.Description
            });
        }
        return [.. result];
    }




    /// <summary>
    /// Returns the REF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type string that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<string, string>[] REFWithStringVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken token = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient, solution: true, token: token);
        var result = new List<REF_Result<string, string>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<string, string>
            {
                Matrix = step.Matrix.Fraction2String(),
                Coefficient = step.Coefficient.SpecialString2String(),
                Description = step.Description
            });
        }
        return [.. result];
    }

    /// <summary>
    /// Returns the REF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type decimal that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<decimal, string>[] REFWithDecimalVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken token = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient, solution: true, token: token);
        var result = new List<REF_Result<decimal, string>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<decimal, string>
            {
                Matrix = step.Matrix.Fraction2Decimal(),
                Coefficient = step.Coefficient.SpecialString2Decimal(),
                Description = step.Description
            });
        }
        return [.. result];
    }

    /// <summary>
    /// Returns the REF of the given matrix and variable coefficient
    /// </summary>
    /// <typeparam name="T">The type of the matrix</typeparam>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
    /// <param name="token"></param>
    /// <returns>2d array of type Fraction that represent the matrix and a string array that represent the coefficient</returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static REF_Result<Fraction, string>[] REFWithFractionVariable<T>(T[,] matrix, SpecialString[]? coefficient = null, CancellationToken token = default)
    {
        coefficient ??= SpecialString.GetVariableMatrix(matrix.GetLength(0));
        CheckCoherenceForREF(matrix, coefficient);
        var solution = Row_Echelon_Form.REF(matrix.GetFractions(), coefficient, solution: true, token: token);
        var result = new List<REF_Result<Fraction, string>>();
        foreach (var step in solution)
        {
            result.Add(new REF_Result<Fraction, string>
            {
                Matrix = step.Matrix,
                Coefficient = step.Coefficient.SpecialString2String(),
                Description = step.Description
            });
        }
        return [.. result];
    }




    private partial class Row_Echelon_Form
    {

        public static IEnumerable<REF_Result<Fraction, T>> REF<T>(Fraction[,] matrix, T[] coefficient, bool solution = false, CancellationToken token = default) where T : ICoefficient
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
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = FindPivot(matrix, currentRow);
                if (currentColumn == -1) break;
                ReOrderMatrix(matrix, coefficient, currentRow, currentColumn, ref current);
                if (matrix[currentRow, currentColumn].Quotient == 0) continue;
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

        /// <summary>
        /// Reorder a matrix and coefficient accordingly
        /// </summary>
        /// <typeparam name="T">The type of the coefficient It should implement the ICoefficient interface</typeparam>
        /// <param name="matrix">The matrix</param>
        /// <param name="coefficient">The coefficient</param>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        /// <param name="solution">Solution</param>
        public static void ReOrderMatrix<T>(Fraction[,] matrix, T[] coefficient, int x, int y, ref REF_Result<Fraction, T>? solution) where T : ICoefficient
        {
            //Gets the Index of a possible swap
            y = CheckPossibleSwap(x, y, matrix);
            //If there is a swap
            if (y > 0)
            {
                //Swap the matrix and the coefficient
                matrix = SwapMatrix(x, y, matrix);
                coefficient = SwapCoefficient(x, y, coefficient);

                //If the solution is not null then we assign to it this step
                if (solution is not null)
                {
                    solution = new REF_Result<Fraction, T>
                    {
                        Description = $"Swap between R{x + 1} and R{y + 1}",
                        Coefficient = (T[])coefficient.Clone(),
                        Matrix = (Fraction[,])matrix.Clone(),
                    };
                }
            }
        }

        /// <summary>
        /// Clears a column making all elements equal to 0 except for the pivot
        /// </summary>
        /// <typeparam name="T">The type of the coefficient It should implement the ICoefficient interface</typeparam>
        /// <param name="matrix">The matrix</param>
        /// <param name="coefficient">The coefficient</param>
        /// <param name="pivotRow">Index of pivot row</param>
        /// <param name="column">Index of the targeted column</param>
        /// <param name="reduced">Whether to apply REF Or RREF</param>
        /// <param name="solution">Whether to return the steps of this operation or not</param>
        /// <returns>IEnumerable of REF_Result of type Fraction, T<br></br><br></br>Note: If the solution is not required nothing will be returned</returns>
        public static IEnumerable<REF_Result<Fraction, T>> ClearPivotColumn<T>(Fraction[,] matrix, T[] coefficient, int pivotRow, int column, bool reduced, bool solution= false) where T : ICoefficient
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

                //Apply this scalar to the entire row and to the coefficient
                matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);
                coefficient[targetedRow] = (T)((coefficient[pivotRow] * scalar) + coefficient[targetedRow]);

                if (solution)
                {//If we want the solution step by step return this step
                    yield return new REF_Result<Fraction, T>
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