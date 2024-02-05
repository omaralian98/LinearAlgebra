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
                { 0, 0, 3, 1 },
                { 0, 0, 0, 1 },
                { 1, 9, 7, 2 },
                { 0, 1, 3, 3 },
            };
            Fraction[,] randomMatrix = Fraction.GenerateRandomMatrix(4, 4);
        }
    }
}