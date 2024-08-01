using System.Text.Json;
using System.Text.Json.Serialization;
using AssemblyAI.Lemur;

namespace AssemblyAI.Core;

public class LemurResponseConverter : JsonConverter<LemurResponse>
{
    public override LemurResponse? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        if (reader.TokenType is JsonTokenType.Null)
            return default;

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        if (jsonElement.GetProperty("response").ValueKind == JsonValueKind.Array)
        {
            return jsonElement.Deserialize<LemurQuestionAnswerResponse>() ?? 
                   throw new Exception("Failed to deserialize LemurQuestionAnswerResponse");
        }

        return jsonElement.Deserialize<LemurStringResponse>() ?? 
               throw new Exception("Failed to deserialize LemurStringResponse");
    }

    public override void Write(Utf8JsonWriter writer, LemurResponse value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}