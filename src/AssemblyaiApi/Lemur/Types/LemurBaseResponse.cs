using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class LemurBaseResponse
{
    /// <summary>
    /// The ID of the LeMUR request
    /// </summary>
    [JsonPropertyName("request_id")]
    public string RequestID { get; init; }
}
