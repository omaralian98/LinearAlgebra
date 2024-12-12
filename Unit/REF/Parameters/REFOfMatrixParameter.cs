using Xunit.Abstractions;

namespace Unit.REF.Parameters;

public record REFOfMatrixParameter<TMatrix, RMatrix> : IXunitSerializable
{
    public TMatrix[,] Matrix { get; protected set; }

    public RMatrix[,] ExpectedMatrix { get; protected set; }


    public REFOfMatrixParameter()
    {
        // Parameterless constructor required by IXunitSerializable
    }

    public REFOfMatrixParameter(TMatrix[,] matrix, RMatrix[,] expectedMatrix)
    {
        Matrix = matrix;
        ExpectedMatrix = expectedMatrix;
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
        Matrix = info.GetValue<TMatrix[,]>(nameof(Matrix));
        ExpectedMatrix = info.GetValue<RMatrix[,]>(nameof(ExpectedMatrix));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Matrix), Matrix);
        info.AddValue(nameof(ExpectedMatrix), ExpectedMatrix);
    }

    public override string ToString()
    {
        return $"{nameof(Matrix)}: {Matrix.ConvertTo1D().ConvertToString()}, {nameof(ExpectedMatrix)}: {ExpectedMatrix.ConvertTo1D().ConvertToString()}";
    }
}