namespace LinearAlgebra.Classes;

public partial struct Fraction
{
    public Fraction(Fraction fraction) => this = fraction; 
    public Fraction(double num)
    {
        Numerator = num;
        Denominator = 1;
    }

    public Fraction(double num, double den)
    {
        if (den == 0) throw new DivideByZeroException();
        double gcd = GCD(Math.Abs(num), Math.Abs(den));
        Denominator = Math.Abs(den) / gcd;
        Numerator = num / gcd;
        Numerator *= den < 0 ? -1 : 1;
    }

    public Fraction(string fraction) : this((Fraction)fraction) { }
}