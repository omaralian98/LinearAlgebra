using System.Text.Json;
using System.Text.Json.Serialization;

public class TwoDimensionalArrayConverter<T> : JsonConverter<T[,]>
{
    public override T[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var root = jsonDocument.RootElement;

        if (root.GetArrayLength() == 0)
        {
            throw new JsonException("Empty array.");
        }

        int rows = root.GetArrayLength();
        int cols = root[0].GetArrayLength();

        T[,] result = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            var row = root[i];
            if (row.GetArrayLength() != cols)
            {
                throw new JsonException("Jagged array.");
            }

            for (int j = 0; j < cols; j++)
            {
                result[i, j] = JsonSerializer.Deserialize<T>(row[j].GetRawText(), options);
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, T[,] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        for (int i = 0; i < value.GetLength(0); i++)
        {
            writer.WriteStartArray();
            for (int j = 0; j < value.GetLength(1); j++)
            {
                JsonSerializer.Serialize(writer, value[i, j], options);
            }
            writer.WriteEndArray();
        }
        writer.WriteEndArray();
    }
}
