using System.Net.Http;

namespace AssemblyAI.Core;

public static class HttpMethodExtensions
{
    public static readonly HttpMethod Patch = new("PATCH");
}
