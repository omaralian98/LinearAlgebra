namespace LinearAlgebra
{
    public partial class Linear
    {
        public partial struct Fraction
        {
            public double Numerator;
            public double Denominator;
            public readonly decimal Quotient
            {
                get { return (decimal)Numerator / (decimal)Denominator; }
            }
            public override string ToString()
            {
                if (Denominator == 1) return Numerator.ToString();
                else if (!Quotient.IsDecimal()) return Quotient.ToString();
                return $"{Numerator}/{Denominator}";
            }
        }
    }
}
