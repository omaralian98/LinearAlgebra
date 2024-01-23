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
                { 1, 0, 3, 7, 2 , 1, 1, 1 },
                { 4, 6, 9, 5, 4, 1, 1, 1 },
                { 4, 9, 7, 6, 0, 1, 1, 1 },
            };
            string[] coe = ["x", "y", "z"];
            Dictionary<string, Fraction> values = new()
            {
                { "x", new Fraction(2)},
                { "y", new Fraction(5)},
                { "z", new Fraction(-8)},
            };
            Console.WriteLine((matrix, coe).GetMatrix());
            decimal[] core = [1, 1, 1];
            var res = Linear.RREF(matrix.GetFractions(), SpecialString.GetVariableMatrix(3), true);
            foreach (var step in res.MatrixSteps)
            {
                Console.WriteLine(step.Description);
                //Console.WriteLine(step.Matrix?.GetMatrix());
                if (step.Matrix is not null && step.Coefficient is not null)
                {
                    Console.WriteLine((step.Matrix, step.Coefficient).GetMatrix());
                }
            }
            //Console.WriteLine(matrix.GetMatrix());
            //Console.WriteLine(matrix.GetDeterminantMatrix());
        }
    }
}