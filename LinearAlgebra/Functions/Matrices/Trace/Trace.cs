using LinearAlgebra.Classes.SolutionSteps;

namespace LinearAlgebra;

public partial class Linear
{
    private static void CheckCoherenceForTrace<T>(T[,] matrix)
    {
        string errorMessage = $"Non-square matrices do not have Trace.";
        if (matrix.GetLength(0) != matrix.GetLength(1)) throw new InvalidOperationException(errorMessage);
    }

    public static T Trace<T>(T[,] matrix) => Trace<T, T>(matrix);

    public static R Trace<R, T>(T[,] matrix)
    {
        var answer = TraceClass.Trace(matrix.GetFractions());
        return (R)answer.ToType(typeof(R), null);
    }    
    
    public static Trace_Result<T> TraceWithResult<T>(T[,] matrix) => TraceWithResult<T, T>(matrix);

    public static Trace_Result<R> TraceWithResult<R, T>(T[,] matrix)
    {
        var answer = TraceClass.TraceWithResult(matrix.GetFractions());
        return new Trace_Result<R>
        {
            Result = (R)answer.Result.ToType(typeof(R), null),
            Step = answer.Step
        };
    }

    private class TraceClass
    {
        public static Fraction Trace(Fraction[,] matrix)
        {
            Fraction trace = matrix[0, 0];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                trace += matrix[i, i];
            }
            return trace;
        }

        public static Trace_Result<Fraction> TraceWithResult(Fraction[,] matrix)
        {
            Fraction trace = matrix[0, 0];
            StringBuilder step = new($"({trace}) ");
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                trace += matrix[i, i];
                step.Append($"+ ({matrix[i, i]}) ");
            }

            return new Trace_Result<Fraction>
            {
                Result = matrix[0, 0],
                Step = step.ToString().TrimEnd()
            };
        }
    }
}