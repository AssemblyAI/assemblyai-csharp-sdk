namespace BlazorSample.Server.Models;

internal class TranscribeFileModel
{
    public string LanguageCode { get; set; }
    public IFormFile File { get; set; }
}