namespace LinearAlgebra;

public partial class Linear
{
    private static void CheckCoherenceForDet<T>(T[,] matrix)
    {
        string errorMessage = $"Non-square matrices do not have determinants.";
        if (matrix.GetLength(0) != matrix.GetLength(1)) throw new ArgumentException(errorMessage);
    }

    public static DeterminantResult Determinant(Fraction[,] matrix)
    {
        CheckCoherenceForDet(matrix);
        Fraction answer = new(0);
        int size = matrix.GetLength(0);      
        if (size == 1)
        {
            answer += matrix[0, 0];
            return new DeterminantResult { Matrix = matrix, Value = answer };
        }
        else if (size >= 2)
        {
            DeterminantResult[] matrixSteps = new DeterminantResult[size];
            for (int i = 0; i < size; i++)
            {
                var errasedMatrix = Erase(0, i, matrix);
                Fraction scalar = i % 2 == 0 ? matrix[0, i] : -matrix[0, i];
                var det = Determinant(errasedMatrix);
                answer += (scalar * det.Value);
                matrixSteps[i] = det with { Scalar = scalar};
            }
            return new DeterminantResult { Value = answer, MatrixSteps = matrixSteps, Matrix = matrix };
        }
        
        throw new NotImplementedException();
    }

    private static T[,] Erase<T>(int x, int y, T[,] matrix)
    {
        int size = matrix.GetLength(0), p = 0, k = 0;
        T[,] erasedMatrix = new T[size - 1, size - 1];
        for (int i = 0; i < size; i++)
        {
            bool skip = false;
            for (int j = 0; j < size; j++)
            {
                if (i == x)
                {
                    skip = true;
                }
                else if (j == y) continue;
                else
                {
                    erasedMatrix[p, k] = matrix[i, j];
                    Reset(ref p, ref k, size - 1);
                }
            }
            if (skip) continue;
        }
        return erasedMatrix;
    }

    private static void Reset(ref int p, ref int k, int r)
    {
        if (k + 1 != r) k++;
        else { p++; k = 0; }
    }
}
