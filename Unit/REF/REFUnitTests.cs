using LinearAlgebra.Classes.Enums;
using Unit.REF.Data;
using Unit.REF.Parameters;

namespace Unit.REF;

public class REFUnitTests
{
    [Theory]
    [ClassData(typeof(REFOfMatrixWithCoefficientData))]
    public void REFOfMatrixWithCoefficientTheory<TMatrix, TCoefficient, RMatrix, RCoefficient>(REFOfMatrixWithCoefficientParameter<TMatrix, TCoefficient, RMatrix, RCoefficient> test)
    {
        var value = Linear.REF<RMatrix, RCoefficient, TMatrix, TCoefficient>(test.Matrix, test.Coefficient);
        Assert.Equal(test.ExpectedMatrix, value.Matrix);
        Assert.Equal(test.ExpectedCoefficient, value.Coefficient);
    }

    [Theory]
    [ClassData(typeof(REFOfMatrixData))]
    public void REFOfMatrixTheory<TMatrix, RMatrix>(REFOfMatrixParameter<TMatrix, RMatrix> test)
    {
        var realValue = Linear.REF<RMatrix, TMatrix>(test.Matrix);
        Assert.Equal(test.ExpectedMatrix, realValue);
    }


    [Theory]
    [ClassData(typeof(REFOfMatrixShouldThrowExceptionData))]
    public void REFOfMatrixShouldThrowExceptionTheory<TMatrix>(REFOfMatrixShouldThrowExceptionParameter<TMatrix> test)
    {
        Assert.Throws(test.Exception, () => Linear.REF(test.Matrix));
    }

    [Theory]
    [ClassData(typeof(REFOfMatrixWithCoefficientShouldThrowExceptionData))]
    public void REFOfMatrixWithCoefficientShouldThrowExceptionTheory<TMatrix, TCoefficient>(REFOfMatrixWithCoefficientShouldThrowExceptionParameter<TMatrix, TCoefficient> test)
    {
        Assert.Throws(test.Exception, () => Linear.REF(test.Matrix, test.Coefficient));
    }


    [Theory]
    [ClassData(typeof(REFOfMatrixShouldNotThrowExceptionData))]
    public void REFOfMatrixShouldNotThrowExceptionTheory<T>(T[,] matrix)
    {
        Linear.REF(matrix);
    }

    public static TheoryData<Fraction[,]> REFOfRandomMatrixShouldNotThrowExceptionData()
    {
        TheoryData<Fraction[,]> data = [];
        for (int i = 0; i < 20; i++)
        {
            data.Add(Fraction.GenerateRandomMatrix(8, 8, RandomFractionGenerationType.IntegersOnly));
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(REFOfRandomMatrixShouldNotThrowExceptionData))]
    public void REFOfRandomMatrixShouldNotThrowExceptionTheory<T>(T[,] matrix)
    {
        try
        {
            Linear.REF(matrix);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Expected no exception, but got: {ex.Message}");
        }
    }
}