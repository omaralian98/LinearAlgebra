namespace LinearAlgebra;

public partial class Linear
{
    public static LU_Result<T, T> LU<T>(T[,] matrix) => LU<T, T, T>(matrix);
    public static LU_Result<T, T>[] LUWithResult<T>(T[,] matrix) => LUWithResult<T, T, T>(matrix);


    public static LU_Result<R1, R2> LU<R1, R2, T>(T[,] matrix)
    {
        var res = LUClass.LU(matrix.GetFractions()).First();
        return new LU_Result<R1, R2> { U = res.U.GetTMatrix<R1>(), L = res.L.GetTMatrix<R2>()};
    }
    public static LU_Result<R1, R2>[] LUWithResult<R1, R2, T>(T[,] matrix)
    {
        var res = LUClass.LU(matrix.GetFractions());
        var result = new List<LU_Result<R1, R2>>();
        foreach (var step in res)
        {
            result.Add(new LU_Result<R1, R2> { U = step.U.GetTMatrix<R1>(), L = step.L.GetTMatrix<R2>() });
        }
        return [.. result];
    }

    private class LUClass
    {
        public static IEnumerable<LU_Result<Fraction, Fraction>> LU(Fraction[,] matrix, bool solution = false, CancellationToken cancellationToken = default)
        {
            int n = matrix.GetLength(0);
            Fraction[,] identity = Fraction.GenerateIdentityMatrix(n);
            if (solution)
            {
                yield return new LU_Result<Fraction, Fraction> { U = (Fraction[,])matrix.Clone(), L = (Fraction[,])identity.Clone() };
            }
            for (int currentRow = 0; currentRow < n; currentRow++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException("Task was canceled.");
                }
                int currentColumn = Row_Echelon_Form.FindPivot(matrix, currentRow);
                if (currentColumn == -1 || Row_Echelon_Form.CheckPossibleSwap(currentRow, currentColumn, matrix) != 0)
                {
                    break;
                }
                var cur = ClearPivotColumn(matrix, identity, currentRow, currentColumn, solution);
                foreach (var step in cur)
                {
                    yield return step;
                }
            }
            if (solution == false)
            {
                yield return new LU_Result<Fraction, Fraction> { U = matrix, L = identity };
            }
        }

        private static IEnumerable<LU_Result<Fraction, Fraction>> ClearPivotColumn(Fraction[,] matrix, Fraction[,] identity, int pivotRow, int column, bool solution = false)
        {
            //Looping through this column
            for (int targetedRow = pivotRow; targetedRow < matrix.GetLength(0); targetedRow++)
            {//If the current row is the pivot or it's already 0 skip it
                if (targetedRow == pivotRow || matrix[targetedRow, column].Quotient == 0)
                {
                    continue;
                }

                //Else define a scalar = -{Target}/{Pivot}
                Fraction scalar = -matrix[targetedRow, column] / matrix[pivotRow, column];

                //Apply this scalar to the entire row
                matrix = Row_Echelon_Form.ClearRow(pivotRow, targetedRow, column, scalar, matrix);

                identity[targetedRow, column] = -scalar;
                if (solution)
                {//If we want the solution step by step return this step
                    yield return new LU_Result<Fraction, Fraction>
                    {
                        Description = $"{scalar} R{pivotRow + 1} + R{targetedRow + 1} ----> R{targetedRow + 1}",
                        U = (Fraction[,])matrix.Clone(),
                        L = (Fraction[,])identity.Clone(),
                    };
                }
            }
        }
    }
}
