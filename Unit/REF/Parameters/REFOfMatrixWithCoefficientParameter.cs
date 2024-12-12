using Xunit.Abstractions;

namespace Unit.REF.Parameters;

public record REFOfMatrixWithCoefficientParameter<TMatrix, TCoefficient, RMatrix, RCoefficient> : REFOfMatrixParameter<TMatrix, RMatrix>, IXunitSerializable
{
    public TCoefficient[] Coefficient { get; protected set; }

    public RCoefficient[] ExpectedCoefficient { get; protected set; }


    public REFOfMatrixWithCoefficientParameter()
    {
        // Parameterless constructor required by IXunitSerializable
    }

    public REFOfMatrixWithCoefficientParameter(TMatrix[,] matrix, TCoefficient[] coefficient, RMatrix[,] expectedMatrix,  RCoefficient[] expectedCoefficient)
        : base(matrix, expectedMatrix)
    {
        Coefficient = coefficient;
        ExpectedCoefficient = expectedCoefficient;
    }

    public new void Deserialize(IXunitSerializationInfo info)
    {
        Coefficient = info.GetValue<TCoefficient[]>(nameof(Coefficient));
        ExpectedCoefficient = info.GetValue<RCoefficient[]>(nameof(ExpectedCoefficient));
        base.Deserialize(info);
    }

    public new void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Coefficient), Coefficient);
        info.AddValue(nameof(ExpectedCoefficient), ExpectedCoefficient);
        base.Serialize(info);
    }

    public override string ToString()
    {
        string coefficient = Coefficient is null || ExpectedCoefficient is null ? "" : $", {nameof(Coefficient)}: {Coefficient.ConvertToString()}, {nameof(ExpectedCoefficient)}: {ExpectedCoefficient.ConvertToString()}";
        return $"{nameof(Matrix)}: {Matrix.ConvertTo1D().ConvertToString()}, {nameof(ExpectedMatrix)}: {ExpectedMatrix.ConvertTo1D().ConvertToString()}{coefficient}";
    }
}