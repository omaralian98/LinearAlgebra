namespace LinearAlgebra.Classes;

public partial struct Fraction
{
    private static double GCD(double a, double b)
    {
        if (a == 1 || b == 1) return 1;
        while (a > 0 && b > 0)
        {
            if (a > b) a %= b;
            else b %= a;
        }
        if (a == 0) return b;
        return a;
    }

    public override string ToString()
    {
        if (Denominator == 1) return Numerator.ToString();
        else if (!Quotient.IsDecimal()) return Quotient.ToString();
        return $"{Numerator}/{Denominator}";
    }
}