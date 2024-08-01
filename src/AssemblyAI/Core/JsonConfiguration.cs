using System.Text.Json;
using System.Text.Json.Serialization;

namespace AssemblyAI.Core;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions JsonSerializerOptions;

    static JsonOptions()
    {
        JsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new DateTimeSerializer() },
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }
}

public static class JsonUtils
{
    public static string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, JsonOptions.JsonSerializerOptions);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, JsonOptions.JsonSerializerOptions)!;
    }
}
