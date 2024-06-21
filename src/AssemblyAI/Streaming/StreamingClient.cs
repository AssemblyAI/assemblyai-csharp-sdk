using AssemblyAI;

#nullable enable

namespace AssemblyAI;

public class StreamingClient
{
    private RawClient _client;

    public StreamingClient(RawClient client)
    {
        _client = client;
    }
}
