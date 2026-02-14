using Sunnah.SDK.Enums;

namespace Sunnah.SDK.Models;

/// <summary>
/// Represents a single Hadith (prophetic tradition) with its Arabic text, English translation, and metadata.
/// </summary>
public sealed class Hadith
{
    /// <summary>
    /// The collection this Hadith belongs to.
    /// </summary>
    public HadithCollection Collection { get; }

    /// <summary>
    /// The hadith number within the collection (the primary reference number used by scholars).
    /// </summary>
    public int HadithNumber { get; }

    /// <summary>
    /// The Arabic text of the Hadith.
    /// </summary>
    public string ArabicText { get; }

    /// <summary>
    /// The Arabic text without tashkeel (diacritical marks).
    /// </summary>
    public string ArabicTextNoTashkeel { get; }

    /// <summary>
    /// The preprocessed/lemmatized Arabic text (normalized with stopword removal and lemmatization).
    /// </summary>
    public string PreprocessedText { get; }

    /// <summary>
    /// The English translation of the Hadith.
    /// </summary>
    public string EnglishText { get; }

    /// <summary>
    /// The book/chapter number within the collection.
    /// </summary>
    public int BookNumber { get; }

    /// <summary>
    /// The English name of the book/chapter this Hadith belongs to.
    /// </summary>
    public string BookName { get; }

    /// <summary>
    /// The authenticity grade of this Hadith.
    /// </summary>
    public HadithGrade Grade { get; }

    /// <summary>
    /// The raw grade text as provided by scholars (e.g., "Sahih", "Hasan Sahih").
    /// </summary>
    public string GradeText { get; }

    /// <summary>
    /// The reference string used on sunnah.com (e.g., "Bukhari 1" or "Muslim 1").
    /// </summary>
    public string Reference { get; }

    /// <summary>
    /// The in-book reference (book number : hadith number within book).
    /// </summary>
    public string InBookReference { get; }

    internal Hadith(
        HadithCollection collection,
        int hadithNumber,
        string arabicText,
        string arabicTextNoTashkeel,
        string preprocessedText,
        string englishText,
        int bookNumber,
        string bookName,
        HadithGrade grade,
        string gradeText,
        string reference,
        string inBookReference)
    {
        Collection = collection;
        HadithNumber = hadithNumber;
        ArabicText = arabicText;
        ArabicTextNoTashkeel = arabicTextNoTashkeel;
        PreprocessedText = preprocessedText;
        EnglishText = englishText;
        BookNumber = bookNumber;
        BookName = bookName;
        Grade = grade;
        GradeText = gradeText;
        Reference = reference;
        InBookReference = inBookReference;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var preview = EnglishText.Length > 80 ? EnglishText.Substring(0, 80) + "..." : EnglishText;
        return $"[{Collection} #{HadithNumber}] {preview}";
    }
}
