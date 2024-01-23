namespace LinearAlgebra.Classes;

public record MatrixStep<T> : MatrixStep where T : ICoefficient
{
    public T[]? Coefficient { get; set; }
}

public record MatrixStep 
{
    public Fraction[,]? Matrix { get; set; }
    public string? Description { get; set; }
}