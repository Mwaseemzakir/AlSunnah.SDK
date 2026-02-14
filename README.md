# Sunnah.SDK

A comprehensive, high-performance .NET library providing complete read-only access to the major Hadith collections. Includes **Sahih al-Bukhari**, **Sahih Muslim**, **Sunan Abu Dawud**, **Jami at-Tirmidhi**, **Sunan an-Nasa'i**, **Sunan Ibn Majah**, and more — with full Arabic text, English translations, book/chapter navigation, grading metadata, and powerful search capabilities.

## Features

- **16 Hadith Collections** — All six major collections (Kutub al-Sittah) plus 10 additional compilations
- **Full Arabic Text** — Original Arabic hadith text
- **English Translations** — Complete English translations
- **Book/Chapter Navigation** — Browse by collection → book → hadith
- **Hadith Grading** — Sahih, Hasan, Da'if classification
- **Powerful Search** — Search across collections in English or Arabic (diacritics-aware)
- **Random & Daily Hadith** — Get random or deterministic "Hadith of the Day"
- **Reference Parsing** — Parse references like `"bukhari:1"` or `"muslim:1-5"`
- **Zero Dependencies** — Fully offline, embedded data
- **Thread-Safe** — All operations are thread-safe with lazy-loaded data
- **Multi-Target** — Supports .NET 8, .NET Standard 2.0, and .NET Framework 4.6.2

## Installation

```
dotnet add package Sunnah.SDK
```

## Quick Start

```csharp
using Sunnah.SDK;
using Sunnah.SDK.Enums;

// Get a specific hadith
var hadith = Hadith.GetHadith(HadithCollection.Bukhari, 1);
Console.WriteLine(hadith.EnglishText);
Console.WriteLine(hadith.ArabicText);

// Get by reference string
var h = Hadith.GetHadith("bukhari:1");

// Browse collections
var collections = Hadith.GetAllCollections();
var sihahSittah = Hadith.GetSihahSittah();

// Browse books in a collection
var books = Hadith.GetBooks(HadithCollection.Bukhari);

// Get all hadith in a book
var bookHadith = Hadith.GetHadithByBook(HadithCollection.Bukhari, 1);

// Search English text
var results = Hadith.Search("prayer", HadithCollection.Bukhari);

// Search Arabic text (diacritics-aware)
var arabicResults = Hadith.SearchArabic("صلاة", HadithCollection.Muslim);

// Search across ALL collections
var allResults = Hadith.SearchAll("fasting");

// Get random hadith
var random = Hadith.GetRandomHadith(HadithCollection.Bukhari);

// Get "Hadith of the Day" (deterministic per date)
var daily = Hadith.GetHadithOfTheDay(HadithCollection.Muslim);

// Filter by grade
var sahihOnly = Hadith.GetSahihHadith(HadithCollection.Tirmidhi);
var weakOnly = Hadith.GetDaifHadith(HadithCollection.IbnMajah);

// Grade distribution
var grades = Hadith.GetGradeDistribution(HadithCollection.AbuDawud);
```

## Supported Collections

### Kutub al-Sittah (The Six Major Collections)
| # | Collection | Author | Hadith Count |
|---|-----------|--------|-------------|
| 1 | Sahih al-Bukhari | Imam al-Bukhari | ~7,563 |
| 2 | Sahih Muslim | Imam Muslim | ~7,563 |
| 3 | Sunan Abu Dawud | Imam Abu Dawud | ~5,274 |
| 4 | Jami at-Tirmidhi | Imam al-Tirmidhi | ~3,956 |
| 5 | Sunan an-Nasa'i | Imam an-Nasa'i | ~5,758 |
| 6 | Sunan Ibn Majah | Imam Ibn Majah | ~4,341 |

### Additional Collections
| # | Collection | Author |
|---|-----------|--------|
| 7 | Muwatta Malik | Imam Malik |
| 8 | Musnad Ahmad | Imam Ahmad |
| 9 | Sunan ad-Darimi | Imam ad-Darimi |
| 10 | Riyad as-Salihin | Imam an-Nawawi |
| 11 | Bulugh al-Maram | Ibn Hajar |
| 12 | Al-Adab Al-Mufrad | Imam al-Bukhari |
| 13 | Mishkat al-Masabih | al-Tabrizi |
| 14 | Shama'il Muhammadiyah | Imam al-Tirmidhi |
| 15 | 40 Hadith Nawawi | Imam an-Nawawi |
| 16 | 40 Hadith Qudsi | Various |

## License

See [LICENSE.txt](LICENSE.txt) for details.
