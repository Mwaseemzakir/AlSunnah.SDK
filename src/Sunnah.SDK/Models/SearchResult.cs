using Sunnah.SDK.Enums;

namespace Sunnah.SDK.Models;

/// <summary>
/// Represents a search result when searching for text within Hadith collections.
/// </summary>
public sealed class SearchResult
{
    /// <summary>
    /// The collection where the match was found.
    /// </summary>
    public HadithCollection Collection { get; }

    /// <summary>
    /// The hadith number of the match.
    /// </summary>
    public int HadithNumber { get; }

    /// <summary>
    /// The matching text (Arabic or English depending on search language).
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The English name of the collection.
    /// </summary>
    public string CollectionName { get; }

    /// <summary>
    /// The book name where the hadith was found.
    /// </summary>
    public string BookName { get; }

    /// <summary>
    /// The search term that was matched.
    /// </summary>
    public string MatchedText { get; }

    /// <summary>
    /// The language of the matched text.
    /// </summary>
    public Language Language { get; }

    internal SearchResult(
        HadithCollection collection,
        int hadithNumber,
        string text,
        string collectionName,
        string bookName,
        string matchedText,
        Language language)
    {
        Collection = collection;
        HadithNumber = hadithNumber;
        Text = text;
        CollectionName = collectionName;
        BookName = bookName;
        MatchedText = matchedText;
        Language = language;
    }

    /// <inheritdoc />
    public override string ToString() => $"[{CollectionName} #{HadithNumber}] ({BookName}) — {MatchedText}";
}
