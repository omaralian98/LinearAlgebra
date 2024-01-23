namespace LinearAlgebra;
public partial class Linear
{
    public static SpecialString[] GetCoefficient(SpecialString[] coefficient, Steps[] steps)
    {
        for (int i = 0; i < steps.Length; i++) 
        {
            var pivot = steps[i].PivotRow;
            var target = steps[i].EffectedRow;
            Fraction scalar = steps[i].Scalar;
            if (steps[i].Operation is Operations.Scale)
            {
                coefficient[target] = (coefficient[pivot] * scalar) + coefficient[target];
            }
            else
            {
                coefficient = SwapCoefficient(pivot, target, coefficient);
            }
        }
        return coefficient;
    }

    public static Fraction[] GetCoefficient(Fraction[] coefficient, Steps[] steps)
    {
        for (int i = 0; i < steps.Length; i++)
        {
            var pivot = steps[i].PivotRow;
            var target = steps[i].EffectedRow;
            Fraction scalar = steps[i].Scalar;
            if (steps[i].Operation is Operations.Scale)
            {
                coefficient[target] = (coefficient[pivot] * scalar) + coefficient[target];
            }
            else
            {
                coefficient = SwapCoefficient(pivot, target, coefficient);
            }
        }
        return coefficient;
    }

    public static Result REF(Fraction[,] matrix, bool solution = false, CancellationToken token = default)
    {
        int matrixRows = matrix.GetLength(0); //Gets the number of rows
        int matrixColumns = matrix.GetLength(1); //Gets the number of columns
        List<Steps> steps = new List<Steps>();
        List<MatrixSteps>? matrixSteps = solution ? new List<MatrixSteps>() : null;
        for (int currentRow = 0; currentRow < Math.Min(matrixRows, matrixColumns); currentRow++)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException("Task was canceled.");
            }
            (matrix, steps) = ReOrderMatrix(matrix, steps, currentRow, matrixSteps);
            int currentColumn = FindPivot(matrix, currentRow);
            if (currentColumn == -1) continue;
            (matrix, steps) = ClearPivotColumn(matrix, steps, currentRow, currentColumn, reduced: false, matrixSteps);
        }
        return new Result { Matrix = matrix, Steps = steps.ToArray(), MatrixSteps = matrixSteps?.ToArray() ?? Array.Empty<MatrixSteps>() };
    }
    private static (Fraction[,], List<Steps>) ReOrderMatrix(Fraction[,] matrix, List<Steps> steps, int row, List<MatrixSteps>? solution)
    {
        var (result, x, y) = CheckPossibleSwap(row, row, matrix);
        if (result)
        {
            matrix = SwapMatrix(x, y, matrix);
            solution?.Add(new MatrixSteps
            {
                Description = $"Swap between R{x + 1} and R{y + 1}",
                Matrix = (Fraction[,])matrix.Clone(),
            });
            steps.Add(new Steps { PivotRow = x, EffectedRow = y, Operation = Operations.Swap });
        }
        return (matrix, steps);
    }

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

    private static (Fraction[,], List<Steps>) ClearPivotColumn(Fraction[,] matrix, List<Steps> steps, int pivotRow, int column, bool reduced, List<MatrixSteps>? solution)
    {
        int targetedRow = reduced ? 0 : pivotRow;
        for (; targetedRow < matrix.GetLength(0); targetedRow++)
        {
            if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0) continue;
            Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];
            matrix = ClearRow(pivotRow, targetedRow, column, scalar, matrix);
            solution?.Add(new MatrixSteps
            {
                Description = $"{scalar}R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                Matrix = (Fraction[,])matrix.Clone(),
            });
            steps.Add(new Steps { PivotRow = pivotRow, EffectedRow = targetedRow, Scalar = scalar, Operation = Operations.Scale });
        }
        return (matrix, steps);
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