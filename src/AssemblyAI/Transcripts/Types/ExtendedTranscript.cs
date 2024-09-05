using System.Text.Json;
using System.Text.Json.Nodes;
using AssemblyAI.Core;

namespace AssemblyAI.Transcripts;

public partial record Transcript
{
    /// <summary>
    /// Create a transcript from a JSON string.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public static Transcript FromJson(string json)
        => JsonUtils.Deserialize<Transcript>(json);

    /// <summary>
    /// Create a transcript from a JSON document.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public static  Transcript FromJson(JsonDocument json)
        => JsonUtils.Deserialize<Transcript>(json);

    /// <summary>
    /// Create a transcript from a JSON element.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public static  Transcript FromJson(JsonElement json)
        => JsonUtils.Deserialize<Transcript>(json);

    /// <summary>
    /// Create a transcript from a JSON node.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public static  Transcript FromJson(JsonNode json)
        => JsonUtils.Deserialize<Transcript>(json);

    /// <summary>
    /// Serialize transcript to JSON string.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public string ToJson()
        => JsonUtils.Serialize(this);

    /// <summary>
    /// Serialize transcript to JSON document.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public JsonDocument ToJsonDocument()
        => JsonUtils.SerializeToDocument(this);

    /// <summary>
    /// Serialize transcript to JSON element.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public JsonElement ToJsonElement()
        => JsonUtils.SerializeToElement(this);

    /// <summary>
    /// Serialize transcript to JSON element.
    /// </summary>
    /// <returns>The object serialized as JSON</returns>
    public JsonNode? ToJsonNode()
        => JsonUtils.SerializeToNode(this);
}