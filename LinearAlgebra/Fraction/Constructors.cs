namespace LinearAlgebra;

public partial class Linear
{
    public partial struct Fraction
    {
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
    }
}