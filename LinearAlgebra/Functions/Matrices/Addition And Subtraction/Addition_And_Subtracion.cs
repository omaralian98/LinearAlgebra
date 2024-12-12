namespace LinearAlgebra;

public partial class Linear
{
    private static Fraction[][,] CheckCoherenceForAddition_And_Subtraction<T>(params T[][,] matrices)
    {
        List<Fraction[,]> result = [matrices[0].GetFractions()];
        int m = matrices[0].GetLength(0), n = matrices[0].GetLength(1);
        for (int i = 1; i < matrices.Length; i++)
        {
            if (m != matrices[i].GetLength(0) || n != matrices[i].GetLength(1))
                throw new InvalidOperationException("The matrices aren't coherent");
            result.Add(matrices[i].GetFractions());
        }
        return [.. result];
    }

    public static T[,] Add<T>(params T[][,] matrices) => Add<T, T>(matrices);
    public static T[,] Subtract<T>(params T[][,] matrices) => Subtract<T, T>(matrices);

    public static Addition_And_Subtraction_Result<T> AddWithResult<T>(T[,] a, T[,] b) => AddWithResult<T, T>(a, b);
    public static Addition_And_Subtraction_Result<T> SubtractWithResult<T>(T[,] a, T[,] b) => SubtractWithResult<T, T>(a, b);

    public static Addition_And_Subtraction_Result<T>[] AddWithResult<T>(params T[][,] matrices) => AddWithResult<T, T>(matrices);
    public static Addition_And_Subtraction_Result<T>[] SubtractWithResult<T>(params T[][,] matrices) => SubtractWithResult<T, T>(matrices);



    /// <summary>
    /// Adds matrices to each other
    /// </summary>
    /// <typeparam name="R">The type of the returned matrix</typeparam>
    /// <typeparam name="T">The type of the given matrices</typeparam>
    /// <param name="matrices">The matrices that you want to add together</param>
    /// <returns>The result as an R matrix</returns>
    public static R[,] Add<R, T>(params T[][,] matrices)
    {
        var mat = CheckCoherenceForAddition_And_Subtraction(matrices);
        return Addition_And_Subtraction.Add(mat).GetTMatrix<R>();
    }
    /// <summary>
    /// Subtracts matrices from each other
    /// </summary>
    /// <typeparam name="R">The type of the returned matrix</typeparam>
    /// <typeparam name="T">The type of the given matrices</typeparam>
    /// <param name="matrices">The matrices that you want subtract</param>
    /// <returns>The result as an R matrix</returns>
    public static R[,] Subtract<R, T>(params T[][,] matrices)
    {
        var mat = CheckCoherenceForAddition_And_Subtraction(matrices);
        return Addition_And_Subtraction.Subtract(mat).GetTMatrix<R>();
    }

    /// <summary>
    /// Adds matrices to each other
    /// </summary>
    /// <typeparam name="R">The type of the returned matrix</typeparam>
    /// <typeparam name="T">The type of the given matrices</typeparam>
    /// <param name="a">The first matrix</param>
    /// <param name="b">The second matrix</param>
    /// <returns>Addition_And_Subtraction_Result of R =><br></br>R[,] Result <br></br>string[,]? Step</returns>
    public static Addition_And_Subtraction_Result<R> AddWithResult<R, T>(T[,] a, T[,] b)
    {
        var mat = CheckCoherenceForAddition_And_Subtraction(a, b);
        var result = Addition_And_Subtraction.AddWithResult(mat).First();
        return new Addition_And_Subtraction_Result<R> { Result = result.Result.GetTMatrix<R>(), Step = result.Step };
    }
    /// <summary>
    /// Subtracts matrices from each other
    /// </summary>
    /// <typeparam name="R">The type of the returned matrix</typeparam>
    /// <typeparam name="T">The type of the given matrices</typeparam>
    /// <param name="a">The first matrix</param>
    /// <param name="b">The second matrix</param>
    /// <returns>Addition_And_Subtraction_Result of R =><br></br>R[,] Result <br></br>string[,]? Step</returns>
    public static Addition_And_Subtraction_Result<R> SubtractWithResult<R, T>(T[,] a, T[,] b)
    {
        var mat = CheckCoherenceForAddition_And_Subtraction(a, b);
        var result = Addition_And_Subtraction.SubtractWithResult(mat).First();
        return new Addition_And_Subtraction_Result<R> { Result = result.Result.GetTMatrix<R>(), Step = result.Step };
    }

    /// <summary>
    /// Adds matrices to each other
    /// </summary>
    /// <typeparam name="R">The type of the returned matrix</typeparam>
    /// <typeparam name="T">The type of the given matrices</typeparam>
    /// <param name="matrices">The matrices that you want add</param>
    /// <returns>Addition_And_Subtraction_Result array of R =><br></br>R[,] Result <br></br>string[,]? Step</returns>
    public static Addition_And_Subtraction_Result<R>[] AddWithResult<R, T>(params T[][,] matrices)
    {
        var mat = CheckCoherenceForAddition_And_Subtraction(matrices);
        Addition_And_Subtraction_Result<R>[] ret = new Addition_And_Subtraction_Result<R>[matrices.Length - 1];
        var result = Addition_And_Subtraction.AddWithResult(mat);
        int index = 0;
        foreach (var it in result)
        {
            ret[index++] = new Addition_And_Subtraction_Result<R> { Result = it.Result.GetTMatrix<R>(), Step = it.Step };
        }
        return ret;
    }
    /// <summary>
    /// Subtracts matrices from each other
    /// </summary>
    /// <typeparam name="R">The type of the returned matrix</typeparam>
    /// <typeparam name="T">The type of the given matrices</typeparam>
    /// <param name="matrices">The matrices that you want subtract</param>
    /// <returns>Addition_And_Subtraction_Result array of R =><br></br>R[,] Result <br></br>string[,]? Step</returns>
    public static Addition_And_Subtraction_Result<R>[] SubtractWithResult<R, T>(params T[][,] matrices)
    {
        var mat = CheckCoherenceForAddition_And_Subtraction(matrices);
        Addition_And_Subtraction_Result<R>[] ret = new Addition_And_Subtraction_Result<R>[matrices.Length - 1];
        var result = Addition_And_Subtraction.SubtractWithResult(mat);
        int index = 0;
        foreach (var it in result)
        {
            ret[index++] = new Addition_And_Subtraction_Result<R> { Result = it.Result.GetTMatrix<R>(), Step = it.Step };
        }
        return ret;
    }


    public partial class Addition_And_Subtraction
    {
        public static Fraction[,] Add(params Fraction[][,] matrices)
        {
            Fraction[,] result = matrices[0];
            for (int i = 1; i < matrices.Length; i++)
            {
                result = Operation(result, matrices[i], operation: '+').Result;
            }
            return result;
        }

        public static Fraction[,] Subtract(params Fraction[][,] matrices)
        {
            Fraction[,] result = matrices[0];
            for (int i = 1; i < matrices.Length; i++)
            {
                result = Operation(result, matrices[i], operation: '-').Result;
            }
            return result;
        }

        public static IEnumerable<Addition_And_Subtraction_Result<Fraction>> AddWithResult(params Fraction[][,] matrices)
        {
            Fraction[,] result = matrices[0];
            for (int i = 1; i < matrices.Length; i++)
            {
                var answer = Operation(result, matrices[i], operation: '+', solution: true);
                yield return answer;
                result = answer.Result;
            }
        }

        public static IEnumerable<Addition_And_Subtraction_Result<Fraction>> SubtractWithResult(params Fraction[][,] matrices)
        {
            Fraction[,] result = matrices[0];
            for (int i = 1; i < matrices.Length; i++)
            {
                var answer = Operation(result, matrices[i], operation: '-', solution: true);
                yield return answer;
                result = answer.Result;
            }
        }

        private static Addition_And_Subtraction_Result<Fraction> Operation(Fraction[,] a, Fraction[,] b, char operation, bool solution = false)
        {
            int row = a.GetLength(0);
            int column = a.GetLength(1);
            Fraction[,] result = new Fraction[row, column];
            string[,]? step = solution ? new string[row, column] : null;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    result[i, j] = operation == '+' ? a[i, j] + b[i, j] : a[i, j] - b[i, j];
                    if (step is not null)
                    {
                        char currentOp = operation;
                        if (b[i, j].Quotient < 0) currentOp = operation == '+' ? '-' : '+';
                        step[i, j] = $"({a[i,j]} {currentOp} {b[i, j].Abs()})";
                    }
                }
            }
            return new Addition_And_Subtraction_Result<Fraction> { Result = result, Step = step };
        }
    }
}