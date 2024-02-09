using MathNet.Numerics;

namespace LinearAlgebra;

public partial class Linear
{
    private partial class DeterminantClass
    {
        public static IEnumerable<REF_Result> DeterminantUsingREF(Fraction[,] matrix, out Fraction determinant, bool solution = false)
        {
            var refSteps = Row_Echelon_Form.REF(matrix, solution: true);
            List<REF_Result> steps = [];
            int opt = 0;
            string description = "";
            determinant = new(1);
            foreach (var step in refSteps)
            {
                if (step.Description.Contains("Swap")) opt++;
                if (solution) steps.Add(step);
            }
            var result = solution ?  steps[^1] : new REF_Result()
            {
                Matrix = matrix,
                Description = description
            };
            for (int i = 0; i < result.Matrix.GetLength(0); i++)
            {
                determinant *= result.Matrix[i, i];
                description += $" {result.Matrix[i, i]} *";
            }
            determinant *= opt.IsEven() ? 1 : -1;
            description = description[1..];
            description += $" (-1)^{opt} = {determinant}";
            result.Description += solution ? $"\n{description}": description;
            steps.Add(result);
            return steps;
        }
    }
}
