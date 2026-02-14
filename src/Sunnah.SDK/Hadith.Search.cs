using Sunnah.SDK.Data;
using Sunnah.SDK.Enums;
using Sunnah.SDK.Exceptions;
using Sunnah.SDK.Extensions;
using Sunnah.SDK.Models;

namespace Sunnah.SDK;

public static partial class Hadith
{
    #region Search Methods

    /// <summary>
    /// Searches a specific Hadith collection for English text containing the specified search term.
    /// Case-insensitive search.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="collection">The collection to search within.</param>
    /// <returns>A list of search results.</returns>
    public static List<SearchResult> Search(string searchTerm, HadithCollection collection)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var results = new List<SearchResult>();
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        var info = CollectionMetadata.GetInfo(collection);
        var collectionName = info?.EnglishName ?? collection.ToString();

        foreach (var h in allHadith)
        {
            if (h.EnglishText.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                results.Add(new SearchResult(
                    collection,
                    h.HadithNumber,
                    h.EnglishText,
                    collectionName,
                    h.BookName,
                    searchTerm,
                    Language.English));
            }
        }

        return results;
    }

    /// <summary>
    /// Searches a specific Hadith collection for Arabic text containing the specified search term.
    /// The search is diacritics-aware (tashkeel is ignored during matching).
    /// </summary>
    /// <param name="searchTerm">The Arabic text to search for.</param>
    /// <param name="collection">The collection to search within.</param>
    /// <returns>A list of search results.</returns>
    public static List<SearchResult> SearchArabic(string searchTerm, HadithCollection collection)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var normalizedSearch = searchTerm.NormalizeForSearch();
        var results = new List<SearchResult>();
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        var info = CollectionMetadata.GetInfo(collection);
        var collectionName = info?.EnglishName ?? collection.ToString();

        foreach (var h in allHadith)
        {
            var normalizedText = h.ArabicText.NormalizeForSearch();
            if (normalizedText.Contains(normalizedSearch))
            {
                results.Add(new SearchResult(
                    collection,
                    h.HadithNumber,
                    h.ArabicText,
                    collectionName,
                    h.BookName,
                    searchTerm,
                    Language.Arabic));
            }
        }

        return results;
    }

    /// <summary>
    /// Searches across ALL loaded Hadith collections for English text containing the specified search term.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <returns>A list of search results from all collections.</returns>
    public static List<SearchResult> SearchAll(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var results = new List<SearchResult>();

        foreach (var collectionInfo in CollectionMetadata.AllCollections)
        {
            if (!HadithDataProvider.IsAvailable(collectionInfo.Collection))
                continue;

            results.AddRange(Search(searchTerm, collectionInfo.Collection));
        }

        return results;
    }

    /// <summary>
    /// Searches across ALL loaded Hadith collections for Arabic text containing the specified search term.
    /// </summary>
    /// <param name="searchTerm">The Arabic text to search for.</param>
    /// <returns>A list of search results from all collections.</returns>
    public static List<SearchResult> SearchAllArabic(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var results = new List<SearchResult>();

        foreach (var collectionInfo in CollectionMetadata.AllCollections)
        {
            if (!HadithDataProvider.IsAvailable(collectionInfo.Collection))
                continue;

            results.AddRange(SearchArabic(searchTerm, collectionInfo.Collection));
        }

        return results;
    }

    /// <summary>
    /// Searches a specific Hadith collection using preprocessed/lemmatized Arabic text.
    /// This enables more flexible matching by searching against normalized, stemmed text.
    /// </summary>
    /// <param name="searchTerm">The text to search for in preprocessed text.</param>
    /// <param name="collection">The collection to search within.</param>
    /// <returns>A list of search results.</returns>
    public static List<SearchResult> SearchPreprocessed(string searchTerm, HadithCollection collection)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var results = new List<SearchResult>();
        var allHadith = HadithDataProvider.GetAllHadith(collection);
        var info = CollectionMetadata.GetInfo(collection);
        var collectionName = info?.EnglishName ?? collection.ToString();

        foreach (var h in allHadith)
        {
            if (h.PreprocessedText.Contains(searchTerm))
            {
                results.Add(new SearchResult(
                    collection,
                    h.HadithNumber,
                    h.PreprocessedText,
                    collectionName,
                    h.BookName,
                    searchTerm,
                    Language.Arabic));
            }
        }

        return results;
    }

    /// <summary>
    /// Searches across ALL loaded Hadith collections using preprocessed/lemmatized Arabic text.
    /// </summary>
    /// <param name="searchTerm">The text to search for in preprocessed text.</param>
    /// <returns>A list of search results from all collections.</returns>
    public static List<SearchResult> SearchAllPreprocessed(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var results = new List<SearchResult>();

        foreach (var collectionInfo in CollectionMetadata.AllCollections)
        {
            if (!HadithDataProvider.IsAvailable(collectionInfo.Collection))
                continue;

            results.AddRange(SearchPreprocessed(searchTerm, collectionInfo.Collection));
        }

        return results;
    }

    /// <summary>
    /// Searches within a specific book of a collection for English text containing the search term.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="collection">The collection to search within.</param>
    /// <param name="bookNumber">The book number to search within.</param>
    /// <returns>A list of search results.</returns>
    public static List<SearchResult> Search(string searchTerm, HadithCollection collection, int bookNumber)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var results = new List<SearchResult>();
        var hadith = HadithDataProvider.GetHadithByBook(collection, bookNumber);
        var info = CollectionMetadata.GetInfo(collection);
        var collectionName = info?.EnglishName ?? collection.ToString();

        foreach (var h in hadith)
        {
            if (h.EnglishText.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                results.Add(new SearchResult(
                    collection,
                    h.HadithNumber,
                    h.EnglishText,
                    collectionName,
                    h.BookName,
                    searchTerm,
                    Language.English));
            }
        }

        return results;
    }

    #endregion
}
