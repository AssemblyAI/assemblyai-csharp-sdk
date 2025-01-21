using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Lemur;

[JsonConverter(typeof(EnumSerializer<LemurModel>))]
public enum LemurModel
{
    [EnumMember(Value = "anthropic/claude-3-5-sonnet")]
    AnthropicClaude3_5_Sonnet,

    [EnumMember(Value = "anthropic/claude-3-opus")]
    AnthropicClaude3_Opus,

    [EnumMember(Value = "anthropic/claude-3-haiku")]
    AnthropicClaude3_Haiku,

    [EnumMember(Value = "anthropic/claude-3-sonnet")]
    AnthropicClaude3_Sonnet,

    [EnumMember(Value = "anthropic/claude-2-1")]
    AnthropicClaude2_1,

    [EnumMember(Value = "anthropic/claude-2")]
    AnthropicClaude2_0,

    [EnumMember(Value = "anthropic/claude-2")]
    [Obsolete("Use AnthropicClaude2_0")]
    AnthropicClaude2,

    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "anthropic/claude-instant-1-2")]
    AnthropicClaudeInstant1_2,

    [EnumMember(Value = "basic")]
    Basic,

    [EnumMember(Value = "assemblyai/mistral-7b")]
    AssemblyaiMistral7b,
}
