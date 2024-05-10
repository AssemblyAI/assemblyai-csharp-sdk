using AssemblyaiApi;

namespace AssemblyaiApi;

public class StreamingClient
{
    private RawClient _client;

    public StreamingClient(RawClient client)
    {
        _client = client;
    }
}
