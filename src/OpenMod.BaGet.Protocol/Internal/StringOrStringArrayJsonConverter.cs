using System.Text.Json;

namespace OpenMod.BaGet.Protocol.Internal;

#nullable disable

internal class StringOrStringArrayJsonConverter : JsonConverter<IReadOnlyList<string>>
{
    public override IReadOnlyList<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return new List<string>
            {
                reader.GetString(),
            };
        }

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        var list = new List<string>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                list.Add(reader.GetString());
                continue;
            }

            if (reader.TokenType != JsonTokenType.EndArray)
            {
                break;
            }

            return list;
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, IReadOnlyList<string> values, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var value in values)
        {
            writer.WriteStringValue(value);
        }

        writer.WriteEndArray();
    }
}
