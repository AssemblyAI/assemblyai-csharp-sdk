using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Realtime;

public record RealtimeError
{
    [JsonPropertyName("error")]
    public required string Error { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
