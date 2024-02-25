using System.Text.Json.Serialization;

namespace LinearAlgebra.Classes;

[Serializable]
public partial struct Fraction
{
    [JsonPropertyName("numerator")]
    public double Numerator {  get; set; }
    [JsonPropertyName("denominator")]
    public double Denominator{  get; set; }
    [JsonIgnore]
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
    [JsonIgnore]
    public readonly double QuotientDouble
    {
        get => Numerator / Denominator;
    }
}
