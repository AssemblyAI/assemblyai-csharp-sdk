using System.Text.Json.Serialization;

namespace AssemblyAI;

public class UploadedFile
{
    [JsonPropertyName("upload_url")]
    public string UploadUrl { get; init; }
}