namespace LinearAlgebra.Classes.SolutionSteps;


public record Addition_And_Subtraction_Result<T>
{
    public T[,] Result = new T[0, 0];
    public string[,]? Step { get; set; }
}