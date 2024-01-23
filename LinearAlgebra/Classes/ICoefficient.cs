namespace LinearAlgebra.Classes;

public interface ICoefficient
{
    public static ICoefficient operator +(ICoefficient a, ICoefficient b)
    {
        return a.Add(a, b);
    }
    public static ICoefficient operator *(ICoefficient a, Fraction b)
    {
        return a.Multiply(a, b);
    }

    public ICoefficient Add(ICoefficient a, ICoefficient b);
    public ICoefficient Multiply(ICoefficient a, Fraction b);

}