using System.Collections;
using Unit.REF.Parameters;

namespace Unit.REF.Data;

public record REFOfMatrixWithCoefficientShouldThrowExceptionData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
            new REFOfMatrixWithCoefficientShouldThrowExceptionParameter<string, string>(
                    matrix: new string[,] { { "10", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                    coefficient: new string[] { "0", "39/5" },
                    exceptionType: typeof(ArgumentException)
            )
        };

        yield return new object[] {
            new REFOfMatrixWithCoefficientShouldThrowExceptionParameter<string, string>(
                    matrix: new string[,] { { "10", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                    coefficient: new string[] { "0", "39/5", "2", "4" },
                    exceptionType: typeof(ArgumentException)
            )
        };

        yield return new object[] {
            new REFOfMatrixWithCoefficientShouldThrowExceptionParameter<string, string>(
                    matrix: new string[,] { { "10", "1", "2" }, { "1/0", "3", "7" }, { "2", "7", "6" } },
                    coefficient: new string[] { "0", "39/5", "2"},
                    exceptionType: typeof(DivideByZeroException)
            )
        };

        yield return new object[] {
            new REFOfMatrixWithCoefficientShouldThrowExceptionParameter<string, string>(
                    matrix: new string[,] { { "10/0", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                    coefficient: new string[] { "0", "39/0", "1" },
                    exceptionType: typeof(DivideByZeroException)
            )
        };
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}