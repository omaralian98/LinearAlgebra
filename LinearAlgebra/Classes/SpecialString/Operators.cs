namespace LinearAlgebra.Classes;

public partial struct SpecialString
{
    public static SpecialString operator +(SpecialString a, SpecialString b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (var item in b.values)
        {
            if (a.values.ContainsKey(item.Key))
            {
                newValues[item.Key] = a.values[item.Key] + item.Value;
            }
            else
            {
                newValues[item.Key] = item.Value;
            }
        }
        return new SpecialString { values = newValues };
    }

    public static SpecialString operator -(SpecialString a, SpecialString b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (var item in b.values)
        {
            if (newValues.ContainsKey(item.Key))
            {
                newValues[item.Key] -= item.Value;
            }
            else
            {
                newValues[item.Key] = -item.Value;
            }
        }
        return new SpecialString { values = newValues };
    }

    public static SpecialString operator *(SpecialString a, SpecialString b)
    {
        Dictionary<string, Fraction> newValues = new();
        foreach (var item in a.values)
        {
            foreach (var item1 in b.values)
            {
                newValues.Add($"{item.Key}{item1.Key}", item.Value * item1.Value);
            }
        }
        return new SpecialString { values = newValues };
    }

    public static SpecialString operator /(SpecialString a, SpecialString b)
    {
        Dictionary<string, Fraction> newValues = new();
        foreach (var item in a.values)
        {
            foreach (var item1 in b.values)
            {
                newValues.Add($"{item.Key}{item1.Key}", item.Value / item1.Value);
            }
        }
        return new SpecialString { values = newValues };
    }

    public static SpecialString operator +(SpecialString a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value + b);
        return new SpecialString { values = newValues };
    }

    public static SpecialString operator -(SpecialString a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value - b);
        return new SpecialString { values = newValues };
    }

    public static SpecialString operator *(SpecialString a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value * b);
        return new SpecialString { values = newValues };
    }

    public static SpecialString operator /(SpecialString a, Fraction b)
    {
        Dictionary<string, Fraction> newValues = a.values
            .ToDictionary(entry => entry.Key, entry => entry.Value / b);
        return new SpecialString { values = newValues };
    }
}