using System.Collections.Generic;
using System.Net.Http;

namespace AssemblyAI.Core
{
    public class ClientOptions
    {
        public HttpClient HttpClient { get; set;} = new HttpClient();
    
        public int MaxRetries { get; set; } = 2;

        public int TimeoutInSeconds { get; set; } = 60;

        public IReadOnlyDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string BaseUrl { get; set; } = Environment.Production.Url;
    }
}