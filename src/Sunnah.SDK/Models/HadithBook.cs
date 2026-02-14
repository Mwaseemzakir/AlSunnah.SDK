using Sunnah.SDK.Enums;

namespace Sunnah.SDK.Models;

/// <summary>
/// Represents a book (chapter/section) within a Hadith collection.
/// </summary>
public sealed class HadithBook
{
    /// <summary>
    /// The collection this book belongs to.
    /// </summary>
    public HadithCollection Collection { get; }

    /// <summary>
    /// The book number within the collection.
    /// </summary>
    public int BookNumber { get; }

    /// <summary>
    /// The English name of the book/chapter.
    /// </summary>
    public string EnglishName { get; }

    /// <summary>
    /// The Arabic name of the book/chapter.
    /// </summary>
    public string ArabicName { get; }

    /// <summary>
    /// The total number of hadith in this book.
    /// </summary>
    public int HadithCount { get; }

    /// <summary>
    /// The starting hadith number in this book.
    /// </summary>
    public int HadithStartNumber { get; }

    /// <summary>
    /// The ending hadith number in this book.
    /// </summary>
    public int HadithEndNumber { get; }

    internal HadithBook(
        HadithCollection collection,
        int bookNumber,
        string englishName,
        string arabicName,
        int hadithCount,
        int hadithStartNumber,
        int hadithEndNumber)
    {
        Collection = collection;
        BookNumber = bookNumber;
        EnglishName = englishName;
        ArabicName = arabicName;
        HadithCount = hadithCount;
        HadithStartNumber = hadithStartNumber;
        HadithEndNumber = hadithEndNumber;
    }

    /// <inheritdoc />
    public override string ToString() => $"Book {BookNumber}: {EnglishName} ({ArabicName}) — {HadithCount} hadith";
}
