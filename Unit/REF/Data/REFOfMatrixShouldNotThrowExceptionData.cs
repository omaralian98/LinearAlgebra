using System.Collections;

namespace Unit.REF.Data;

public record REFOfMatrixShouldNotThrowExceptionData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
            new string[,] { { "10/0.1/2/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
        };
        yield return new object[] {
            new decimal[,] { { 1, 0, 3, 7, 2 }, { 0, 6, -3, -23, -4 }, { 0, 0, -1m / 2, 25m / 2, -2 }, { 0, 0, 0, -214, 29 }, { 0, 0, 0, 0, -727m / 321 } },
        };
        yield return new object[] {
            new decimal[,] { { 1, 1, 1, 1, 2 }, { -1, -1, -1, 1, 2 }, { 3, 3, 3, 4, 7 } },
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}