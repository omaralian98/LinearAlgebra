namespace LinearAlgebra.Classes;

public record DeterminantResult
{
    public Fraction[,] Matrix { get; set; } = new Fraction[0, 0];
    public Fraction Value { get; set; } = new Fraction(0);
    public Fraction Scalar { get; set; } = new Fraction(1);
    public DeterminantResult[] MatrixSteps { get; set; } = [];

    public List<DeterminantResult> GetAllChildren()
    {
        List<DeterminantResult> allChildren = [this];

        if (MatrixSteps is not null && MatrixSteps.Length > 0)
        {
            foreach (var childMatrixStep in MatrixSteps)
            {
                allChildren.AddRange(childMatrixStep.GetAllChildren());
            }
        }
        return allChildren;
    }
}
