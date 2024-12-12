using System.Collections;
using Unit.REF.Parameters;

namespace Unit.REF.Data;

public record REFOfMatrixShouldThrowExceptionData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
            new REFOfMatrixShouldThrowExceptionParameter<string>(
                    new string[,] { { "10/0", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                    typeof(DivideByZeroException)
            )
        };
        yield return new object[] {
            new REFOfMatrixShouldThrowExceptionParameter<string>(
                    new string[,] { { "10//", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                    typeof(FormatException)
            )
        };
        yield return new object[] {
            new REFOfMatrixShouldThrowExceptionParameter<string>(
                new string[,] { { "10/0.1/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                typeof(FormatException)
            )
        };
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}