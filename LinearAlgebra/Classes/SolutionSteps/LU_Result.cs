namespace LinearAlgebra.Classes.SolutionSteps;

public record LU_Result<T, S>
{
    public S[,] L = new S[0, 0];
    public T[,] U = new T[0, 0];
    public string Description { get; set; } = "";
}