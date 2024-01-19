namespace LinearAlgebra.Classes;

public partial struct SpecialString
{
    public static char[] vari = { 'x', 'y', 'z', 't', 'd', 's', 'h', 'k', 'p', 'v', 'e', 'l', 'a', 'b', 'c', 'f', 'g', 'i', 'j', 'm', 'n', 'o', 'q', 'r', 'u', 'w' };
    public Dictionary<string, Fraction> values = new();
    public SpecialString(string str)
    {
        values.Add(str.ToString(), new Fraction(1));
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
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            matrix[i] = new(vari[i].ToString());
        }
        return matrix;
    }

    public override string ToString()
    {
        string result = "";
        foreach (var item in values)
        {
            if (item.Value.Numerator != 1) result += $"{item.Value} * {item.Key} + ";
            else result += $"{item.Key} + ";
        }
        return result[..^3];
    }
}
