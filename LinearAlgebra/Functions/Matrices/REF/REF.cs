namespace LinearAlgebra;

public partial class Linear
{
    private partial class Row_Echelon_Form
    {
        /// <summary>
        /// Finds the column index of the pivot in the given matrix.
        /// </summary>
        /// <param name="matrix">The matrix to search for the pivot.</param>
        /// <param name="row">The row index to start searching from. The search will be performed on this row and all rows below it.</param>
        /// <returns>The zero-based index of the column if a pivot is found, -1 otherwise.</returns>
        public static int FindPivot(Fraction[,] matrix, int row)
        {
            //Loop through all the columns in the matrix.
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                //If the first element in this column is not 0, then we found the pivot.
                if (matrix[row, column].Quotient != 0) return column;

                //Get the rest of the column elements.
                var elements = matrix.GetColumn(column, startFromIndex: row);

                //If all elements in this column are 0, then we skip this column.
                //This means the entire column is 0, so we don't have a pivot.
                if (elements.All(x => x.Quotient == 0)) continue;

                //If there is a single element in this column that's not 0, then we have a pivot in this column.
                return column;
            }

            //If we haven't found any pivot, return -1.
            //This means from the given row and down, all elements are 0.
            return -1;
        }

        /// <summary>
        /// Apply REF or RREF operation on given row
        /// </summary>
        /// <param name="pivotRow">Index of pivot row</param>
        /// <param name="targetedRow">Index of targeted row</param>
        /// <param name="columnStart">Index of column so we don't have to start from the first every time</param>
        /// <param name="scalar">The scalar</param>
        /// <param name="matrix">The matrix</param>
        /// <returns>The matrix after applying this operation on the entire target row</returns>
        public static Fraction[,] ClearRow(int pivotRow, int targetedRow, int columnStart, Fraction scalar, Fraction[,] matrix)
        {
            //Make the targeted element 0
            matrix[targetedRow, columnStart] = new(0);
            //Loop through the rest of the row
            for (int y = columnStart + 1; y < matrix.GetLength(1); y++)
            {
                //Get the new value as a fraction
                var testVal = scalar * matrix[pivotRow, y] + matrix[targetedRow, y];

                //If the new value is not an integer just assign it
                if (testVal.Quotient.IsDecimal()) matrix[targetedRow, y] = testVal;
                //Else assign it's quotient as the Numerator 
                else matrix[targetedRow, y] = new Fraction((double)testVal.Quotient);
            }
            return matrix;
        }

        /// <summary>
        /// Check If we have to swap in matrix[x, y]
        /// </summary>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        /// <param name="matrix">The matrix</param>
        /// <returns>
        /// -1, if we have to swap but we don't have a candidate
        /// <br></br>
        ///  0, if we don't have to swap
        /// <br></br>
        /// Else the index of the row that we have to swap with
        /// </returns>
        public static int CheckPossibleSwap(int x, int y, Fraction[,] matrix)
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
                return final.First().Key;
            }
            return 0;
        }

        /// <summary>
        /// Creates an array with indexes from start to end
        /// </summary>
        /// <param name="start">First Index</param>
        /// <param name="end">Last Index</param>
        /// <returns>An array with (end - start) length</returns>
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

        /// <summary>
        /// Swaps between two rows in a matrix(2d array)
        /// </summary>
        /// <typeparam name="T">The type of the matrix</typeparam>
        /// <param name="x">The first row</param>
        /// <param name="y">The Second row</param>
        /// <param name="matrix">The matrix</param>
        /// <returns>The new matrix</returns>
        public static T[,] SwapMatrix<T>(int x, int y, T[,] matrix)
        {
            int columns = matrix.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                (matrix[x, i], matrix[y, i]) = (matrix[y, i], matrix[x, i]);
            }
            return matrix;
        }

        /// <summary>
        /// Swaps between two elements in a coefficient(1d array)
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="x">The first elements</param>
        /// <param name="y"></param>
        /// <param name="coefficient"></param>
        /// <returns></returns>        
        public static T[] SwapCoefficient<T>(int x, int y, T[] coefficient)
        {
            (coefficient[x], coefficient[y]) = (coefficient[y], coefficient[x]);
            return coefficient;
        }
    }

    /// <summary>
    /// Custom Comparer
    /// </summary>
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
            else if (f1.Denominator == 1 && f1 < f2) return -1;
            else if (f2.Denominator == 1 && f1 > f2) return 1;
            else return 0; // otherwise they are equal
        }

        public int Compare(KeyValuePair<int, Fraction> x, KeyValuePair<int, Fraction> y)
        {
            return Compare(x.Value, y.Value);
        }
    }
}