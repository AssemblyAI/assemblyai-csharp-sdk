using System.Net.Http;

namespace AssemblyAI.Core;

internal static class HttpMethodExtensions
{
    public static readonly HttpMethod Patch = new("PATCH");
}
