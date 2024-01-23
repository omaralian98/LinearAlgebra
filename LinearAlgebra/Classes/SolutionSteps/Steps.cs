namespace LinearAlgebra.Classes;

public struct Steps
{
    public Steps() { }
    public int PivotRow { get; set; } = -1;
    public int EffectedRow { get; set; } = -1;
    public Fraction Scalar { get; set; } = new Fraction();
    public Operations Operation { get; set; } = Operations.Swap;
    public string StepDescription { get; set; } = "";
}