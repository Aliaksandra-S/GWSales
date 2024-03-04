using System.Text.Json;
using System.Text.Json.Serialization;

namespace GWSales.WebApi.JsonConverter;

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //return DateOnly.FromDateTime(reader.GetDateTime());

        string dateString = reader.GetString();
        if (DateOnly.TryParseExact(dateString, "dd.MM.yyyy", out DateOnly result))
        {
            return result;
        }

        throw new JsonException("Unable to convert to DateOnly.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("dd.MM.yyyy");
        writer.WriteStringValue(isoDate);
    }
}
