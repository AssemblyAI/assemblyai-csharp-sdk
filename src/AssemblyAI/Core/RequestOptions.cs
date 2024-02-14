namespace AssemblyAI.Core
{
    public class RequestOptions
    {
        public string ApiKey { get; set; }
    
        public int? MaxRetries { get; set; }
    
        public int? TimeoutInSeconds { get; set; }
    }
}