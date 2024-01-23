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
        Fraction[,] newMatrix = matrix.GetFractions();
        var result = REF(newMatrix, solution: false);
        return result.Matrix.Fraction2Decimal();
    }

    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <returns>
    /// Returns the REF of the giving matrix as Fraction array
    /// <br></br>
    /// **Note**: Fraction is a struct that you can access like this:
    /// <br></br>
    /// LinearAlgebra.Linear.Fraction
    /// </returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DivideByZeroException"></exception>
    public static Fraction[,] REFAsFraction<T>(T[,] matrix)
    {
        var result = REF(matrix.GetFractions(), solution: false);
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
    public static string[,] REFAsString<T>(T[,] matrix)
    {
        var result = REF(matrix.GetFractions(), solution: false);
        return result.Matrix.Fraction2String();
    }
}