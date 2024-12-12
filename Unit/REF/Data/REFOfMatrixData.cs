using System.Collections;
using Unit.REF.Parameters;

namespace Unit.REF.Data;

public record REFOfMatrixData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
            new REFOfMatrixParameter<decimal, decimal>(
                new decimal[,] { { 5, 1, 2 }, { 1, 3, 7 }, { 2, 7, 6 } },
                new decimal[,] { { 5, 1, 2 }, { 0, 14m / 5, 33m / 5 }, { 0, 0, -145m / 14 } }
            )
        };

        yield return new object[] {
            new REFOfMatrixParameter<double, decimal>(
                new double[,] { { 5, 6, 6 }, { 1, 9, 5 }, { 4, 5, 9 } },
                new decimal[,] { { 5, 6, 6 }, { 0, 39m / 5, 19m / 5 }, { 0, 0, 160m / 39 } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<string, decimal>(
                new string[,] { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                new decimal[,] { { 5, 1, 2 }, { 0, 14m / 5, 33m / 5 }, { 0, 0, -145m / 14 } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<string, string>(
                new string[,] { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                new string[,] { { "5", "1", "2" }, { "0", "14/5", "33/5" }, { "0", "0", "-145/14" } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<decimal, decimal>(
                new decimal[,] { { 1, 3 }, { 9, 3 }, { 1, 9 } },
                new decimal[,] { { 1, 3 }, { 0, -24 }, { 0, 0 } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<decimal, string>(
                new decimal[,] { { 3, 5 }, { 8, 5 }, { 9, 8 }, { 1, 6 } },
                new string[,] { { "3", "5" }, { "0", "-25/3" }, { "0", "0" }, { "0", "0" } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<decimal, string>(
                new decimal[,] { { 2, 2, 1, 7 }, { 7, 0, 9, 7 } },
                new string[,] { { "2", "2", "1", "7" }, { "0", "-7", "11/2", "-35/2" } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<decimal, decimal>(
                new decimal[,] { { 1, 4, 1, 5 }, { 1, 4, 0, 5 }, { 2, 8, 7, 6 } },
                new decimal[,] { { 1, 4, 1, 5 }, { 0, 0, -1, 0 }, { 0, 0, 0, -4 } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<decimal, decimal>(
                new decimal[,] { { 1, 0, 3, 7, 2 }, { 4, 6, 9, 5, 4 }, { 4, 9, 7, 6, 0 }, { 6, 6, 7, 5, 5 }, { 2, 4, 9, 7, 1 } },
                new decimal[,] { { 1, 0, 3, 7, 2 }, { 0, 6, -3, -23, -4 }, { 0, 0, -1m / 2, 25m / 2, -2 }, { 0, 0, 0, -214, 29 }, { 0, 0, 0, 0, -727m / 321 } }
            )
        };
        yield return new object[] {
            new REFOfMatrixParameter<decimal, decimal>(
                new decimal[,] { { 1, 1, 1, 1, 2 }, { -1, -1, -1, 1, 2 }, { 3, 3, 3, 4, 7 } },
                new decimal[,] { { 1, 1, 1, 1, 2 }, { 0, 0, 0, 2, 4 }, { 0, 0, 0, 0, -1 } }
            )
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}