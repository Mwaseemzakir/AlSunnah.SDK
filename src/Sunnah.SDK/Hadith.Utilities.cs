using Sunnah.SDK.Data;
using Sunnah.SDK.Enums;
using Sunnah.SDK.Exceptions;
using Sunnah.SDK.Models;

namespace Sunnah.SDK;

public static partial class Hadith
{
    #region Private Helpers

    private static readonly object RandomLock = new object();
    private static readonly Random SharedRandom = new Random();

    private static int NextRandom(int maxExclusive)
    {
        lock (RandomLock)
        {
            return SharedRandom.Next(maxExclusive);
        }
    }

    #endregion

    #region Reference Methods

    /// <summary>
    /// Parses a hadith reference string into a <see cref="HadithReference"/>.
    /// Supported formats: "bukhari:1" (single hadith), "bukhari:1-5" (hadith range).
    /// </summary>
    /// <param name="reference">The hadith reference string.</param>
    /// <returns>A parsed <see cref="HadithReference"/>.</returns>
    /// <exception cref="FormatException">Thrown when the reference format is invalid.</exception>
    public static HadithReference ParseReference(string reference)
    {
        return HadithReference.Parse(reference);
    }

    /// <summary>
    /// Gets a specific Hadith by reference string (e.g., "bukhari:1").
    /// </summary>
    /// <param name="reference">The hadith reference in "collection:number" format.</param>
    /// <returns>The Hadith.</returns>
    /// <exception cref="FormatException">Thrown when the reference format is invalid.</exception>
    /// <exception cref="HadithNotFoundException">Thrown when the hadith is not found.</exception>
    public static Models.Hadith GetHadith(string reference)
    {
        var parsed = HadithReference.Parse(reference);
        return GetHadith(parsed.Collection, parsed.HadithNumber);
    }

    /// <summary>
    /// Gets a range of hadith by reference string (e.g., "bukhari:1-5").
    /// Also supports single-hadith references like "muslim:1" (returns a list with one item).
    /// </summary>
    /// <param name="reference">The hadith reference in "collection:number" or "collection:start-end" format.</param>
    /// <returns>A list of hadith.</returns>
    public static List<Models.Hadith> GetHadithRange(string reference)
    {
        var parsed = HadithReference.Parse(reference);
        if (parsed.IsRange)
        {
            return GetHadithRange(parsed.Collection, parsed.HadithNumber, parsed.EndHadithNumber!.Value);
        }
        return new List<Models.Hadith> { GetHadith(parsed.Collection, parsed.HadithNumber) };
    }

    #endregion

    #region Random & Daily Hadith Methods

    /// <summary>
    /// Gets a random Hadith from a specific collection.
    /// </summary>
    /// <param name="collection">The collection to pick from.</param>
    /// <returns>A random Hadith.</returns>
    /// <exception cref="CollectionNotFoundException">Thrown when the collection has no data.</exception>
    public static Models.Hadith GetRandomHadith(HadithCollection collection)
    {
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        if (allHadith.Count == 0)
            throw new CollectionNotFoundException($"No hadith data available for {collection}.");

        int index = NextRandom(allHadith.Count);
        return allHadith[index];
    }

    /// <summary>
    /// Gets a random Hadith from any loaded collection.
    /// </summary>
    /// <returns>A random Hadith from any available collection.</returns>
    /// <exception cref="CollectionNotFoundException">Thrown when no collections have data loaded.</exception>
    public static Models.Hadith GetRandomHadith()
    {
        var available = CollectionMetadata.AllCollections
            .Where(c => HadithDataProvider.IsAvailable(c.Collection))
            .ToList();

        if (available.Count == 0)
            throw new CollectionNotFoundException("No hadith data is available in any collection.");

        var randomCollection = available[NextRandom(available.Count)];
        return GetRandomHadith(randomCollection.Collection);
    }

    /// <summary>
    /// Gets a random Hadith from a specific book within a collection.
    /// </summary>
    /// <param name="collection">The collection to pick from.</param>
    /// <param name="bookNumber">The book number.</param>
    /// <returns>A random Hadith from the specified book.</returns>
    /// <exception cref="BookNotFoundException">Thrown when the book is not found or empty.</exception>
    public static Models.Hadith GetRandomHadith(HadithCollection collection, int bookNumber)
    {
        var hadith = HadithDataProvider.GetHadithByBook(collection, bookNumber);
        if (hadith.Count == 0)
            throw new BookNotFoundException($"No hadith found in Book {bookNumber} of {collection}.");

        int index = NextRandom(hadith.Count);
        return hadith[index];
    }

    /// <summary>
    /// Gets the "Hadith of the Day" — a deterministic Hadith based on the current UTC date.
    /// The same date always returns the same Hadith, making it suitable for daily reflection features.
    /// </summary>
    /// <param name="collection">The collection to pick from.</param>
    /// <returns>The Hadith of the day.</returns>
    /// <exception cref="CollectionNotFoundException">Thrown when the collection has no data.</exception>
    public static Models.Hadith GetHadithOfTheDay(HadithCollection collection)
    {
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        if (allHadith.Count == 0)
            throw new CollectionNotFoundException($"No hadith data available for {collection}.");

        var today = DateTime.UtcNow.Date;
        int seed = today.Year * 10000 + today.Month * 100 + today.Day;
        var rng = new Random(seed);
        int index = rng.Next(allHadith.Count);
        return allHadith[index];
    }

    /// <summary>
    /// Gets the "Hadith of the Day" for a specific date.
    /// </summary>
    /// <param name="collection">The collection to pick from.</param>
    /// <param name="date">The date to get the Hadith for.</param>
    /// <returns>The Hadith of the specified day.</returns>
    /// <exception cref="CollectionNotFoundException">Thrown when the collection has no data.</exception>
    public static Models.Hadith GetHadithOfTheDay(HadithCollection collection, DateTime date)
    {
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        if (allHadith.Count == 0)
            throw new CollectionNotFoundException($"No hadith data available for {collection}.");

        int seed = date.Year * 10000 + date.Month * 100 + date.Day;
        var rng = new Random(seed);
        int index = rng.Next(allHadith.Count);
        return allHadith[index];
    }

    #endregion

    #region Statistics Methods

    /// <summary>
    /// Gets the total word count for all hadith in a collection (English text).
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>The number of words.</returns>
    public static int GetWordCount(HadithCollection collection)
    {
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        return allHadith.Sum(h => CountWords(h.EnglishText));
    }

    /// <summary>
    /// Gets the total word count for Arabic text of all hadith in a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>The number of words.</returns>
    public static int GetArabicWordCount(HadithCollection collection)
    {
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        return allHadith.Sum(h => CountWords(h.ArabicText));
    }

    /// <summary>
    /// Gets a summary of hadith grade distribution for a collection.
    /// </summary>
    /// <param name="collection">The collection identifier.</param>
    /// <returns>A dictionary of grade to count.</returns>
    public static Dictionary<HadithGrade, int> GetGradeDistribution(HadithCollection collection)
    {
        return HadithDataProvider.GetAllHadith(collection)
            .GroupBy(h => h.Grade)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private static int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    #endregion
}
