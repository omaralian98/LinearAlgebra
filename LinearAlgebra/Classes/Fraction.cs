using MathNet.Numerics;
using System.Text.Json.Serialization;
using Xunit.Abstractions;

namespace LinearAlgebra.Classes;

[Serializable]
public struct Fraction : IConvertible, IXunitSerializable, ICoefficient, IEquatable<Fraction>, IComparable<Fraction>
{
    [JsonPropertyName("numerator")]
    public double Numerator { get; set; }
    [JsonPropertyName("denominator")]
    public double Denominator{ get; set; }
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

    public Fraction(Fraction fraction) => this = fraction;
    public Fraction(double num) : this(num, 1) { }

    public Fraction(double num, double den)
    {
        if (den == 0) throw new DivideByZeroException();
        double gcd = GCD(Math.Abs(num), Math.Abs(den));
        Denominator = Math.Abs(den) / gcd;
        Numerator = num / gcd;
        Numerator *= den < 0 ? -1 : 1;
    }

    public Fraction(string fraction) : this((Fraction)fraction) { }

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

    public readonly Fraction Abs() => new(Math.Abs(Numerator), Denominator);
    public readonly decimal DecimalAbs() => Math.Abs(Quotient);


    public readonly string ValueToString()
    {
        try
        {
            return Quotient.ToString();
        }
        catch
        {
            return QuotientDouble.ToString();
        }
    }

    public override string ToString()
    {
        if (Numerator == 0) return "0";
        if (Denominator == 1) return Numerator.ToString();
        else if (!QuotientDouble.IsDecimal()) return QuotientDouble.ToString();
        return $"{Numerator}/{Denominator}";
    }

    private const decimal Min = -9;
    private const decimal Max = 9;

    public static Fraction[] GenerateRandomVector(int row, RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        return GenerateRandomVector(row, Min, Max, randomFraction);
    }

    public static Fraction[] GenerateRandomVector(int row, decimal min, decimal max, RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        if (max - min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }

        Fraction[] matrix = new Fraction[row];
        for (int i = 0; i < row; i++)
        {
            matrix[i] = GenerateRandomFraction(min, max, randomFraction);
        }
        return matrix;
    }


    public static Fraction[,] GenerateRandomMatrix(int row, int column, RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        return GenerateRandomMatrix(row, column, Min, Max, randomFraction);
    }

    public static Fraction[,] GenerateRandomMatrix(int row, int column, decimal min, decimal max, RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        if (max - min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }

        Fraction[,] matrix = new Fraction[row, column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                matrix[i, j] = GenerateRandomFraction(min, max, randomFraction);
            }
        }
        return matrix;
    }

    public static Fraction[][] GenerateRandomMatrixJagged(int row, int column, RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        return GenerateRandomMatrixJagged(row, column, Min, Max, randomFraction);
    }

    public static Fraction[][] GenerateRandomMatrixJagged(int row, int column, decimal min, decimal max, RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        if (max - min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }

        Fraction[][] matrix = new Fraction[row][];
        for (int i = 0; i < row; i++)
        {
            matrix[i] = new Fraction[column];
            for (int j = 0; j < column; j++)
            {
                matrix[i][j] = GenerateRandomFraction(min, max, randomFraction);
            }
        }
        return matrix;
    }

    public static Fraction GenerateRandomFraction(RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        return GenerateRandomFraction(Min, Max, randomFraction);
    }

    public static Fraction GenerateRandomFraction(decimal min, decimal max, RandomFractionGenerationType randomFraction = RandomFractionGenerationType.Simplified)
    {
        decimal range = max - min;
        if (range < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "Shouldn't be greater than max");
        }

        Random rand = new();
        var randomValue = rand.NextDouble() * (double)range + (double)min;
        if (randomFraction == RandomFractionGenerationType.IntegersOnly)
        {
            randomValue = rand.Next(Convert.ToInt32(min), Convert.ToInt32(max));
        }
        else if (randomFraction == RandomFractionGenerationType.IntegersPrefered && rand.Next() % 2 == 0)
        {
            randomValue = Math.Round(randomValue, 0);
        }
        else if (randomFraction == RandomFractionGenerationType.Simplified)
        {
            randomValue = Math.Round(randomValue, 1);
        }
        return ConvertDecimalToFraction(randomValue);
    }

    public static Fraction ConvertDecimalToFraction(double value, double accuracy = 0.000001)
    {
        if (accuracy <= 0.0 || accuracy >= 1.0)
        {
            throw new ArgumentOutOfRangeException(nameof(accuracy), "Must be > 0 and < 1.");
        }

        int sign = Math.Sign(value);

        if (sign == -1)
        {
            value = Math.Abs(value);
        }

        // Accuracy is the maximum relative error; convert to absolute maxError
        double maxError = sign == 0 ? accuracy : value * accuracy;

        int n = (int)Math.Floor(value);
        value -= n;

        if (value < maxError)
        {
            return new Fraction(sign * n, 1);
        }

        if (1 - maxError < value)
        {
            return new Fraction(sign * (n + 1), 1);
        }

        // The lower fraction is 0/1
        int lower_n = 0;
        int lower_d = 1;

        // The upper fraction is 1/1
        int upper_n = 1;
        int upper_d = 1;

        while (true)
        {
            // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
            int middle_n = lower_n + upper_n;
            int middle_d = lower_d + upper_d;

            if (middle_d * (value + maxError) < middle_n)
            {
                // real + error < middle : middle is our new upper
                upper_n = middle_n;
                upper_d = middle_d;
            }
            else if (middle_n < (value - maxError) * middle_d)
            {
                // middle < real - error : middle is our new lower
                lower_n = middle_n;
                lower_d = middle_d;
            }
            else
            {
                // Middle is our best fraction
                return new Fraction((n * middle_d + middle_n) * sign, middle_d);
            }
        }
    }

    public static Fraction[,] GenerateIdentityMatrix(int n)
    {
        var identity = new Fraction[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                identity[i, j] = 0;
            }
            identity[i, i] = 1;
        }
        return identity;
    }

    public static Fraction[][] GenerateIdentityMatrixJagged(int n)
    {
        var identity = new Fraction[n][];
        for (int i = 0; i < n; i++)
        {
            identity[i] = new Fraction[n];
            for (int j = 0; j < n; j++)
            {
                identity[i][j] = 0;
            }
            identity[i][i] = 1;
        }
        return identity;
    }

    public static bool TryParse(string value, out Fraction result)
    {
        try
        {
            result = (Fraction)value;
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static bool TryParse(string value, out Fraction result, Fraction defaultValue)
    {
        try
        {
            result = (Fraction)value;
            return true;
        }
        catch
        {
            result = defaultValue;
            return false;
        }
    }

    public static Fraction operator +(Fraction a) => a;

    public static Fraction operator -(Fraction a) => new(-a.Numerator, a.Denominator);

    public static Fraction operator ++(Fraction a) => a + 1;

    public static Fraction operator --(Fraction a) => a - 1;

    public static Fraction operator *(Fraction a, Fraction b) => new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

    public static Fraction operator /(Fraction a, Fraction b) => new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);

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
        return a.Quotient == b.Quotient;
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
        static int MiddleIndexOf(string str, char a)
        {
            int counter = 0;
            List<int> Indexes = [];
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == a)
                {
                    Indexes.Add(i);
                    counter++;
                }
            }
            if (counter == 0) return -1;
            else if (counter.IsEven()) return -2;
            else return Indexes[(counter - 1) / 2];
        }
        var index = MiddleIndexOf(fraction, '/');
        if (index != -1)
        {
            try
            {
                if (fraction.IndexOf('/', startIndex: index + 1) != -1)
                {
                    Fraction num1 = fraction[..index];
                    Fraction den1 = fraction[(index + 1)..];
                    return num1 / den1;
                }
                double num = Convert.ToDouble(fraction[..index]);
                double den = Convert.ToDouble(fraction[(index + 1)..]);

                if (num.IsDecimal() || den.IsDecimal())
                {
                    var numer = ConvertDecimalToFraction(num);
                    var deno = ConvertDecimalToFraction(den);
                    return numer / deno;
                }
                return new Fraction(num, den);
            }
            catch (DivideByZeroException ex)
            {
                throw new DivideByZeroException();
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
            "\n1) {Numerator:T}/{Denominator:T}\n2) {Numerator}");
    }

    public static explicit operator decimal(Fraction a) => a.Quotient;

    public static explicit operator double(Fraction a) => Convert.ToDouble(a.Quotient);

    public static explicit operator int(Fraction a) => Convert.ToInt32(a.Quotient);

    public static explicit operator string(Fraction a) => a.ToString();


    public TypeCode GetTypeCode()
    {
        return TypeCode.Object;
    }

    public bool ToBoolean(IFormatProvider? provider)
    {
        return Convert.ToBoolean(Quotient);
    }

    public char ToChar(IFormatProvider? provider)
    {
        return Convert.ToChar(Quotient);
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        return Convert.ToSByte(Quotient);
    }

    public byte ToByte(IFormatProvider? provider)
    {
        return Convert.ToByte(Quotient);
    }

    public short ToInt16(IFormatProvider? provider)
    {
        return Convert.ToInt16(Quotient);
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        return Convert.ToUInt16(Quotient);
    }

    public int ToInt32(IFormatProvider? provider)
    {
        return (int)this;
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        return Convert.ToUInt32(Quotient);
    }

    public long ToInt64(IFormatProvider? provider)
    {
        return Convert.ToInt64(Quotient);
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        return Convert.ToUInt64(Quotient);
    }

    public float ToSingle(IFormatProvider? provider)
    {
        return Convert.ToSingle(Quotient);
    }

    public double ToDouble(IFormatProvider? provider)
    {
        return (double)this;
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        return (decimal)this;
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        throw new InvalidCastException("Invalid cast from Fraction to DateTime.");
    }

    public string ToString(IFormatProvider? provider)
    {
        return Quotient.ToString(provider);
    }

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(string)) return this.ToString();
        if (conversionType == typeof(Fraction)) return this;
        return Convert.ChangeType(Quotient, conversionType, provider);
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
        this = info.GetValue<string>(nameof(Fraction));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Fraction), ToString());
    }

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

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        else if (obj is Fraction otherFraction)
        {
            return this == otherFraction;
        }
        else if (obj.IsNumber())
        {
            return Convert.ToDouble(obj) == QuotientDouble;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Numerator.GetHashCode() ^ Denominator.GetHashCode() * 11;
    }

    public bool Equals(Fraction other)
    {
        return this == other;
    }

    public int CompareTo(Fraction other)
    {
        if (this == other)
        {
            return 0;
        }
        return Quotient.CompareTo(other.Quotient);
    }
}