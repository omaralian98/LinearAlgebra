namespace LinearAlgebra;

public partial class Linear
{
    internal partial class DeterminantClass
    {
        public static (Fraction, REFResult) DeterminantUsingREF(Fraction[,] matrix, bool solution = false)
        {
            var result = Row_Echelon_Form.REF(matrix, true);
            int opt = 0;
            string description = "";
            Fraction determinant = new(1);
            var step = result.GetAllChildren();
            for (int i = 0; i < step.Count; i++)
            {
                if (step[i].Description.Contains("Swap")) opt++;
                if (i + 1 == step.Count)
                {
                    for (int j = 0; j < step[i].Matrix.GetLength(0); j++)
                    {
                        determinant *= step[i].Matrix[j, j];
                        description += $" {step[i].Matrix[j, j]} *";
                    }
                    determinant *= opt % 2 == 0 ? 1 : -1;
                    description = description[1..];
                    description += $" (-1)^{opt} = {determinant}";
                    step[i].Description += $"\n{description}";
                }
            }
            if (solution)
            {
                return (determinant, result);
            }
            else
            {
                return (determinant, new REFResult()
                {
                    Matrix = matrix,
                    Description = description
                });
            }
        }
    }
}
