using Xunit.Abstractions;

namespace Unit.REF.Parameters;

public record REFOfMatrixWithCoefficientShouldThrowExceptionParameter<TMatrix, TCoefficient> : REFOfMatrixShouldThrowExceptionParameter<TMatrix>, IXunitSerializable
{
    public TCoefficient[] Coefficient { get; protected set; }

    public REFOfMatrixWithCoefficientShouldThrowExceptionParameter()
    {
        // Parameterless constructor required by IXunitSerializable
    }

    public REFOfMatrixWithCoefficientShouldThrowExceptionParameter(TMatrix[,] matrix, TCoefficient[] coefficient)
        : this(matrix, coefficient, typeof(Exception))
    {
    }

    public REFOfMatrixWithCoefficientShouldThrowExceptionParameter(TMatrix[,] matrix, TCoefficient[] coefficient, Type exceptionType)
        : base(matrix, exceptionType)
    {
        Coefficient = coefficient;
    }

    public new void Deserialize(IXunitSerializationInfo info)
    {
        Coefficient = info.GetValue<TCoefficient[]>(nameof(Coefficient));
        base.Deserialize(info);
    }

    public new void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Coefficient), Coefficient);
        base.Serialize(info);
    }

    public override string ToString()
    {
        return $"{nameof(Matrix)}: {Matrix.ConvertTo1D().ConvertToString()}, {nameof(Coefficient)}: {Coefficient.ConvertToString()}, {nameof(Exception)}: {Exception.Name}";
    }
}