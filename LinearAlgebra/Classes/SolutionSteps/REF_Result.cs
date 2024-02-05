namespace LinearAlgebra.Classes;

public record REF_Result<T> : REF_Result where T : ICoefficient
{
    public T[] Coefficient { get; set; } = [];
    public new REF_Result<T>? NextStep { get; set; } = null;
    public new List<REF_Result<T>> GetAllChildren()
    {
        List<REF_Result<T>> allChildren = [];
        var temp = this;
        while (temp is not null)
        {
            allChildren.Add(temp);
            temp = temp.NextStep;
        }
        return allChildren;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(Description);
        stringBuilder.AppendLine((Matrix, Coefficient).GetMatrix());
        return stringBuilder.ToString();
    }
}
public record REF_Result
{
    public Fraction[,] Matrix { get; set; } = new Fraction[0, 0];
    public string Description { get; set; } = "";
    public REF_Result? NextStep { get; set; } = null;
    public virtual List<REF_Result> GetAllChildren()
    {
        List<REF_Result> allChildren = [];
        var temp = this;
        while (temp is not null)
        {
            allChildren.Add(temp);
            temp = temp.NextStep;
        }
        return allChildren;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(Description);
        stringBuilder.AppendLine(Matrix.GetMatrix());
        return stringBuilder.ToString();
    }
}
