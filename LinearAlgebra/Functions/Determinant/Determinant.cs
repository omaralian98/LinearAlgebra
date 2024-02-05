﻿namespace LinearAlgebra;

public partial class Linear
{
    /// <summary>
    /// Gets the Determinant of a given matrix as a Fraction.
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <returns>A fraction that's represent the value of the determinant</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Fraction DeterminantAsFraction<T>(T[,] matrix)
    {
        CheckCoherenceForDeterminant(matrix);
        var result = DeterminantClass.Determinant(matrix.GetFractions());
        return result.Value;
    }

    /// <summary>
    /// Gets the Determinant of a given matrix as a decimal.
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <returns>The value of the determinant as a decimal</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static decimal Determinant<T>(T[,] matrix)
    {
        CheckCoherenceForDeterminant(matrix);
        var result = DeterminantClass.Determinant(matrix.GetFractions());
        return (decimal)result.Value;
    }

    /// <summary>
    /// Gets the Determinant of a given matrix as a string.
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <returns>The value of the determinant as a string</returns>
    /// <remarks>If the value of the determinant is decimal the string will be in this format: {Numerator}/{Denominator}.</remarks>
    /// <exception cref="InvalidOperationException"></exception>
    public static string DeterminantAsString<T>(T[,] matrix)
    {
        CheckCoherenceForDeterminant(matrix);
        var result = DeterminantClass.Determinant(matrix.GetFractions());
        return (string)result.Value;
    }

    public static Determinant_Result[] DeterminantWithResult<T>(T[,] matrix)
    {
        CheckCoherenceForDeterminant(matrix);
        var result = DeterminantClass.Determinant(matrix.GetFractions(), true);
        return [.. result.GetAllChildren()];
    }

    public static (Fraction, REF_Result[]) DeterminantWithResultUsingREF<T>(T[,] matrix)
    {
        CheckCoherenceForDeterminant(matrix);
        var result = DeterminantClass.DeterminantUsingREF(matrix.GetFractions(), true);
        return (result.Item1, [.. result.Item2.GetAllChildren()]);
    }

    /// <summary>
    /// Checks if the given matrix can be passed to the determinant function.
    /// </summary>
    /// <typeparam name="T">Type of the matrix</typeparam>
    /// <param name="matrix">The matrix</param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void CheckCoherenceForDeterminant<T>(T[,] matrix)
    {
        string errorMessage = $"Non-square matrices do not have determinants.";
        if (matrix.GetLength(0) != matrix.GetLength(1)) throw new InvalidOperationException(errorMessage);
    }


    internal partial class DeterminantClass
    {
        public static Determinant_Result Determinant(Fraction[,] matrix, bool solution = false)
        {
            Fraction answer = new(0);
            int size = matrix.GetLength(0);
            if (size == 1)
            {
                answer += matrix[0, 0];
                return new Determinant_Result { Matrix = matrix, Value = answer };
            }
            else if (size >= 2)
            {
                Determinant_Result[] matrixSteps = new Determinant_Result[size];
                for (int i = 0; i < size; i++)
                {
                    var errasedMatrix = Erase(0, i, matrix);
                    Fraction scalar = i % 2 == 0 ? matrix[0, i] : -matrix[0, i];
                    var det = Determinant(errasedMatrix);
                    answer += scalar * det.Value;
                    matrixSteps[i] = det with { Scalar = scalar };
                }
                return new Determinant_Result
                {
                    Value = answer,
                    MatrixSteps = solution ? matrixSteps : [],
                    Matrix = matrix
                };
            }

            throw new NotImplementedException();
        }

        private static T[,] Erase<T>(int x, int y, T[,] matrix)
        {
            int size = matrix.GetLength(0) - 1; //Get the new size.
            int row = 0;
            int column = 0;
            T[,] erasedMatrix = new T[size, size]; //Create the new array.
            for (int i = 0; i < matrix.GetLength(0); i++) 
            { //Loop through the original array.
                for (int j = 0; j < matrix.GetLength(1); j++)
                { //If we are the at the same row that we want to erase goto the next row.
                    if (i == x) goto skip; 
                    else if (j == y) continue; //If we are at the same column that we want to erase then go to the next column
                    else 
                    {//Otherwise copy the original element to the new one.
                        erasedMatrix[row, column] = matrix[i, j]; //Assign using row and column.
                        Reset(ref row, ref column, size); //Then reset them.
                    }
                }
            skip:;
            }
            return erasedMatrix;
        }

        private static void Reset(ref int row, ref int column, int size)
        {
            //If we didn't reach the end of the row increase column's index.
            if (column + 1 != size) column++;
            else 
            {//If we did reach the end of the row.
                row++; //Increase the index of the row.
                column = 0; //Reset the column index.
            }
        }
    }
}
