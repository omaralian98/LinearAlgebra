namespace LinearAlgebra.Classes;

public partial struct SpecialString : ICoefficient
{
    private static readonly char[] vari = ['x', 'y', 'z', 't', 'd', 's', 'h', 'k', 'p', 'v', 
        'e', 'l', 'a', 'b', 'c', 'f', 'g', 'i', 'j', 'm', 'n', 'o', 'q', 'r', 'u', 'w'];
    public Dictionary<string, Fraction> values = [];

    public SpecialString()
    {
        values = new Dictionary<string, Fraction>();
    }
    public SpecialString(string str)
    {
        values.Add(str.ToString(), new Fraction(1));
    }

    public SpecialString(char str)
    {
        values.Add(str.ToString(), new Fraction(1));
    }

    public void Add(char str)
    {
        values.Add(str.ToString(), new Fraction(1));
    }

    public void Add(string str)
    {
        values.Add(str.ToString(), new Fraction(1));
    }

    public void Add(string str, Fraction fraction)
    {
        values.Add(str, fraction);
    }

    public static Fraction[] Solve(SpecialString[] t, Dictionary<string, Fraction> variablesValue)
    {
        Fraction[] answer = new Fraction[t.Length];
        for (int i = 0; i < t.Length; i++)
        {
            answer[i] = ExpressionHelpers.EvaluateAsFraction(t[i].ToString(), variablesValue);
        }
        return answer;
    }

    public static SpecialString[] GetVariableMatrix(int length)
    {
        SpecialString[] matrix = new SpecialString[length];
        int counter = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            matrix[i] = i < vari.Length ? new(vari[i]) : new($"{vari[i % vari.Length]}{counter}");
            counter += (i + 1) % vari.Length == 0 ? 1 : 0;
        }
        return matrix;
    }

    public override string ToString()
    {
        string result = "";
        foreach (var item in values)
        {
            if (item.Value.Numerator == 0) continue;
            else if (item.Value.Numerator < 0 && result.Length > 0) result += $" - {item.Value.GetAbs()} * {item.Key}";
            else if (item.Value.Numerator < 0) result += $" - {item.Value} * {item.Key}";
            else if (item.Value.Numerator != 1) result += $" + {item.Value} * {item.Key}";
            else result += $" + {item.Key}";
        }
        if (result.Length < 3) return "0";
        return result[3..];
    }
    public string ToDecimalString()
    {
        string result = "";
        foreach (var item in values)
        {
            if (item.Value.Quotient == 0) continue;
            else if (item.Value.Numerator < 0 && result.Length > 0) result += $" - {item.Value.GetDecimalAbs()} * {item.Key}";
            else if (item.Value.Numerator < 0) result += $" - {item.Value.Quotient} * {item.Key}";
            else if (item.Value.Numerator != 1) result += $" + {item.Value.Quotient} * {item.Key}";
            else result += $" + {item.Key}";
        }
        if (result.Length < 3) return "0";
        return result[3..];
    }

}
