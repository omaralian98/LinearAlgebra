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
            decimal[,] matr =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
            };

            decimal[,] matr2 =
            {
                { 7, 8 },
                { 9, 10 },
                { 11, 12 },
            };

            Console.WriteLine(Linear.Multiplication.Multiply(matr.GetFractions(), matr2.GetFractions()).GetMatrix());
        }
    }
}