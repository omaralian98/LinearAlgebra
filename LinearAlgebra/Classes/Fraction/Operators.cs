namespace LinearAlgebra.Classes;

public partial struct Fraction
{
    public static Fraction operator +(Fraction a) => a;

    public static Fraction operator -(Fraction a) => new (-a.Numerator, a.Denominator);

    public static Fraction operator *(Fraction a, Fraction b) => new (a.Numerator * b.Numerator, a.Denominator * b.Denominator);

    public static Fraction operator /(Fraction a, Fraction b) => new (a.Numerator * b.Denominator, a.Denominator * b.Numerator);

    public static Fraction operator +(Fraction a, Fraction b)
    {
        double dividend = (a.Numerator * b.Denominator) + (b.Numerator * a.Denominator);
        double divisor = a.Denominator * b.Denominator;
        return new Fraction(dividend, divisor);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        double dividend = (a.Numerator * b.Denominator) - (b.Numerator * a.Denominator);
        double divisor = a.Denominator * b.Denominator;
        return new Fraction(dividend, divisor);
    }

    public static Fraction operator +(Fraction a, object b)
    {
        if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invalid.");
        double num = a.Numerator + (double)b * a.Denominator;
        double den = a.Denominator;
        return new Fraction(num, den);
    }

    public static Fraction operator -(Fraction a, object b)
    {
        if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invalid.");
        double num = a.Numerator - (double)b * a.Denominator;
        double den = a.Denominator;
        return new Fraction(num, den);
    }

    public static Fraction operator *(Fraction a, object b)
    {
        if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invalid.");
        return new Fraction(a.Numerator * (double)b, a.Denominator);
    }

    public static Fraction operator /(Fraction a, object b)
    {
        if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invalid.");
        return new Fraction(a.Numerator, a.Denominator * (double)b);
    }

    public static bool operator ==(Fraction a, Fraction b)
    {
        return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
    }

    public static bool operator !=(Fraction a, Fraction b)
    {
        return !(a == b);
    }

    public static bool operator <(Fraction a, Fraction b)
    {
        return a.Quotient < b.Quotient;
    }
    public static bool operator <=(Fraction a, Fraction b)
    {
        return a.Quotient <= b.Quotient;
    }
    public static bool operator >(Fraction a, Fraction b)
    {
        return a.Quotient > b.Quotient;
    }
    public static bool operator >=(Fraction a, Fraction b)
    {
        return a.Quotient >= b.Quotient;
    }
}