using System.Text.Json;
using System.Text.Json.Serialization;

namespace GWSales.WebApi.JsonConverter;

public sealed class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateString = reader.GetString();
        if (dateString == null)
        {
            return null;
        }

        if (DateOnly.TryParseExact(dateString, "dd.MM.yyyy", out DateOnly result))
        {
            return result;
        }

        throw new JsonException($"Unable to convert {dateString} to DateOnly.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(((DateOnly)value).ToString("dd.MM.yyyy"));
    }
}
