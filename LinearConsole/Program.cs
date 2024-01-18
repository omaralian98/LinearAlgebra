using LinearAlgebra;
using static LinearAlgebra.Linear;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Numerics;
using MathNet.Numerics;
using System.Linq.Expressions;

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
            decimal[] coe = { 1, 1, 1 };
            (var top, var topco) = Linear.REFAsSpecialString(matrix.GetFractions(), coe.GetFractions());
            Console.WriteLine((top, topco).GetMatrix());
            foreach (var it in topco)
            { 
                //Console.WriteLine(string.Join("", ExpressionHelpers.InfixToPostfix(it.str)));
            }
            Special test = new(new Fraction(-3, 2), "x");
            Special test2 = new(new Fraction(1, 4), "y");
            Special test4 = new Special(new Fraction(2), "x");
            var test3 = test + test2;
            Console.WriteLine($"({test}) + ({test2}) = ({test3})\n");
            var test5 = test3 - test4;
            Console.WriteLine($"({test3}) - ({test4}) = ({test5})\n");
            var frac = new Fraction(-3, 4);
            Console.WriteLine($"({frac}) * ({test5}) = ({test5 * frac})\n");
            Console.WriteLine($"({frac}) / ({test5}) = ({test5 / frac})\n");
            Console.WriteLine($"({frac}) + ({test5}) = ({test5 + frac})\n");
            Console.WriteLine($"({frac}) - ({test5}) = ({test5 - frac})\n");
        }
    }
}