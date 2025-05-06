namespace LinearAlgebra.Classes.SolutionSteps;

public record Multiplication_Result<T>
{
    public T[,] Result = new T[0, 0];
    public string[,]? Step { get; set; }
}