using LinearAlgebra;
namespace Mr_Sure21
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[,] matrix =
            {
                { "10/2", "1", "2" },
                { "1", "3", "7" },
                { "2", "7", "6" }
            };
            var test = Linear.REFAsString(matrix);
            test.PrintMatrix();
        }
    }
}