namespace LinearAlgebra;

public partial class Linear
{
    private static Fraction[][,] CheckCoherenceForMuliplication<T>(params T[][,] matrices)
    {
        List<Fraction[,]> result = [matrices[0].GetFractions()];
        int m = matrices[0].GetLength(0); 
        int n = matrices[0].GetLength(1);
        for (int i = 1; i < matrices.Length; i++)
        {
            int p = matrices[i].GetLength(0);
            int q = matrices[i].GetLength(1);
            if (n != p)
                throw new InvalidOperationException("The matrices aren't coherent");
            n = q;
            result.Add(matrices[i].GetFractions());
        }
        return [.. result];
    }

    public static T[,] Multiply<T>(params T[][,] matrices) => Multiply<T, T>(matrices);
    public static Multiplication_Result<T> MultiplyWithResult<T>(T[,] a, T[,] b) => MultiplyWithResult<T, T>(a, b);
    public static Multiplication_Result<T>[] MultiplyWithResult<T>(params T[][,] matrices) => MultiplyWithResult<T, T>(matrices);


    public static R[,] Multiply<R, T>(params T[][,] matrices)
    {
        var mat = CheckCoherenceForMuliplication(matrices);
        var result = Multiplication.Multiply(mat);
        return result.GetTMatrix<R>();
    }
    public static Multiplication_Result<R> MultiplyWithResult<R, T>(T[,] a, T[,] b)
    {
        var mat = CheckCoherenceForMuliplication(a, b);
        var result = Multiplication.MultiplyWithResult(mat).First();
        return new Multiplication_Result<R> { Result = result.Result.GetTMatrix<R>(), Step = result.Step };
    }
    public static Multiplication_Result<R>[] MultiplyWithResult<R, T>(params T[][,] matrices)
    {
        var mat = CheckCoherenceForMuliplication(matrices);
        Multiplication_Result<R>[] ret = new Multiplication_Result<R>[matrices.Length - 1];
        var result = Multiplication.MultiplyWithResult(mat);
        int index = 0;
        foreach (var it in result)
        {
            ret[index++] = new Multiplication_Result<R> { Result = it.Result.GetTMatrix<R>(), Step = it.Step };
        }
        return ret;
    }

    public static T[,] Multiply<T>(T[,] matrix, T scale) => Multiply<T, T, T>(matrix, scale);
    public static T[,] Multiply<T, S>(T[,] matrix, S scale) => Multiply<T, T, S>(matrix, scale);
    public static R[,] Multiply<R, T, S>(T[,] matrix, S scale)
    {
        Fraction fraction = (Fraction)(scale?.ToString() ?? "1");
        return Multiplication.Scale(matrix.GetFractions(), fraction).GetTMatrix<R>();
    }

    private class Multiplication
    {
        public static Fraction[,] Multiply(params Fraction[][,] matrices)
        {
            Fraction[,] result = matrices[0];
            for (int i = 1; i < matrices.GetLength(0); i++)
            {
                result = Multiply(result, matrices[i]);
            }
            return result;
        }

        public static Fraction[,] Multiply(Fraction[,] a, Fraction[,] b)
        {
            int m = a.GetLength(0);
            int q = b.GetLength(1);
            Fraction[,] result = new Fraction[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    var row = a.GetRow(i);
                    var column = b.GetColumn(j);
                    result[i, j] = MultiplyRowAColumn(row, column);
                }
            }
            return result;
        }

        private static Fraction MultiplyRowAColumn(Fraction[] row, Fraction[] column)
        {
            Fraction result = row[0] * column[0];
            for (int i = 1; i < row.Length; i++) 
            {
                result += row[i] * column[i];
            }
            return result;
        }



        public static IEnumerable<Multiplication_Result<Fraction>> MultiplyWithResult(params Fraction[][,] matrices)
        {
            Fraction[,] result = matrices[0];
            for (int i = 1; i < matrices.GetLength(0); i++)
            {
                var res = MultiplyWithResult(result, matrices[i]);
                yield return res;
                result = res.Result;
            }
        }

        public static Multiplication_Result<Fraction> MultiplyWithResult(Fraction[,] a, Fraction[,] b)
        {
            int m = a.GetLength(0);
            int q = b.GetLength(1);
            string[,] step = new string[m, q];
            Fraction[,] result = new Fraction[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    var row = a.GetRow(i);
                    var column = b.GetColumn(j);
                    step[i, j] = MultiplyRowAColumnWithResult(row, column, out result[i, j]);
                }
            }
            return new Multiplication_Result<Fraction> { Result = result, Step = step };
        }

        private static string MultiplyRowAColumnWithResult(Fraction[] row, Fraction[] column, out Fraction fraction)
        {
            string result = $"[({row[0]} * {column[0]})";
            fraction = row[0] * column[0];
            for (int i = 1; i < row.Length; i++)
            {
                result += $" + ({row[i]} * {column[i]})";
                fraction += row[i] * column[i];
            }
            return result + "]";
        }



        public static Fraction[,] MultiplyVectors(Fraction[] vectorA, Fraction[] vectorB)
        {
            int m = vectorA.Length;
            int q = vectorB.Length;
            Fraction[,] result = new Fraction[m, q];
            for (int i = 0; i < m; i++)
            {
                Fraction A = vectorA[i];
                for (int j = 0; j < q; j++)
                {
                    Fraction B = vectorB[j];
                    result[i, j] = A * B;
                }
            }
            return result;
        }

        public static Fraction[,] Scale(Fraction[,] matrix, Fraction scalar)
        {
            for (int i = 0; i < matrix.GetLength(0); i++) 
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matrix[i, j] *= scalar;
                }
            }
            return matrix;
        }
    }
}