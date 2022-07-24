namespace Challenger.Extensions;


public static class StringExtensions
{
    /// <summary>
    /// Base-64 encodes text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Base64Encode(this string text)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Decodes text that was Base-64 encoded
    /// </summary>
    /// <param name="encodedText"></param>
    /// <returns></returns>
    public static string Base64Decode(this string encodedText)
    {
        var bytes = Convert.FromBase64String(encodedText);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}