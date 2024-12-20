﻿using System.Collections;
using Unit.REF.Parameters;

namespace Unit.REF.Data;

public record REFOfMatrixWithCoefficientData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, int, decimal, int>(
                new decimal[,] { { 5, 1, 2 }, { 1, 3, 7 }, { 2, 7, 6 } },
                new int[] { 0, 0, 0 },
                new decimal[,] { { 5, 1, 2 }, { 0, 14m / 5, 33m / 5 }, { 0, 0, -145m / 14 } },
                new int[] { 0, 0, 0 }
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<double, int, decimal, Fraction>(
                new double[,] { { 5, 6, 6 }, { 1, 9, 5 }, { 4, 5, 9 } },
                new int[] { 1, 1, 1 },
                new decimal[,] { { 5, 6, 6 }, { 0, 39m / 5, 19m / 5 }, { 0, 0, 160m / 39 } },
                new Fraction[] { 1, "4/5", "7/39"}
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<string, int, decimal, Fraction>(
                new string[,] { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                new int[] { 1, 1, 1 },
                new decimal[,] { { 5, 1, 2 }, { 0, 14m / 5, 33m / 5 }, { 0, 0, -145m / 14 } },
                new Fraction[] { 1, "4/5", "-9/7" }
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<string, int, string, Fraction>(
                new string[,] { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } },
                new int[] { 1, 1, 1 },
                new string[,] { { "5", "1", "2" }, { "0", "14/5", "33/5" }, { "0", "0", "-145/14" } },
                new Fraction[] { 1, "4/5", "-9/7"}
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, int, decimal, Fraction>(
                 new decimal[,] { { 1, 3 }, { 9, 3 }, { 1, 9 } },
                 new int[] { 1, 1, 1 },
                 new decimal[,] { { 1, 3 }, { 0, -24 }, { 0, 0 } },
                 new Fraction[] { 1, -8, -2 }
             )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, int, string, Fraction>(
                new decimal[,] { { 3, 5 }, { 8, 5 }, { 9, 8 }, { 1, 6 } },
                new int[] { 1, 1, 1, 1 },
                new string[,] { { "3", "5" }, { "0", "-25/3" }, { "0", "0" }, { "0", "0" } },
                new Fraction[] { 1, "-5/3", "-3/5", "-1/5" }
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, int, string, Fraction>(
                new decimal[,] { { 2, 2, 1, 7 }, { 7, 0, 9, 7 } },
                new int[] { 1, 1 },
                new string[,] { { "2", "2", "1", "7" }, { "0", "-7", "11/2", "-35/2" } },
                new Fraction[] { 1, "-5/2" }
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, int, decimal, Fraction>(
                new decimal[,] { { 1, 4, 1, 5 }, { 1, 4, 0, 5 }, { 2, 8, 7, 6 } },
                new int[] { 1, 1, 1 },
                new decimal[,] { { 1, 4, 1, 5 }, { 0, 0, -1, 0 }, { 0, 0, 0, -4 } },
                new Fraction[] { 1, 0, -1 }
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, int, decimal, Fraction>(
                new decimal[,] { { 1, 0, 3, 7, 2 }, { 4, 6, 9, 5, 4 }, { 4, 9, 7, 6, 0 }, { 6, 6, 7, 5, 5 }, { 2, 4, 9, 7, 1 } },
                new int[] { 1, 1, 1, 1, 1 },
                new decimal[,] { { 1, 0, 3, 7, 2 }, { 0, 6, -3, -23, -4 }, { 0, 0, -1m / 2, 25m / 2, -2 }, { 0, 0, 0, -214, 29 }, { 0, 0, 0, 0, -727m / 321 } },
                new Fraction[] { 1, -3, "3/2", -26, "-64/321" }
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, int, decimal, Fraction>(
                new decimal[,] { { 1, 1, 1, 1, 2 }, { -1, -1, -1, 1, 2 }, { 3, 3, 3, 4, 7 } },
                new int[] { 1, 1, 1 },
                new decimal[,] { { 1, 1, 1, 1, 2 }, { 0, 0, 0, 2, 4 }, { 0, 0, 0, 0, -1 } },
                new Fraction[] { 1, 2, -3 }
            )
        };
        yield return new object[] {
            new REFOfMatrixWithCoefficientParameter<decimal, decimal, decimal, decimal>(
                new decimal[,] { { 1, 1, 2 }, { 1, 3, 7 }, { 2, 6, 6 } },
                new decimal[] { 1, 1, 1 },
                new decimal[,] { { 1, 1, 2 }, { 0, 2, 5 }, { 0, 0, -8 } },
                new decimal[] { 1, 0, -1 }
            )
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}