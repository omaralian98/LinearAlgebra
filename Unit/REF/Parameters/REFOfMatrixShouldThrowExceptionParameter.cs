using Xunit.Abstractions;

namespace Unit.REF.Parameters;

public record REFOfMatrixShouldThrowExceptionParameter<TMatrix> : IXunitSerializable
{
    public TMatrix[,] Matrix { get; protected set; }
    public Type Exception { get; protected set; }

    public REFOfMatrixShouldThrowExceptionParameter()
    {
        // Parameterless constructor required by IXunitSerializable
    }

    public REFOfMatrixShouldThrowExceptionParameter(TMatrix[,] matrix)
        : this(matrix, typeof(Exception))
    {
    }

    public REFOfMatrixShouldThrowExceptionParameter(TMatrix[,] matrix, Type exceptionType)
    {
        Matrix = matrix;
        Exception = exceptionType;
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
        Matrix = info.GetValue<TMatrix[,]>(nameof(Matrix));
        Exception = info.GetValue<Type>(nameof(Exception));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Matrix), Matrix);
        info.AddValue(nameof(Exception), Exception);
    }

    public override string ToString()
    {
        return $"{nameof(Matrix)}: {Matrix.ConvertTo1D().ConvertToString()}, {nameof(Exception)}: {Exception.Name}";
    }
}