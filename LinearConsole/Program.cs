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
                { 4, 6, 9, 1 },
                { 4, 9, 7, 2 },
                { 1, 1, 3, 3 },

                { 1, 0, 3, 1 },
                { -1, 6, 9, 1 },
                { 5, 9, 7, 2 },
                { -5, 1, 3, 3 }
            };
            string[] coe = ["x", "y", "z", "t"];
            Dictionary<string, Fraction> values = new()
            {
                { "x", new Fraction(1)},
                { "y", new Fraction(1)},
                { "z", new Fraction(1)},
            };
            Console.WriteLine((matrix, coe).GetMatrix());
            decimal[] core = [1, 1, 1];
            var res = Linear.RREF(matrix.GetFractions(), SpecialString.GetVariableMatrix(8));
            //var test = new REFResult { Matrix = matrix.GetFractions() };
            //var tope1 = matrix.GetFractions();
            //Linear.ClearPivotColumn(tope1, 0, 0, reduced: false, test);
            //Linear.ClearPivotColumn(tope1, 1, 1, reduced: false, test.GetAllChildren().Last());
            //Console.WriteLine($"Matrix before: \n{test}");
            //Console.WriteLine($"Nextmatrix before: \n{test.NextStep}");
            //Console.WriteLine($"Nextmatrix Nextmatrix before: \n{test.NextStep?.NextStep}");
            //foreach (var item in test.GetAllChildren())
            //{
            //    Console.WriteLine(item.Description);
            //    Console.WriteLine(item.Matrix.GetMatrix());
            //}
            foreach (var step in res.GetAllChildren())
            {
                Console.WriteLine(step);
                //Console.WriteLine(step.Description);
                //Console.WriteLine(step.Matrix.GetMatrix());
                //if (step.Matrix is not null && step.Coefficient is not null)
                //{
                //    Console.WriteLine((step.Matrix, step.Coefficient).GetMatrix());
                //}
            }

            //var result = Linear.Determinant(matrix.GetFractions()).GetAllChildren();
            //foreach (var item in result)
            //{
            //    Console.WriteLine($"Value of: {item.Value}\n{item.Matrix.GetDeterminantMatrix()}");
            //    foreach (var item2 in item.NextStep)
            //    {
            //        Console.WriteLine($"Scalar: {item2.Scalar}");
            //        Console.WriteLine(item2.Matrix.GetDeterminantMatrix());
            //    }
            //}
            //Console.WriteLine(SpecialString.Solve(res.Coefficient, values).GetMatrix());
            //Console.WriteLine(matrix.GetMatrix());
            //Console.WriteLine(matrix.GetDeterminantMatrix());

        }
    }
}