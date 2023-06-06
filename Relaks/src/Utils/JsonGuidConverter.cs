using System.Text.Json;
using System.Text.Json.Serialization;

namespace Relaks.Utils;

public sealed class JsonGuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetGuid(out Guid value))
        {
            return value;
        }

        throw new FormatException("The JSON value is not in a supported Guid format.");
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}