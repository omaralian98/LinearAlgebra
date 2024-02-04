namespace LinearAlgebra.Classes;

public record REFResult<T> : REFResult where T : ICoefficient
{
    public T[] Coefficient { get; set; } = [];
    public new REFResult<T>? NextStep { get; set; } = null;
    public new List<REFResult<T>> GetAllChildren()
    {
        List<REFResult<T>> allChildren = [];
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
public record REFResult
{
    public Fraction[,] Matrix { get; set; } = new Fraction[0, 0];
    public string Description { get; set; } = "";
    public REFResult? NextStep { get; set; } = null;
    public virtual List<REFResult> GetAllChildren()
    {
        List<REFResult> allChildren = [];
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
