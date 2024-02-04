namespace LinearAlgebra.Classes;

public partial struct Fraction
{
    public double Numerator;
    public double Denominator;
    public readonly decimal Quotient
    {
        get { return (decimal)Numerator / (decimal)Denominator; }
    }
}
