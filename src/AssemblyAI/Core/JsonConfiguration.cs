using System.Text.Json;
using System.Text.Json.Serialization;
using AssemblyAI.Lemur;
using OneOf;

namespace AssemblyAI.Core;

internal static class JsonOptions
{
    internal static readonly JsonSerializerOptions JsonSerializerOptions;

    static JsonOptions()
    {
        JsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new DateTimeSerializer(),
                new OneOfSerializer<OneOf<LemurStringResponse, LemurQuestionAnswerResponse>>()
            },
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }
}

internal static class JsonUtils
{
    internal static string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, JsonOptions.JsonSerializerOptions);
    }

    internal static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, JsonOptions.JsonSerializerOptions)!;
    }
}
