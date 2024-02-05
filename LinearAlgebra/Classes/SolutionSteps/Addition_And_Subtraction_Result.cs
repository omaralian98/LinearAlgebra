namespace LinearAlgebra.Classes;
public record Addition_And_Subtraction_Result
{
    public Fraction[,] A { get; set; } = new Fraction[0, 0];
    public char Operation { get; set; }
    public Fraction[,] B { get; set; } = new Fraction[0, 0];
    public string[,]? Step { get; set; }
    public Fraction[,] Result { get; set; } = new Fraction[0, 0];

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(A.GetMatrix());
        stringBuilder.AppendLine($"{Operation}\n");
        stringBuilder.AppendLine(B.GetMatrix());
        stringBuilder.AppendLine(Step?.GetMatrix());
        stringBuilder.AppendLine(Result.GetMatrix());
        return stringBuilder.ToString();
    }
}