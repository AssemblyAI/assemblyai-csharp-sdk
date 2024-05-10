namespace AssemblyaiApi;

public class CreateRealtimeTemporaryTokenParams
{
    /// <summary>
    /// The amount of time until the token expires in seconds
    /// </summary>
    public int ExpiresIn { get; init; }
}
