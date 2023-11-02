using LinearAlgebra;
namespace Mr_Sure21
{
    class Program
    {
        public static void Main(string[] args)
        {
            decimal[,] matrix = { { 5, 1, 2 }, { 1, 3, 7 }, { 2, 7, 6 } };
            var test = Linear.REF(matrix);
            foreach (var it in Linear.steps)
            {
                Console.WriteLine(it.StepDescription);
                it.Matrix?.Print();
                Console.WriteLine("");
            }
        }
    }
}