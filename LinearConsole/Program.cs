using LinearAlgebra;
namespace Mr_Sure21
{
    class Program
    {
        public static void Main(string[] args)
        {
            decimal[,] matrix =
            {
                { 2, 2, 1, 7 },
                { 7, 0, 9, 7 }
            };
            var test = Linear.REFAsString(matrix);
            test.PrintMatrix();
        }
    }
}