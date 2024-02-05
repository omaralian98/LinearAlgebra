namespace LinearAlgebra.Classes;

public record Determinant_Result
{
    public Fraction[,] Matrix { get; set; } = new Fraction[0, 0];
    public Fraction Value { get; set; } = new Fraction(0);
    public Fraction Scalar { get; set; } = new Fraction(1);
    public Determinant_Result[] MatrixSteps { get; set; } = [];

    public List<Determinant_Result> GetAllChildren()
    {
        List<Determinant_Result> allChildren = [this];

        if (MatrixSteps is not null && MatrixSteps.Length > 0)
        {
            foreach (var childMatrixStep in MatrixSteps)
            {
                allChildren.AddRange(childMatrixStep.GetAllChildren());
            }
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
