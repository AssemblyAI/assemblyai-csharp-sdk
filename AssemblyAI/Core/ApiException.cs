namespace AssemblyAI.Core;

public class ApiException : AssemblyAIException
{
    public int? StatusCode { init; get; }
    
    public object? Body { init; get; }
}