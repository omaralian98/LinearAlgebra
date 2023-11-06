using LinearAlgebra;
using static LinearAlgebra.Linear;
namespace Mr_Sure21
{
    class Program
    {
        public static void Test<T>(T top)
        {

        }
        public static void Main(string[] args)
        {
            decimal[,] matrix = { { 1, 1, 2 }, { 1, 3, 7 }, { 2, 6, 6 } };
            decimal[] coefficient = { 1, 1, 1 };
            (matrix, coefficient).Print();
            var coe1 = Linear.REF(matrix, coefficient);
            foreach (var it in Linear.steps)
            {
                Console.WriteLine(it.StepDescription);
                if (it.Matrix is not null && it.Coefficient is not null)
                {
                    (it.Matrix, SpecialString.Solve(it.Coefficient)).Print();
                }
                Console.WriteLine("");
            }
            coe1.Print();
        }
    }
}