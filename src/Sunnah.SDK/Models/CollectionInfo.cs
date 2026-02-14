using Sunnah.SDK.Enums;

namespace Sunnah.SDK.Models;

/// <summary>
/// Represents metadata about a Hadith collection (e.g., Sahih al-Bukhari, Sahih Muslim).
/// </summary>
public sealed class CollectionInfo
{
    /// <summary>
    /// The collection enum identifier.
    /// </summary>
    public HadithCollection Collection { get; }

    /// <summary>
    /// The English name of the collection (e.g., "Sahih al-Bukhari").
    /// </summary>
    public string EnglishName { get; }

    /// <summary>
    /// The Arabic name of the collection (e.g., "صحيح البخاري").
    /// </summary>
    public string ArabicName { get; }

    /// <summary>
    /// A short English description or alternative name.
    /// </summary>
    public string ShortName { get; }

    /// <summary>
    /// The name of the compiler/author of the collection.
    /// </summary>
    public string AuthorName { get; }

    /// <summary>
    /// The total number of hadith in this collection.
    /// </summary>
    public int TotalHadith { get; }

    /// <summary>
    /// The total number of books/chapters in this collection.
    /// </summary>
    public int TotalBooks { get; }

    /// <summary>
    /// Whether this collection is part of the Kutub al-Sittah (the six major collections).
    /// </summary>
    public bool IsSihahSittah { get; }

    /// <summary>
    /// The API slug identifier used by sunnah.com (e.g., "bukhari", "muslim").
    /// </summary>
    internal string ApiSlug { get; }

    /// <summary>
    /// The embedded resource file name (without extension).
    /// </summary>
    internal string ResourceName { get; }

    internal CollectionInfo(
        HadithCollection collection,
        string englishName,
        string arabicName,
        string shortName,
        string authorName,
        int totalHadith,
        int totalBooks,
        bool isSihahSittah,
        string apiSlug,
        string resourceName)
    {
        Collection = collection;
        EnglishName = englishName;
        ArabicName = arabicName;
        ShortName = shortName;
        AuthorName = authorName;
        TotalHadith = totalHadith;
        TotalBooks = totalBooks;
        IsSihahSittah = isSihahSittah;
        ApiSlug = apiSlug;
        ResourceName = resourceName;
    }

    /// <inheritdoc />
    public override string ToString() => $"{EnglishName} ({ArabicName}) — {TotalHadith} hadith in {TotalBooks} books";
}
