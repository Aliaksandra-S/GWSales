using System.Text.Json.Serialization;

namespace GWSales.Services.Models;

public class CommandResult<TType, TResult>
    where TType : Enum
    where TResult : class
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TType ResultType { get; set; }

    public TResult? Value { get; set; }

    public List<string>? Messages { get; set; } = new List<string>();
}
