namespace LinearAlgebra.Classes;

public record REF_Result<T> : REF_Result where T : ICoefficient
{
    public T[] Coefficient { get; set; } = [];
}

public record REF_Result
{
    public Fraction[,] Matrix { get; set; } = new Fraction[0, 0];
    public string Description { get; set; } = "";
}