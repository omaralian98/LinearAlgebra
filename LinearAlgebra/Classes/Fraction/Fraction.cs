namespace LinearAlgebra.Classes;

public partial struct Fraction
{
    public double Numerator;
    public double Denominator;
    public readonly decimal Quotient
    {
        get 
        {
            decimal quotient;
            try
            {
                quotient = (decimal)Numerator / (decimal)Denominator;
            }
            catch
            {
                quotient = (decimal)(Numerator / Denominator);
            }
            return quotient;
        }
    }
}
