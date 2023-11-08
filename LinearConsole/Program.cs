using LinearAlgebra;
using static LinearAlgebra.Linear;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Numerics;
using MathNet.Numerics;

namespace Mr_Sure21
{
    
    [MemoryDiagnoser]
    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<Program>();
        }
        [Benchmark]
        public void Test()
        {
            decimal[,] matrix = { { 1, 0, 3, 7, 2 , 1, 1, 1 }, { 4, 6, 9, 5, 4, 1, 1, 1 }, { 4, 9, 7, 6, 0, 1, 1, 1 }, { 6, 6, 7, 5, 5, 1, 1, 1 }, { 2, 4, 9, 7, 1, 1, 1, 1 }, { 2, 4, 9, 7, 1, 1, 1, 1 }, { 2, 4, 9, 7, 1, 1, 1, 1 }, { 2, 4, 9, 7, 1, 1, 1, 1 } };
            var top = Linear.GetREF(matrix.GetFractions(), true, true);
        }
    }
}