namespace AssemblyAI.Core;

public class RequestOptions
{
    public string? ApiKey { get; init; }

    public int? MaxRetries { get; init; }

    public int? TimeoutInSeconds { get; init; }
}