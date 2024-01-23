namespace LinearAlgebra.Classes;

public record Result
{
    public Fraction[,] Matrix { get; set; } = new Fraction[0, 0];
    public Steps[] Steps { get; set; } = Array.Empty<Steps>();
    public MatrixSteps[] MatrixSteps { get; set; } = Array.Empty<MatrixSteps>();
}
