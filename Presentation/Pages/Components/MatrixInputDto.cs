using LinearAlgebra;

namespace Presentation.Pages.Components;

public record class MatrixInputDto
{
    public MatrixInputDto()
    {
    }   
    
    public MatrixInputDto(Fraction scalar)
    {
        Scalar = scalar;
    }

    public MatrixInputDto(Fraction[,] matrix) 
    {
        Matrix = matrix;
    }

    public MatrixInputDto(Fraction[] coefficient)
    {
        Coefficient = coefficient;
    }

    public MatrixInputDto(Fraction[,] matrix, Fraction[] coefficient)
    {
        Matrix = matrix;
        Coefficient = coefficient;
    }

    public Fraction? Scalar { get; private set; } = null;
    public Fraction[,]? Matrix { get; private set; } = null;
    public Fraction[]? Coefficient { get; private set; } = null;

    public static MatrixInputDto FormScalar(Fraction[,] matrix)
    {
        return new MatrixInputDto(matrix[0, 0]);
    }

    public static MatrixInputDto FormMatrix(Fraction[,] matrix)
    {
        return new MatrixInputDto(matrix);
    }

    public static MatrixInputDto FormAugmentedMatrix(Fraction[,] matrix)
    {
        (var slicedMatrix, var coefficient) = matrix.SliceColumn(matrix.GetLength(1) - 1); 
        return new MatrixInputDto(slicedMatrix, coefficient);
    }
}