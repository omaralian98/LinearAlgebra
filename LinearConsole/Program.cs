using LinearAlgebra;
namespace Mr_Sure21
{
    class Program
    {
        public static void Main(string[] args)
        {
            decimal[,] matrix = { { 1, 4, 1, 5 }, { 1, 4, 0, 5 }, { 2, 8, 7, 6 } };
            var test = Linear.REF(matrix);
            test.PrintMatrix();
        }
    }
}