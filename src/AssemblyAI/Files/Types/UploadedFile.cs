using System.Text.Json.Serialization;

#nullable enable

namespace AssemblyAI.Files;

public record UploadedFile
{
    /// <summary>
    /// A URL that points to your audio file, accessible only by AssemblyAI's servers
    /// </summary>
    [JsonPropertyName("upload_url")]
    public required string UploadUrl { get; set; }
}
