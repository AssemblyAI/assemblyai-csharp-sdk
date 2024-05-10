using System.Text.Json.Serialization;

namespace AssemblyaiApi;

public class UploadedFile
{
    /// <summary>
    /// A URL that points to your audio file, accessible only by AssemblyAI's servers
    /// </summary>
    [JsonPropertyName("upload_url")]
    public string UploadURL { get; init; }
}
