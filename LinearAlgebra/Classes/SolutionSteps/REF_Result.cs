using System.Text.Json.Serialization;

namespace LinearAlgebra.Classes.SolutionSteps;

[Serializable]
public record REF_Result<T, S> : REF_Result<T>
{
    [JsonPropertyName("Coefficient")]
    public S[] Coefficient { get; set; } = [];
}

[Serializable]
public record REF_Result<T>
{
    [JsonPropertyName("Matrix")]
    public T[,] Matrix { get; set; } = new T[0, 0];
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";
}