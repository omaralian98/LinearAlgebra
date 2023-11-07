using LinearAlgebra;
using static LinearAlgebra.Linear;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;
namespace Mr_Sure21
{
    class Program
    {
        public static void Main(string[] args)
        {
            decimal[,] matrix = { { 1, 1, 2 }, { 1, 3, 7 }, { 2, 6, 6 } };
            decimal[] coefficient = { 1, 1, 1 };
            var top = Linear.GetREF(matrix.GetFractions(), false, true);
            foreach (var it in top.Item2)
            {
                Console.WriteLine($"{it.Scalar}R{it.PivotRow + 1} + R{it.EffectedRow + 1} ---> R{it.EffectedRow + 1}");
            }
            var top1 = Linear.REFAsFraction(matrix.GetFractions(), coefficient.GetFractions());
            top1.Print();

        }
    }
}