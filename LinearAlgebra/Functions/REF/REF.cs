using LinearAlgebra.Classes;

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

    private static (bool, int, int) CheckPossibleSwap(int x, int y, Fraction[,] matrix)
    {//if the pivot is 0 than there is a swap 
        if (matrix[x, y].Quotient == 0)
        {
            int num = -1;
            for (int i = x + 1; i < matrix.GetLength(0); i++)
            {//Loops through all the row to find a suitable row to swap
                decimal current = matrix[i, y].Quotient;
                //If we finds 1 or -1 or any number that's not 0.
                if (current == 1 || current == -1 || current != 0)
                {
                    num = i - x;
                    break;
                }
            }//If num is still -1 that means all this column is 0 so we return false and -1, -1
            if (num == -1) return (false, -1, -1);
            return (true, x, y + num); //Else we return true and the coordinate of the row.
        }//If not we return false because we don't have too swap
        return (false, 0, 0);
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