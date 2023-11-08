namespace LinearAlgebra;
public partial class Linear
{
    private static void CheckCoherence<T, S>(T[,] matrix, S[] coefficient)
    {
        //If the matrix and the coefficient matrix has different number of rows throw an exception
        string errorMessage = $"The matrix of coefficients should be consistent with the original matrix.\nThe matrix has {matrix.GetLength(0)} rows and the coefficient has {coefficient.Length} rows";
        if (matrix.GetLength(0) != coefficient?.GetLength(0)) throw new ArgumentException(errorMessage);
    }
    /// <summary>
    /// Aka: Row Echelon Form.
    /// </summary>
    /// <param name="matrix">The matrix you want to get it's REF</param>
    /// <param name="coefficient">The coefficient of the matrix</param>
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
    public static (Fraction[,], SpecialString[]) REFAsSpecialString(Fraction[,] matrix, Fraction[] coefficient)
    {
        CheckCoherence(matrix, coefficient);
        (matrix, var solution) = GetREF(matrix, reduced: false);
        var special = GetCoefficient(coefficient.GetFraction(), solution);
        return (matrix, special);
    }
    public static (Fraction[,], Fraction[]) REFAsFraction(Fraction[,] matrix, Fraction[] coefficient)
    {
        CheckCoherence(matrix, coefficient);
        (matrix, var solution) = GetREF(matrix, reduced: false);
        var special = GetCoefficient(coefficient, solution);
        return (matrix, special);
    }

    public static string[] REFGetCoefficientAsStrings<T>(T[,] matrix, T[] coefficient)
    {
        var (result, coe) = REFAsSpecialString(matrix.GetFractions(), coefficient.GetFractions());
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
        CheckCoherence(matrix, coefficient);
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
        CheckCoherence(matrix, coefficient);
        var (result, coe) = REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
        return (result.Fraction2String(), coe.Fraction2String());
    }
}