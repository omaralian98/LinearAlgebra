namespace LinearAlgebra.Classes;

public record Result<T> : Result where T : ICoefficient
{
    public new MatrixStep<T>[] MatrixSteps { get; set; } = [];
    public T[] Coefficient { get; set; } = [];
}

public record Result
{
    public Fraction[,] Matrix { get; set; } = new Fraction[0, 0];
    public MatrixStep[] MatrixSteps { get; set; } = [];
}
