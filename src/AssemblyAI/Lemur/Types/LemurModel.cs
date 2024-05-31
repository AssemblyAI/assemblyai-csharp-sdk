using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI;

[JsonConverter(typeof(StringEnumSerializer<LemurModel>))]
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
