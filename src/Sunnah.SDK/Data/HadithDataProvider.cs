using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using Sunnah.SDK.Enums;
using Sunnah.SDK.Models;

namespace Sunnah.SDK.Data;

/// <summary>
/// Provides lazy-loaded Hadith data from embedded JSON resources.
/// Thread-safe with per-collection caching.
/// </summary>
internal static class HadithDataProvider
{
    private static readonly ConcurrentDictionary<HadithCollection, Lazy<HadithStore>> Cache = new();

    /// <summary>
    /// Gets a specific Hadith by collection and hadith number.
    /// </summary>
    internal static Models.Hadith? GetHadith(HadithCollection collection, int hadithNumber)
    {
        var store = GetStore(collection);
        store.ByNumber.TryGetValue(hadithNumber, out var hadith);
        return hadith;
    }

    /// <summary>
    /// Gets all hadith for a specific book within a collection.
    /// </summary>
    internal static List<Models.Hadith> GetHadithByBook(HadithCollection collection, int bookNumber)
    {
        var store = GetStore(collection);
        return store.ByBook.TryGetValue(bookNumber, out var list) ? list : new List<Models.Hadith>();
    }

    /// <summary>
    /// Gets all hadith in a collection.
    /// </summary>
    internal static List<Models.Hadith> GetAllHadith(HadithCollection collection)
    {
        var store = GetStore(collection);
        return store.AllHadith;
    }

    /// <summary>
    /// Gets all books for a collection.
    /// </summary>
    internal static List<HadithBook> GetBooks(HadithCollection collection)
    {
        var store = GetStore(collection);
        return store.Books;
    }

    /// <summary>
    /// Gets a specific book by number.
    /// </summary>
    internal static HadithBook? GetBook(HadithCollection collection, int bookNumber)
    {
        var store = GetStore(collection);
        return store.BookByNumber.TryGetValue(bookNumber, out var book) ? book : null;
    }

    /// <summary>
    /// Checks whether data is loaded and available for a specific collection.
    /// </summary>
    internal static bool IsAvailable(HadithCollection collection)
    {
        var store = GetStore(collection);
        return store.AllHadith.Count > 0;
    }

    /// <summary>
    /// Gets the total number of loaded hadith for a collection.
    /// </summary>
    internal static int GetCount(HadithCollection collection)
    {
        var store = GetStore(collection);
        return store.AllHadith.Count;
    }

    private static HadithStore GetStore(HadithCollection collection)
    {
        var lazy = Cache.GetOrAdd(collection, c =>
            new Lazy<HadithStore>(() => Load(c), true));
        return lazy.Value;
    }

    private static HadithStore Load(HadithCollection collection)
    {
        var store = new HadithStore();

        try
        {
            var info = CollectionMetadata.GetInfo(collection);
            if (info == null) return store;

            var resourceName = $"Sunnah.SDK.Data.Resources.{info.ResourceName}.json";
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null) return store;

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var entries = JsonSerializer.Deserialize<List<HadithEntry>>(json, options);
            if (entries == null) return store;

            var bookMap = new Dictionary<int, (string EnglishName, string ArabicName, int MinNum, int MaxNum, int Count)>();

            foreach (var entry in entries)
            {
                var grade = ParseGrade(entry.Grade);

                var hadith = new Models.Hadith(
                    collection,
                    entry.HadithNumber,
                    entry.ArabicText ?? string.Empty,
                    entry.ArabicTextNoTashkeel ?? string.Empty,
                    entry.PreprocessedText ?? string.Empty,
                    entry.EnglishText ?? string.Empty,
                    entry.BookNumber,
                    entry.BookName ?? string.Empty,
                    grade,
                    entry.Grade ?? string.Empty,
                    entry.Reference ?? string.Empty,
                    entry.InBookReference ?? string.Empty);

                store.AllHadith.Add(hadith);
                store.ByNumber[entry.HadithNumber] = hadith;

                if (!store.ByBook.ContainsKey(entry.BookNumber))
                    store.ByBook[entry.BookNumber] = new List<Models.Hadith>();
                store.ByBook[entry.BookNumber].Add(hadith);

                // Track book metadata
                if (!bookMap.ContainsKey(entry.BookNumber))
                {
                    bookMap[entry.BookNumber] = (
                        entry.BookName ?? string.Empty,
                        entry.BookArabicName ?? string.Empty,
                        entry.HadithNumber,
                        entry.HadithNumber,
                        1);
                }
                else
                {
                    var existing = bookMap[entry.BookNumber];
                    bookMap[entry.BookNumber] = (
                        existing.EnglishName,
                        existing.ArabicName,
                        Math.Min(existing.MinNum, entry.HadithNumber),
                        Math.Max(existing.MaxNum, entry.HadithNumber),
                        existing.Count + 1);
                }
            }

            // Build book list
            foreach (var kvp in bookMap.OrderBy(k => k.Key))
            {
                var book = new HadithBook(
                    collection,
                    kvp.Key,
                    kvp.Value.EnglishName,
                    kvp.Value.ArabicName,
                    kvp.Value.Count,
                    kvp.Value.MinNum,
                    kvp.Value.MaxNum);

                store.Books.Add(book);
                store.BookByNumber[kvp.Key] = book;
            }
        }
        catch
        {
            // If data files are not present, return empty — allows metadata-only usage
        }

        return store;
    }

    private static HadithGrade ParseGrade(string? gradeText)
    {
        if (string.IsNullOrWhiteSpace(gradeText))
            return HadithGrade.Unknown;

        var lower = gradeText!.Trim().ToLowerInvariant();

        if (lower.Contains("mawdu") || lower.Contains("fabricat"))
            return HadithGrade.Mawdu;

        if (lower.Contains("hasan sahih") || lower.Contains("hasan/sahih"))
            return HadithGrade.HasanSahih;

        if (lower.Contains("sahih li ghayrihi") || lower.Contains("sahih lighairihi"))
            return HadithGrade.SahihLiGhayrihi;

        if (lower.Contains("hasan li ghayrihi") || lower.Contains("hasan lighairihi"))
            return HadithGrade.HasanLiGhayrihi;

        if (lower.Contains("sahih"))
            return HadithGrade.Sahih;

        if (lower.Contains("hasan"))
            return HadithGrade.Hasan;

        if (lower.Contains("da'if") || lower.Contains("daif") || lower.Contains("da`if") || lower.Contains("weak"))
            return HadithGrade.Daif;

        return HadithGrade.Unknown;
    }

    /// <summary>
    /// Internal store holding loaded data for a single collection.
    /// </summary>
    private class HadithStore
    {
        public List<Models.Hadith> AllHadith { get; } = new();
        public Dictionary<int, Models.Hadith> ByNumber { get; } = new();
        public Dictionary<int, List<Models.Hadith>> ByBook { get; } = new();
        public List<HadithBook> Books { get; } = new();
        public Dictionary<int, HadithBook> BookByNumber { get; } = new();
    }

    /// <summary>
    /// Internal DTO for JSON deserialization.
    /// </summary>
    private class HadithEntry
    {
        public int HadithNumber { get; set; }
        public string? ArabicText { get; set; }
        public string? ArabicTextNoTashkeel { get; set; }
        public string? PreprocessedText { get; set; }
        public string? EnglishText { get; set; }
        public int BookNumber { get; set; }
        public string? BookName { get; set; }
        public string? BookArabicName { get; set; }
        public string? Grade { get; set; }
        public string? Reference { get; set; }
        public string? InBookReference { get; set; }
    }
}
