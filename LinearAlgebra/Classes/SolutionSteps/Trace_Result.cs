namespace LinearAlgebra.Classes.SolutionSteps;

public record Trace_Result<T>
{
    public T Result { get; set; }
    public string Step { get; set; } = string.Empty;
}