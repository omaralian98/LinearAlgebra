using LinearAlgebra;
using LinearAlgebra.Classes;
namespace Mr_Sure21
{

    public class Program
    {
        public static void Main()
        {
            decimal[,] matrix =
            {
                { 1, 1 },
                { 1, 0 },
                { 1, -1 }
            };
            string[] coe = ["x", "y", "z"];
            Dictionary<string, Fraction> values = new()
            {
                { "x", new Fraction(2)},
                { "y", new Fraction(5)},
                { "z", new Fraction(-8)},
            };
            Console.WriteLine((matrix, coe).GetMatrix());
            var res = Linear.REF(matrix.GetFractions(), true);
            decimal[] core = { 1, 1, 1 };
            var coeff = Linear.GetCoefficient(SpecialString.GetVariableMatrix(3), res.Steps);
            foreach (var step in res.MatrixSteps)
            {
                Console.WriteLine(step.Description);
                Console.WriteLine(step.Matrix?.GetMatrix());
            }
            Console.WriteLine((res.Matrix, coeff).GetMatrix());
        }
    }
}