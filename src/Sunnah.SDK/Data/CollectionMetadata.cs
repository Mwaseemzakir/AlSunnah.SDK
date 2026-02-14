using Sunnah.SDK.Enums;
using Sunnah.SDK.Models;

namespace Sunnah.SDK.Data;

/// <summary>
/// Contains metadata catalog for all supported Hadith collections.
/// </summary>
internal static class CollectionMetadata
{
    internal const int TotalCollections = 16;

    internal static readonly CollectionInfo[] AllCollections =
    {
        //                          collection                    englishName                  arabicName               shortName       authorName                                   totalHadith totalBooks  isSihahSittah  apiSlug              resourceName
        new(HadithCollection.Bukhari,            "Sahih al-Bukhari",          "صحيح البخاري",          "Bukhari",      "Imam Muhammad al-Bukhari",                  7563,       97,         true,          "bukhari",           "bukhari"),
        new(HadithCollection.Muslim,             "Sahih Muslim",              "صحيح مسلم",             "Muslim",       "Imam Muslim ibn al-Hajjaj",                 7563,       56,         true,          "muslim",            "muslim"),
        new(HadithCollection.AbuDawud,           "Sunan Abu Dawud",           "سنن أبي داود",          "Abu Dawud",    "Imam Abu Dawud al-Sijistani",               5274,       43,         true,          "abudawud",          "abudawud"),
        new(HadithCollection.Tirmidhi,           "Jami at-Tirmidhi",          "جامع الترمذي",          "Tirmidhi",     "Imam al-Tirmidhi",                          3956,       49,         true,          "tirmidhi",          "tirmidhi"),
        new(HadithCollection.Nasai,              "Sunan an-Nasa'i",           "سنن النسائي",           "Nasai",        "Imam an-Nasa'i",                            5758,       51,         true,          "nasai",             "nasai"),
        new(HadithCollection.IbnMajah,           "Sunan Ibn Majah",           "سنن ابن ماجه",          "Ibn Majah",    "Imam Ibn Majah",                            4341,       37,         true,          "ibnmajah",          "ibnmajah"),
        new(HadithCollection.Malik,              "Muwatta Malik",             "موطأ مالك",             "Malik",        "Imam Malik ibn Anas",                       1832,       61,         false,         "malik",             "malik"),
        new(HadithCollection.Ahmad,              "Musnad Ahmad",              "مسند أحمد",             "Ahmad",        "Imam Ahmad ibn Hanbal",                     28199,      6,          false,         "ahmad",             "ahmad"),
        new(HadithCollection.Darimi,             "Sunan ad-Darimi",           "سنن الدارمي",           "Darimi",       "Imam ad-Darimi",                            3367,       23,         false,         "darimi",            "darimi"),
        new(HadithCollection.RiyadAsSalihin,     "Riyad as-Salihin",          "رياض الصالحين",         "Riyad",        "Imam an-Nawawi",                            1896,       19,         false,         "riyadussalihin",    "riyadussalihin"),
        new(HadithCollection.BulughAlMaram,      "Bulugh al-Maram",           "بلوغ المرام",           "Bulugh",       "Ibn Hajar al-Asqalani",                     1582,       16,         false,         "bulughalmaram",     "bulughalmaram"),
        new(HadithCollection.AdabAlMufrad,       "Al-Adab Al-Mufrad",         "الأدب المفرد",          "Adab",         "Imam al-Bukhari",                           1322,       57,         false,         "adab",              "adab"),
        new(HadithCollection.MishkatAlMasabih,   "Mishkat al-Masabih",        "مشكاة المصابيح",        "Mishkat",      "al-Khatib al-Tabrizi",                      6294,       30,         false,         "mishkat",           "mishkat"),
        new(HadithCollection.ShamailMuhammadiyah,"Shama'il Muhammadiyah",     "الشمائل المحمدية",      "Shamail",      "Imam al-Tirmidhi",                          396,        56,         false,         "shamail",           "shamail"),
        new(HadithCollection.FortyNawawi,        "40 Hadith Nawawi",          "الأربعون النووية",      "Nawawi",       "Imam an-Nawawi",                            42,         1,          false,         "nawawi40",          "nawawi40"),
        new(HadithCollection.FortyQudsi,         "40 Hadith Qudsi",           "الأحاديث القدسية",      "Qudsi",        "Various",                                   40,         1,          false,         "qudsi40",           "qudsi40"),
    };

    private static readonly Dictionary<HadithCollection, CollectionInfo> Lookup =
        AllCollections.ToDictionary(c => c.Collection);

    private static readonly Dictionary<string, CollectionInfo> BySlug =
        AllCollections.ToDictionary(c => c.ApiSlug, StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets the collection info for a specific collection, or null if not found.
    /// </summary>
    internal static CollectionInfo? GetInfo(HadithCollection collection)
    {
        Lookup.TryGetValue(collection, out var info);
        return info;
    }

    /// <summary>
    /// Gets a collection info by its API slug, or null if not found.
    /// </summary>
    internal static CollectionInfo? GetBySlug(string slug)
    {
        BySlug.TryGetValue(slug, out var info);
        return info;
    }
}
