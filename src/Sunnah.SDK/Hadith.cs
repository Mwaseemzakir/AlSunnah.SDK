using Sunnah.SDK.Data;
using Sunnah.SDK.Enums;
using Sunnah.SDK.Exceptions;
using Sunnah.SDK.Models;

namespace Sunnah.SDK;

/// <summary>
/// The main entry point for accessing Hadith (prophetic traditions) data.
/// Provides static methods for retrieving hadith, navigating collections, books, performing searches, and more.
/// </summary>
public static partial class Hadith
{
    #region Lookup Maps

    private static readonly Lazy<Dictionary<HadithCollection, CollectionInfo>> CollectionMap = new(
        () => CollectionMetadata.AllCollections.ToDictionary(c => c.Collection), isThreadSafe: true);

    private static readonly Lazy<Dictionary<string, CollectionInfo>> CollectionByName = new(
        () => CollectionMetadata.AllCollections.ToDictionary(c => c.EnglishName.ToLowerInvariant()), isThreadSafe: true);

    #endregion

    #region Constants

    /// <summary>
    /// The total number of supported Hadith collections.
    /// </summary>
    public const int TotalCollections = 16;

    /// <summary>
    /// The number of Kutub al-Sittah (the six major authentic collections).
    /// </summary>
    public const int TotalSihahSittah = 6;

    #endregion

    #region Collection Methods

    /// <summary>
    /// Gets metadata for a specific Hadith collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>The collection metadata.</returns>
    /// <exception cref="CollectionNotFoundException">Thrown when the collection is not found.</exception>
    public static CollectionInfo GetCollection(HadithCollection collection)
    {
        if (CollectionMap.Value.TryGetValue(collection, out var info))
            return info;

        throw new CollectionNotFoundException($"Collection '{collection}' was not found.");
    }

    /// <summary>
    /// Gets metadata for a Hadith collection by its English name. Case-insensitive.
    /// </summary>
    /// <param name="englishName">The English name (e.g., "Sahih al-Bukhari").</param>
    /// <returns>The collection metadata.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the name is null or whitespace.</exception>
    /// <exception cref="CollectionNotFoundException">Thrown when no collection with the given name is found.</exception>
    public static CollectionInfo GetCollection(string englishName)
    {
        if (string.IsNullOrWhiteSpace(englishName))
            throw new ArgumentNullException(nameof(englishName));

        if (CollectionByName.Value.TryGetValue(englishName.Trim().ToLowerInvariant(), out var info))
            return info;

        throw new CollectionNotFoundException($"Collection with name '{englishName}' was not found.");
    }

    /// <summary>
    /// Gets metadata for a Hadith collection, or null if not found.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>The collection metadata, or null if not found.</returns>
    public static CollectionInfo? GetCollectionOrDefault(HadithCollection collection)
    {
        CollectionMap.Value.TryGetValue(collection, out var info);
        return info;
    }

    /// <summary>
    /// Gets all supported Hadith collections.
    /// </summary>
    /// <returns>A list of all collection metadata.</returns>
    public static List<CollectionInfo> GetAllCollections()
    {
        return new List<CollectionInfo>(CollectionMetadata.AllCollections);
    }

    /// <summary>
    /// Gets the six major authentic Hadith collections (Kutub al-Sittah):
    /// Bukhari, Muslim, Abu Dawud, Tirmidhi, Nasa'i, and Ibn Majah.
    /// </summary>
    /// <returns>A list of the six major collections.</returns>
    public static List<CollectionInfo> GetSihahSittah()
    {
        return CollectionMetadata.AllCollections.Where(c => c.IsSihahSittah).ToList();
    }

    /// <summary>
    /// Checks whether data is available for a specific collection.
    /// </summary>
    /// <param name="collection">The collection to check.</param>
    /// <returns>True if hadith data is loaded and available.</returns>
    public static bool IsDataAvailable(HadithCollection collection)
    {
        return HadithDataProvider.IsAvailable(collection);
    }

    #endregion

    #region Book Methods

    /// <summary>
    /// Gets all books/chapters for a specific Hadith collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>A list of books in the collection.</returns>
    public static List<HadithBook> GetBooks(HadithCollection collection)
    {
        return HadithDataProvider.GetBooks(collection);
    }

    /// <summary>
    /// Gets a specific book by number within a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="bookNumber">The book number.</param>
    /// <returns>The book metadata.</returns>
    /// <exception cref="BookNotFoundException">Thrown when the book is not found.</exception>
    public static HadithBook GetBook(HadithCollection collection, int bookNumber)
    {
        var book = HadithDataProvider.GetBook(collection, bookNumber);
        if (book == null)
            throw new BookNotFoundException($"Book {bookNumber} was not found in {collection}.");

        return book;
    }

    /// <summary>
    /// Gets a specific book by number, or null if not found.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="bookNumber">The book number.</param>
    /// <returns>The book metadata, or null if not found.</returns>
    public static HadithBook? GetBookOrDefault(HadithCollection collection, int bookNumber)
    {
        return HadithDataProvider.GetBook(collection, bookNumber);
    }

    #endregion

    #region Hadith Retrieval Methods

    /// <summary>
    /// Gets a specific Hadith by collection and hadith number.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="hadithNumber">The hadith number within the collection.</param>
    /// <returns>The Hadith.</returns>
    /// <exception cref="HadithNotFoundException">Thrown when the hadith is not found.</exception>
    public static Models.Hadith GetHadith(HadithCollection collection, int hadithNumber)
    {
        var hadith = HadithDataProvider.GetHadith(collection, hadithNumber);
        if (hadith == null)
            throw new HadithNotFoundException($"Hadith #{hadithNumber} was not found in {collection}.");

        return hadith;
    }

    /// <summary>
    /// Gets a specific Hadith by collection and hadith number, or null if not found.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="hadithNumber">The hadith number.</param>
    /// <returns>The Hadith, or null if not found.</returns>
    public static Models.Hadith? GetHadithOrDefault(HadithCollection collection, int hadithNumber)
    {
        return HadithDataProvider.GetHadith(collection, hadithNumber);
    }

    /// <summary>
    /// Gets all hadith in a specific collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>A list of all hadith in the collection.</returns>
    public static List<Models.Hadith> GetAllHadith(HadithCollection collection)
    {
        return new List<Models.Hadith>(HadithDataProvider.GetAllHadith(collection));
    }

    /// <summary>
    /// Gets all hadith from a specific book within a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="bookNumber">The book number.</param>
    /// <returns>A list of hadith in the specified book.</returns>
    /// <exception cref="BookNotFoundException">Thrown when the book is not found.</exception>
    public static List<Models.Hadith> GetHadithByBook(HadithCollection collection, int bookNumber)
    {
        var hadith = HadithDataProvider.GetHadithByBook(collection, bookNumber);
        if (hadith.Count == 0)
        {
            // Check if the book exists at all
            var book = HadithDataProvider.GetBook(collection, bookNumber);
            if (book == null)
                throw new BookNotFoundException($"Book {bookNumber} was not found in {collection}.");
        }

        return new List<Models.Hadith>(hadith);
    }

    /// <summary>
    /// Gets a range of hadith from a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="startNumber">The starting hadith number (inclusive).</param>
    /// <param name="endNumber">The ending hadith number (inclusive).</param>
    /// <returns>A list of hadith in the specified range.</returns>
    public static List<Models.Hadith> GetHadithRange(HadithCollection collection, int startNumber, int endNumber)
    {
        return HadithDataProvider.GetAllHadith(collection)
            .Where(h => h.HadithNumber >= startNumber && h.HadithNumber <= endNumber)
            .ToList();
    }

    /// <summary>
    /// Gets the total number of hadith loaded for a specific collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>The total hadith count.</returns>
    public static int GetHadithCount(HadithCollection collection)
    {
        return HadithDataProvider.GetCount(collection);
    }

    /// <summary>
    /// Gets the Arabic text without tashkeel (diacritical marks) for a specific hadith.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="hadithNumber">The hadith number.</param>
    /// <returns>The Arabic text without tashkeel.</returns>
    /// <exception cref="HadithNotFoundException">Thrown when the hadith is not found.</exception>
    public static string GetArabicTextNoTashkeel(HadithCollection collection, int hadithNumber)
    {
        var hadith = GetHadith(collection, hadithNumber);
        return hadith.ArabicTextNoTashkeel;
    }

    /// <summary>
    /// Gets the preprocessed/lemmatized Arabic text for a specific hadith.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="hadithNumber">The hadith number.</param>
    /// <returns>The preprocessed Arabic text.</returns>
    /// <exception cref="HadithNotFoundException">Thrown when the hadith is not found.</exception>
    public static string GetPreprocessedText(HadithCollection collection, int hadithNumber)
    {
        var hadith = GetHadith(collection, hadithNumber);
        return hadith.PreprocessedText;
    }

    #endregion

    #region Grade-Based Methods

    /// <summary>
    /// Gets all hadith in a collection with a specific grade.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <param name="grade">The hadith grade to filter by.</param>
    /// <returns>A list of hadith matching the specified grade.</returns>
    public static List<Models.Hadith> GetHadithByGrade(HadithCollection collection, HadithGrade grade)
    {
        return HadithDataProvider.GetAllHadith(collection)
            .Where(h => h.Grade == grade)
            .ToList();
    }

    /// <summary>
    /// Gets only the authentic (Sahih) hadith from a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>A list of Sahih hadith.</returns>
    public static List<Models.Hadith> GetSahihHadith(HadithCollection collection)
    {
        return HadithDataProvider.GetAllHadith(collection)
            .Where(h => h.Grade == HadithGrade.Sahih || h.Grade == HadithGrade.SahihLiGhayrihi)
            .ToList();
    }

    /// <summary>
    /// Gets only the Hasan (good) hadith from a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>A list of Hasan hadith.</returns>
    public static List<Models.Hadith> GetHasanHadith(HadithCollection collection)
    {
        return HadithDataProvider.GetAllHadith(collection)
            .Where(h => h.Grade == HadithGrade.Hasan || h.Grade == HadithGrade.HasanSahih || h.Grade == HadithGrade.HasanLiGhayrihi)
            .ToList();
    }

    /// <summary>
    /// Gets only the weak (Da'if) hadith from a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>A list of Da'if hadith.</returns>
    public static List<Models.Hadith> GetDaifHadith(HadithCollection collection)
    {
        return HadithDataProvider.GetAllHadith(collection)
            .Where(h => h.Grade == HadithGrade.Daif)
            .ToList();
    }

    #endregion
}
