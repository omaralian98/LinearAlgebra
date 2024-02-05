namespace LinearAlgebra;

public partial class Linear
{
    public partial class Addition_And_Subtraction
    {
        public static Addition_And_Subtraction_Result Add(Fraction[,] a, Fraction[,] b, bool solution = false) => 
            Operation(a, b, operation: '+', solution: solution);

        public static Addition_And_Subtraction_Result Subtract(Fraction[,] a, Fraction[,] b, bool solution = false) => 
            Operation(a, b, operation: '-', solution: solution);

        private static Addition_And_Subtraction_Result Operation(Fraction[,] a, Fraction[,] b, char operation, bool solution = false)
        {
            int row = a.GetLength(0);
            int column = a.GetLength(1);
            Fraction[,] result = new Fraction[row, column];
            string[,]? step = solution ? new string[row, column] : null;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    result[i, j] = operation == '+' ? a[i, j] + b[i, j] : a[i, j] - b[i, j];
                    if (step is not null)
                    {
                        char currentOp = operation;
                        if (b[i, j].Quotient < 0) operation = operation == '+' ? '-' : '+';
                        step[i, j] = $"({a[i,j]} {currentOp} {b[i, j].GetAbs()})";
                    }
                }
            }
            return new Addition_And_Subtraction_Result
            {
                A = a,
                B = b,
                Operation = operation,
                Step = step,
                Result = result
            };
        }
    }
}
