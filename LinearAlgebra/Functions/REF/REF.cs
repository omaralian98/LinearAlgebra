using LinearAlgebra.Classes;
using MathNet.Symbolics;

namespace LinearAlgebra;
public partial class Linear
{

    private static int FindPivot(Fraction[,] matrix, int row)
    {
        for (int column = 0; column < matrix.GetLength(1); column++)
        {
            if (matrix[row, column].Quotient != 0) return column;
            var elements = Enumerable.Range(0, matrix.GetLength(0)).SkipWhile(x => x <= row)
                .Select(x => matrix[x, column]).ToArray();
            if (elements.All(x => x.Quotient == 0)) continue;
            return column;
        }
        return -1;
    }

    private static Fraction[,] ClearRow(int pivotRow, int targetedRow, int columnStart, Fraction scalar, Fraction[,] matrix)
    {
        matrix[targetedRow, columnStart] = new(0);
        for (int y = columnStart + 1; y < matrix.GetLength(1); y++)
        {
            var testVal = scalar * matrix[pivotRow, y] + matrix[targetedRow, y];
            if (testVal.Quotient.IsDecimal()) matrix[targetedRow, y] = testVal;
            else matrix[targetedRow, y] = new Fraction((double)testVal.Quotient);
        }
        return matrix;
    }

    private static int CheckPossibleSwap(int x, int y, Fraction[,] matrix)
    {
        if (matrix[x, y].Quotient == 0)
        {
            //gets the column the x column
            var column = matrix.GetColumn(y, x + 1);
            //If all the elements in this column are 0 then we don't have a row to swap with
            if (column.All(x => x.Quotient == 0)) return -1;
            //gets the index of every element it the column array
            var keys = CreateIndexArray(x + 1, matrix.GetLength(0));
            //Creates a dictionary of The element and it's index
            Dictionary<int, Fraction> dic = keys
                .Zip(column, (key, value) => new { key, value })
                 .ToDictionary(x => x.key, x => x.value);
            //Reorder the dictionary accordingly
            var final = dic.Order(comparer: new CustomCompare()).ToArray();
            //return the first index
            return y + final.First().Key;
        }
        return 0;
    }

    public static int[] CreateIndexArray(int start, int end)
    {
        int[] array = new int[end - start];
        int counter = 0;
        for (int i = start; i < end; i++)
        {
            array[counter++] = i;
        }
        return array;
    }

    private static T[,] SwapMatrix<T>(int x, int y, T[,] matrix)
    {
        int columns = matrix.GetLength(1);
        for (int i = 0; i < columns; i++)
        {
            (matrix[x, i], matrix[y, i]) = (matrix[y, i], matrix[x, i]);
        }
        return matrix;
    }
    private static T[] SwapCoefficient<T>(int x, int y, T[] coefficient)
    {
        (coefficient[x], coefficient[y]) = (coefficient[y], coefficient[x]);
        return coefficient;
    }
}

public class CustomCompare : IComparer<KeyValuePair<int, Fraction>>
{
    public static int Compare(Fraction f1, Fraction f2)
    {
        if (f1.Quotient == 1 && f2.Quotient == 1) return 0; // Both are equal
        else if (f1.Quotient == 1) return -1; // f1 is 1, comes first
        else if (f2.Quotient == 1) return 1;  // f2 is 1, comes first
        else if (f1.Quotient == -1 && f2.Quotient == -1) return 0; // Both are equal
        else if (f1.Quotient == -1) return -1; // f1 is -1, comes second
        else if (f2.Quotient == -1) return 1;  // f2 is -1, comes second
        else return 0; // otherwise they are equal
    }

    public int Compare(KeyValuePair<int, Fraction> x, KeyValuePair<int, Fraction> y)
    {
        return Compare(x.Value, y.Value);
    }
}