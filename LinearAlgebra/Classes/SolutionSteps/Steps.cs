namespace LinearAlgebra.Classes;

public struct MatrixStep
{
    public string? StepDescription { get; set; }
    public Fraction[,]? Matrix { get; set; }
}

public struct Steps
{
    public Steps() { }
    public int PivotRow { get; set; } = -1;
    public int EffectedRow { get; set; } = -1;
    public Fraction Scalar { get; set; } = new Fraction();
    public Operations Operation { get; set; } = Operations.Swap;
}

public enum Operations
{
    Swap,
    Scale
}