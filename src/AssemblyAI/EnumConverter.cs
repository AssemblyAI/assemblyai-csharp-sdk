using AssemblyAI.Core;

namespace AssemblyAI;

/// <summary>
/// Convert an AssemblyAI enum to a string and vice versa.
/// </summary>
public static class EnumConverter
{
    /// <summary>
    /// Convert a string value to an enum. For example, "en_us" to TranscriptLanguageCode.EnUs.
    /// </summary>
    /// <param name="value">String value of the enum</param>
    /// <typeparam name="T">The enum type to convert into</typeparam>
    /// <returns>An enum value of the given enum type</returns>
    /// <remarks>This method uses the Value property of EnumMemberAttribute on the given enum value.</remarks>
    public static T ToEnum<T>(string value) where T : Enum => JsonUtils.Deserialize<T>($"\"{value}\"");
   
    /// <summary>
    /// Convert an enum value to a string value. For example, TranscriptLanguageCode.EnUs to "en_us".
    /// </summary>
    /// <param name="value">Enum value</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The string value of the given enum</returns>
    /// <remarks>This method uses the Value property of EnumMemberAttribute on the given enum value.</remarks>
    public static string ToString<T>(T value) where T : Enum => JsonUtils.Serialize(value).Trim('"');
}