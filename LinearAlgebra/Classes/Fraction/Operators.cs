namespace LinearAlgebra.Classes;

public partial struct Fraction
{
    public static Fraction operator +(Fraction a) => a;

    public static Fraction operator -(Fraction a) => new (-a.Numerator, a.Denominator);

    public static Fraction operator ++(Fraction a) => a + 1;

    public static Fraction operator --(Fraction a) => a - 1;

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
        return a + Convert.ToDouble(b);
    }

    public static Fraction operator -(Fraction a, object b)
    {
        if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invalid.");
        return a - Convert.ToDouble(b);
    }

    public static Fraction operator *(Fraction a, object b)
    {
        if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invalid.");
        return a * Convert.ToDouble(b);
    }

    public static Fraction operator /(Fraction a, object b)
    {
        if (!b.IsNumber()) throw new Exception($"The operation between {a.GetType().Name} and {b.GetType().Name} is invalid.");
        return a / Convert.ToDouble(b);
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

    public static implicit operator Fraction(int num)
    {
        return new Fraction(num, 1);
    }

    public static implicit operator Fraction(double num)
    {
        return ConvertDecimalToFraction(num);
    }

    public static implicit operator Fraction(decimal num)
    {
        return ConvertDecimalToFraction((double)num);
    }

    public static implicit operator Fraction(string fraction)
    {
        var index = fraction.IndexOf('/');
        if (index != -1)
        {
            try
            {
                double num = Convert.ToDouble(fraction[..index]);
                double den = Convert.ToDouble(fraction[(index + 1)..]);
                return new Fraction(num, den);
            }
            catch { }
        }
        else if (fraction.Contains('.'))
        {
            return ConvertDecimalToFraction(Convert.ToDouble(fraction));
        }
        else
        {
            if (double.TryParse(fraction, out double num))
            {
                return new Fraction(num);
            }
        }
        throw new FormatException("Fraction was not in a correct format\nAccepted Formats:" +
            "\n1) {Numerator}/{Denominator}\n2) {Numerator}");
    }

    public static explicit operator decimal(Fraction a) => a.Quotient;

    public static explicit operator double(Fraction a) => Convert.ToDouble(a.Quotient);

    public static explicit operator int(Fraction a) => Convert.ToInt32(a.Quotient);

    public static explicit operator string(Fraction a) => a.ToString();

}