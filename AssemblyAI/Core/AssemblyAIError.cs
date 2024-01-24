namespace AssemblyAI.Core;

public class AssemblyAIError : SystemException {
    
    public AssemblyAIError() {}
    public AssemblyAIError(string? message)
        : base(message) {}
}