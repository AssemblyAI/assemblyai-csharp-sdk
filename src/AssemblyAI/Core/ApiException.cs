namespace AssemblyAI.Core
{
    public class ApiException : AssemblyAIException
    {
        public int? StatusCode { set; get; }
    
        public object Body { set; get; }
    }
}