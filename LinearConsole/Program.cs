using LinearAlgebra;
using static LinearAlgebra.Linear;
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
            string[] coe = { "x", "y", "z" };
            Dictionary<string, Fraction> values = new()
            {
                { "x", new Fraction(2)},
                { "y", new Fraction(5)},
                { "z", new Fraction(-8)},
            };
            Console.WriteLine((matrix, coe).GetMatrix());
            (var top, var topco) = Linear.REFAsSpecialString(matrix.GetFractions());
            Console.WriteLine((top, topco).GetMatrix());
            Console.WriteLine(SpecialString.Solve(topco, values).GetMatrix());
            decimal[] coe2 = { 2, 5, -8 };
            (var top1, var topco1) = Linear.REFAsFraction(matrix.GetFractions(), coe2.GetFractions());
            Console.WriteLine((top1, topco1).GetMatrix());
            //foreach (var it in topco)
            //{
            //    Console.WriteLine(string.Join("", ExpressionHelpers.InfixToPostfix(it.ToString())));
            //    Console.WriteLine(it);
            //}
        }
    }
}