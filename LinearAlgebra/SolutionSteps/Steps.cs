namespace LinearAlgebra;
public partial class Linear
{
    public enum Operations
    {
        Swap,
        Add,
        Multiply
    }
    public struct Steps
    {
        public int PivotRow { get; set; }
        public int EffectedRow { get; set; }
        public Fraction? Scalar { get; set; } = null;
        public Operations Operation { get; set; }
        public Steps(int pivotRow, int effectedRow, Operations operation)
        {
            PivotRow = pivotRow;
            EffectedRow = effectedRow;
            Operation = operation;
        }
        public Steps(int piviotRow, int effectedRow, Operations operation, Fraction scalar)
        {
            PivotRow = piviotRow;
            EffectedRow = effectedRow;
            Operation = operation;
            Scalar = scalar;
        }
    }
}