namespace LinearAlgebra.Classes;

public partial struct Fraction : IConvertible
{
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
        return Convert.ChangeType(Quotient, conversionType, provider);
    }

}