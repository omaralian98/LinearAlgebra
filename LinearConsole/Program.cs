using LinearAlgebra;
using LinearAlgebra.Classes;
using Newtonsoft.Json.Linq;
using static LinearAlgebra.Linear;
namespace Mr_Sure21
{

    public class Program
    {
        public static void Main()
        {
            decimal[,] matrix =
            {
                { 0, 0, 3, 1 },
                { 0, 0, 0, 1 },
                { 1, 9, 7, 2 },
                { 0, 1, 3, 3 },
            };

            var mat = Fraction.GenerateRandomMatrix(4, 4, IntegersOnly: true);
            var res = Linear.DeterminantWithResultUsingREF(mat, out Fraction determinant);
            foreach (var step in res)
            {
                Console.WriteLine(step.Description);
                Console.WriteLine(step.Matrix.GetMatrix());
            }

        }
    }
}