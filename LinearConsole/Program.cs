using LinearAlgebra;
using LinearAlgebra.Classes;
using Microsoft.Diagnostics.Runtime.Utilities;
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
            string[] coe = ["x", "y", "z", "t"];
            decimal[] test = [1, 2, 3, 4, 5, 6, 7, 8];
            //Dictionary<string, Fraction> values = new()
            //{
            //    { "x", new Fraction(1)},
            //    { "y", new Fraction(1)},
            //    { "z", new Fraction(1)},
            //};
            //Console.WriteLine((matrix, coe).GetMatrix());
            //decimal[] core = [1, 1, 1];
            //var res = Linear.Row_Echelon_Form.REF(matrix.GetFractions(), SpecialString.GetVariableMatrix(4), true);
            //foreach (var step in res.GetAllChildren())
            //{
            //    Console.WriteLine(step);
            //}
            var rete = Linear.DeterminantWithResultUsingREF(matrix.GetFractions());
            foreach (var item in rete.Item2)
            {
                Console.WriteLine(item);
            }
        }
    }
}