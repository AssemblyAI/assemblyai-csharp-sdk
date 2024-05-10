using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum LemurModel
{
    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "basic")]
    Basic,

    [EnumMember(Value = "assemblyai/mistral-7b")]
    AssemblyaiMistral7b,

    [EnumMember(Value = "anthropic/claude-2-1")]
    AnthropicClaude2_1
}
