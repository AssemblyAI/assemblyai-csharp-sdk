namespace AssemblyAI.Core;

public class APIError : AssemblyAIError
{
    public int? StatusCode { init; get; }
    
    public object? Body { init; get; }
}