using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using LinearAlgebra;
using LinearAlgebra.Classes;
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
                fractions.Add(Fraction.GenerateRandomMatrix(4, 4, simplify: false));
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
            Fraction[,] matrix =
            {
                { 3, -7, 5, 3, 1, -9 },
                { 4, 5, 1, 5, 3, 4 }
            };
            //var res = Linear.LU<Fraction, float, decimal>(matrix);
            //Console.WriteLine(res.Description);
            //Console.WriteLine((res.U, res.L).GetMatrix());
            matrix.GetTMatrix<Fraction>();
            //Console.WriteLine(Linear.Add<Fraction, Fraction>(fractions[0], fractions[1]).GetMatrix());
            //Console.WriteLine(Linear.Add<decimal, Fraction>(fractions[0], fractions[1]).GetMatrix());
            //Console.WriteLine(Linear.Add<double, Fraction>(fractions[0], fractions[1]).GetMatrix());
            //Console.WriteLine(Linear.Add<bool, Fraction>(fractions[0], fractions[1]).GetMatrix());
        }
    }
}