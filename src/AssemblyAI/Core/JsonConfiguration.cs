using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AssemblyAI.Lemur;
using OneOf;

namespace AssemblyAI.Core;

/// <summary>
/// The JSON options used by the AssemblyAI SDK.
/// </summary>
internal static class JsonOptions
{
    /// <summary>
    /// The JSON options used by the AssemblyAI SDK.
    /// </summary>
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

/// <summary>
/// Utilities class for JSON serialization and deserialization.
/// </summary>
internal static class JsonUtils
{
    /// <summary>
    /// Serialize an object to JSON using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <typeparam name="T">Type of the object to serialize</typeparam>
    /// <returns>The object serialized as JSON</returns>
    internal static string Serialize<T>(T obj) 
        => JsonSerializer.Serialize(obj, JsonOptions.JsonSerializerOptions);

    /// <summary>
    /// Serialize an object to JSON using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <typeparam name="T">Type of the object to serialize</typeparam>
    /// <returns>The object serialized as JSON</returns>
    internal static JsonDocument SerializeToDocument<T>(T obj) 
        => JsonSerializer.SerializeToDocument(obj, JsonOptions.JsonSerializerOptions);

    /// <summary>
    /// Serialize an object to JSON using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <typeparam name="T">Type of the object to serialize</typeparam>
    /// <returns>The object serialized as JSON</returns>
    internal static JsonElement SerializeToElement<T>(T obj) 
        => JsonSerializer.SerializeToElement(obj, JsonOptions.JsonSerializerOptions);

    /// <summary>
    /// Serialize an object to JSON using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <typeparam name="T">Type of the object to serialize</typeparam>
    /// <returns>The object serialized as JSON</returns>
    internal static JsonNode? SerializeToNode<T>(T obj)
        => JsonSerializer.SerializeToNode(obj, JsonOptions.JsonSerializerOptions);

    /// <summary>
    /// Deserialize a JSON string to an object using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="json">The JSON string</param>
    /// <typeparam name="T">The type to deserialize the JSON to</typeparam>
    /// <returns>The deserialized object of type T</returns>
    internal static T Deserialize<T>(string json) 
        => JsonSerializer.Deserialize<T>(json, JsonOptions.JsonSerializerOptions)!;

    /// <summary>
    /// Deserialize a JSON document to an object using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="json">The JSON string</param>
    /// <typeparam name="T">The type to deserialize the JSON to</typeparam>
    /// <returns>The deserialized object of type T</returns>
    internal static T Deserialize<T>(JsonDocument json) 
        => json.Deserialize<T>(JsonOptions.JsonSerializerOptions)!;

    /// <summary>
    /// Deserialize a JSON element to an object using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="json">The JSON string</param>
    /// <typeparam name="T">The type to deserialize the JSON to</typeparam>
    /// <returns>The deserialized object of type T</returns>
    internal static T Deserialize<T>(JsonElement json) 
        => json.Deserialize<T>(JsonOptions.JsonSerializerOptions)!;

    /// <summary>
    /// Deserialize a JSON node to an object using the AssemblyAI SDKs JSON options.
    /// </summary>
    /// <param name="json">The JSON string</param>
    /// <typeparam name="T">The type to deserialize the JSON to</typeparam>
    /// <returns>The deserialized object of type T</returns>
    internal static T Deserialize<T>(JsonNode json) 
        => json.Deserialize<T>(JsonOptions.JsonSerializerOptions)!;
}