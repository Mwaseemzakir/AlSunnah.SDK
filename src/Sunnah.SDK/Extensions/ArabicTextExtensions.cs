using System.Text;
using System.Text.RegularExpressions;

namespace Sunnah.SDK.Extensions;

/// <summary>
/// Extension methods for working with Arabic text, including tashkeel removal and normalization.
/// </summary>
public static class ArabicTextExtensions
{
    private static readonly Regex TashkeelRegex = new(
        @"[\u064B-\u0655\u0670\u06D6-\u06ED\u0610-\u061A\u06D6-\u06DC\u06DF-\u06E4\u06E7\u06E8\u06EA-\u06ED\uFE70-\uFE7F]",
        RegexOptions.Compiled);

    private static readonly Regex NormalizationRegex = new(
        @"[\u0622\u0623\u0625]",
        RegexOptions.Compiled);

    /// <summary>
    /// Removes all tashkeel (diacritical marks) from the Arabic text.
    /// </summary>
    /// <param name="input">The Arabic text with tashkeel.</param>
    /// <returns>The text without tashkeel.</returns>
    public static string RemoveTashkeel(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return TashkeelRegex.Replace(input, string.Empty);
    }

    /// <summary>
    /// Normalizes Arabic text by replacing different forms of Alef and Hamza with plain Alef.
    /// </summary>
    /// <param name="input">The Arabic text to normalize.</param>
    /// <returns>Normalized Arabic text.</returns>
    public static string NormalizeAlef(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return NormalizationRegex.Replace(input, "\u0627");
    }

    /// <summary>
    /// Normalizes Arabic text for searching by removing tashkeel and normalizing Alef forms.
    /// </summary>
    /// <param name="input">The Arabic text to normalize.</param>
    /// <returns>Normalized Arabic text suitable for comparison.</returns>
    public static string NormalizeForSearch(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return input.RemoveTashkeel().NormalizeAlef().Trim();
    }

    /// <summary>
    /// Checks whether a character is an Arabic tashkeel (diacritical mark) character.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a tashkeel mark.</returns>
    public static bool IsTashkeelChar(char c)
    {
        return (c >= '\u064B' && c <= '\u0655') ||
               c == '\u0670' ||
               (c >= '\u06D6' && c <= '\u06ED') ||
               (c >= '\u0610' && c <= '\u061A') ||
               (c >= '\uFE70' && c <= '\uFE7F');
    }
}
