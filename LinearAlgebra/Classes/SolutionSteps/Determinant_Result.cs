namespace LinearAlgebra.Classes;

public record Determinant_Result<T>
{
    public T[,] Matrix { get; set; } = new T[0, 0];
    public Fraction Value { get; set; } = new Fraction(0);
    public Fraction Scalar { get; set; } = new Fraction(1);
    public Determinant_Result<T>[] MatrixSteps { get; set; } = [];

    public List<Determinant_Result<T>> GetAllChildren()
    {
        List<Determinant_Result<T>> allChildren = [this];
        foreach (var childMatrixStep in MatrixSteps)
        {
            allChildren.AddRange(childMatrixStep.GetAllChildren());
        }
        return allChildren;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(Value.ToString());
        stringBuilder.AppendLine(Matrix.GetDeterminantMatrix());
        return stringBuilder.ToString();
    }
}
