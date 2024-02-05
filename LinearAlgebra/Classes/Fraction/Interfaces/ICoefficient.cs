namespace LinearAlgebra.Classes;

public partial struct Fraction : ICoefficient
{
    public ICoefficient Add(ICoefficient a, ICoefficient b)
    {
        Fraction x = (Fraction)a;
        Fraction y = (Fraction)b;
        return x + y;
    }

    public ICoefficient Multiply(ICoefficient a, Fraction b)
    {
        Fraction x = (Fraction)a;
        Fraction y = b;
        return x * y;
    }
}