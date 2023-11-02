using LinearAlgebra;
namespace Mr_Sure21
{
    class Program
    {
        public static void Main(string[] args)
        {
            decimal[,] matrix = { { 1, 0, 3, 7, 2 }, { 4, 6, 9, 5, 4 }, { 4, 9, 7, 6, 0 }, { 6, 6, 7, 5, 5 }, { 2, 4, 9, 7, 1 } };
            matrix.Print();
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