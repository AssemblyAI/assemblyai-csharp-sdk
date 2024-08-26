using System.Runtime.Serialization;

namespace AssemblyAI.Core;

internal static class Extensions
{
    internal static string Stringify(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (EnumMemberAttribute)
            Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute));
        return attribute?.Value ?? value.ToString();
    }
}