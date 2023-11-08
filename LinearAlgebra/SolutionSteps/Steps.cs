namespace LinearAlgebra;
public partial class Linear
{
    public struct MatrixStep
    {
        public string? StepDescription { get; set; }
        public Fraction[,]? Matrix { get; set; }
    }
    public struct Steps
    {
        public int PivotRow { get; set; }
        public int EffectedRow { get; set; }
        public Fraction? Scalar { get; set; } = null;
        public Steps(int pivotRow, int effectedRow)
        {
            PivotRow = pivotRow;
            EffectedRow = effectedRow;
        }
        public Steps(int pivotRow, int effectedRow, Fraction scalar)
        {
            PivotRow = pivotRow;
            EffectedRow = effectedRow;
            Scalar = scalar;
        }
    }
}