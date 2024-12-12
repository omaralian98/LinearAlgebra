using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using LinearAlgebra;
using LinearAlgebra.Classes;
using LinearAlgebra.Classes.Enums;
using MathNet.Numerics.LinearAlgebra.Complex;
namespace Mr_Sure21
{
    public class Program
    {
        public static void Main()
        {
            List<Fraction[,]> fractions = new List<Fraction[,]>();
            int sample = 1000;
            for (int i = 0; i < sample; i++)
            {
                fractions.Add(Fraction.GenerateRandomMatrix(4, 4, RandomFractionGenerationType.Simplified));
            }
            ////long startTime1 = Stopwatch.GetTimestamp();
            ////REF(fractions);
            ////TimeSpan elapsedTime1 = Stopwatch.GetElapsedTime(startTime1);

            ////long startTime = Stopwatch.GetTimestamp();
            ////OMAR(fractions);
            ////TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);
            ////Console.WriteLine("Total of {0} matrices", sample);
            ////Console.WriteLine("OMAR Took: {0}", elapsedTime.TotalSeconds);
            ////Console.WriteLine("REF Took:  {0}", elapsedTime1.TotalSeconds);
            ////Fraction[][,] mat = [.. fractions]; 
            ////long startTime1 = Stopwatch.GetTimestamp();
            ////var result = Linear.MultiplyAsFraction(mat);
            ////TimeSpan elapsedTime1 = Stopwatch.GetElapsedTime(startTime1);
            ////Console.WriteLine("Took: {0}", elapsedTime1.TotalSeconds);
            ////Console.WriteLine(result.GetMatrix());
            var matrix = new string[,] { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            //var res = Linear.LU<Fraction, float, decimal>(matrix);
            //Console.WriteLine(res.Description);
            //Console.WriteLine((res.U, res.L).GetMatrix());
            var res = Linear.REFWithResult<Fraction, Fraction, string, int>(matrix, [1, 1, 1]);
            foreach (var item in res)
            {
                (item.Matrix, item.Coefficient).Print();
            }
            //Console.WriteLine(Linear.Add<Fraction, Fraction>(fractions[0], fractions[1]).GetMatrix());
            //Console.WriteLine(Linear.Add<decimal, Fraction>(fractions[0], fractions[1]).GetMatrix());
            //Console.WriteLine(Linear.Add<double, Fraction>(fractions[0], fractions[1]).GetMatrix());
            //Console.WriteLine(Linear.Add<bool, Fraction>(fractions[0], fractions[1]).GetMatrix());
        }
    }
}