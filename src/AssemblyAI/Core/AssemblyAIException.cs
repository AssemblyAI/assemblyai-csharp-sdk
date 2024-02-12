namespace AssemblyAI.Core;

public class AssemblyAIException : SystemException {
    
    public AssemblyAIException() {}
    public AssemblyAIException(string? message)
        : base(message) {}
}