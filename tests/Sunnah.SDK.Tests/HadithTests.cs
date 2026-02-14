using Sunnah.SDK;
using Sunnah.SDK.Enums;
using Sunnah.SDK.Exceptions;
using Sunnah.SDK.Models;
using Xunit;

namespace Sunnah.SDK.Tests;

public class HadithTests
{
    #region Collection Methods

    [Fact]
    public void GetCollection_ValidCollection_ReturnsCollectionInfo()
    {
        var info = Hadith.GetCollection(HadithCollection.Bukhari);

        Assert.NotNull(info);
        Assert.Equal(HadithCollection.Bukhari, info.Collection);
        Assert.Equal("Sahih al-Bukhari", info.EnglishName);
        Assert.True(info.IsSihahSittah);
    }

    [Fact]
    public void GetCollection_ByName_ReturnsCollectionInfo()
    {
        var info = Hadith.GetCollection("Sahih al-Bukhari");

        Assert.NotNull(info);
        Assert.Equal(HadithCollection.Bukhari, info.Collection);
    }

    [Fact]
    public void GetCollection_ByNameCaseInsensitive_ReturnsCollectionInfo()
    {
        var info = Hadith.GetCollection("sahih al-bukhari");

        Assert.NotNull(info);
        Assert.Equal(HadithCollection.Bukhari, info.Collection);
    }

    [Fact]
    public void GetCollection_InvalidName_ThrowsCollectionNotFoundException()
    {
        Assert.Throws<CollectionNotFoundException>(() => Hadith.GetCollection("NonExistent"));
    }

    [Fact]
    public void GetCollection_NullName_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Hadith.GetCollection((string)null!));
    }

    [Fact]
    public void GetCollectionOrDefault_ValidCollection_ReturnsInfo()
    {
        var info = Hadith.GetCollectionOrDefault(HadithCollection.Muslim);

        Assert.NotNull(info);
        Assert.Equal(HadithCollection.Muslim, info!.Collection);
    }

    [Fact]
    public void GetAllCollections_ReturnsAllCollections()
    {
        var collections = Hadith.GetAllCollections();

        Assert.Equal(Hadith.TotalCollections, collections.Count);
    }

    [Fact]
    public void GetSihahSittah_ReturnsSixCollections()
    {
        var sittah = Hadith.GetSihahSittah();

        Assert.Equal(Hadith.TotalSihahSittah, sittah.Count);
        Assert.All(sittah, c => Assert.True(c.IsSihahSittah));
    }

    [Fact]
    public void IsDataAvailable_Bukhari_ReturnsTrue()
    {
        Assert.True(Hadith.IsDataAvailable(HadithCollection.Bukhari));
    }

    #endregion

    #region Hadith Retrieval

    [Fact]
    public void GetHadith_ValidHadith_ReturnsHadith()
    {
        var hadith = Hadith.GetHadith(HadithCollection.Bukhari, 1);

        Assert.NotNull(hadith);
        Assert.Equal(1, hadith.HadithNumber);
        Assert.Equal(HadithCollection.Bukhari, hadith.Collection);
        Assert.False(string.IsNullOrEmpty(hadith.ArabicText));
    }

    [Fact]
    public void GetHadith_InvalidNumber_ThrowsHadithNotFoundException()
    {
        Assert.Throws<HadithNotFoundException>(() => Hadith.GetHadith(HadithCollection.Bukhari, -1));
    }

    [Fact]
    public void GetHadithOrDefault_ValidHadith_ReturnsHadith()
    {
        var hadith = Hadith.GetHadithOrDefault(HadithCollection.Bukhari, 1);

        Assert.NotNull(hadith);
    }

    [Fact]
    public void GetHadithOrDefault_InvalidNumber_ReturnsNull()
    {
        var hadith = Hadith.GetHadithOrDefault(HadithCollection.Bukhari, -1);

        Assert.Null(hadith);
    }

    [Fact]
    public void GetAllHadith_Bukhari_ReturnsNonEmptyList()
    {
        var hadith = Hadith.GetAllHadith(HadithCollection.Bukhari);

        Assert.NotEmpty(hadith);
    }

    [Fact]
    public void GetHadithCount_Bukhari_ReturnsPositiveCount()
    {
        var count = Hadith.GetHadithCount(HadithCollection.Bukhari);

        Assert.True(count > 0);
    }

    [Fact]
    public void GetHadithRange_ValidRange_ReturnsHadithInRange()
    {
        var hadith = Hadith.GetHadithRange(HadithCollection.Bukhari, 1, 5);

        Assert.NotEmpty(hadith);
        Assert.All(hadith, h =>
        {
            Assert.InRange(h.HadithNumber, 1, 5);
        });
    }

    [Fact]
    public void GetHadithRange_EmptyRange_ReturnsEmptyList()
    {
        var hadith = Hadith.GetHadithRange(HadithCollection.Bukhari, 999999, 999999);

        Assert.Empty(hadith);
    }

    #endregion

    #region Book Methods

    [Fact]
    public void GetBooks_Bukhari_ReturnsBooks()
    {
        var books = Hadith.GetBooks(HadithCollection.Bukhari);

        Assert.NotEmpty(books);
    }

    [Fact]
    public void GetBook_ValidBook_ReturnsBook()
    {
        var books = Hadith.GetBooks(HadithCollection.Bukhari);
        var firstBook = books.First();

        var book = Hadith.GetBook(HadithCollection.Bukhari, firstBook.BookNumber);

        Assert.NotNull(book);
        Assert.Equal(firstBook.BookNumber, book.BookNumber);
    }

    [Fact]
    public void GetBook_InvalidBook_ThrowsBookNotFoundException()
    {
        Assert.Throws<BookNotFoundException>(() => Hadith.GetBook(HadithCollection.Bukhari, -1));
    }

    [Fact]
    public void GetBookOrDefault_InvalidBook_ReturnsNull()
    {
        var book = Hadith.GetBookOrDefault(HadithCollection.Bukhari, -1);

        Assert.Null(book);
    }

    [Fact]
    public void GetHadithByBook_ValidBook_ReturnsHadith()
    {
        var books = Hadith.GetBooks(HadithCollection.Bukhari);
        var firstBook = books.First();

        var hadith = Hadith.GetHadithByBook(HadithCollection.Bukhari, firstBook.BookNumber);

        Assert.NotEmpty(hadith);
    }

    #endregion

    #region Grade Methods

    [Fact]
    public void GetGradeDistribution_Bukhari_ReturnsDistribution()
    {
        var distribution = Hadith.GetGradeDistribution(HadithCollection.Bukhari);

        Assert.NotNull(distribution);
    }

    #endregion

    #region New Fields

    [Fact]
    public void GetHadith_HasArabicTextNoTashkeel()
    {
        var hadith = Hadith.GetHadith(HadithCollection.Bukhari, 1);

        Assert.NotNull(hadith.ArabicTextNoTashkeel);
        Assert.NotEmpty(hadith.ArabicTextNoTashkeel);
    }

    [Fact]
    public void GetHadith_HasPreprocessedText()
    {
        var hadith = Hadith.GetHadith(HadithCollection.Bukhari, 1);

        Assert.NotNull(hadith.PreprocessedText);
        Assert.NotEmpty(hadith.PreprocessedText);
    }

    [Fact]
    public void GetArabicTextNoTashkeel_ValidHadith_ReturnsText()
    {
        var text = Hadith.GetArabicTextNoTashkeel(HadithCollection.Bukhari, 1);

        Assert.NotNull(text);
        Assert.NotEmpty(text);
    }

    [Fact]
    public void GetArabicTextNoTashkeel_InvalidHadith_Throws()
    {
        Assert.Throws<HadithNotFoundException>(() =>
            Hadith.GetArabicTextNoTashkeel(HadithCollection.Bukhari, -1));
    }

    [Fact]
    public void GetPreprocessedText_ValidHadith_ReturnsText()
    {
        var text = Hadith.GetPreprocessedText(HadithCollection.Bukhari, 1);

        Assert.NotNull(text);
        Assert.NotEmpty(text);
    }

    [Fact]
    public void GetPreprocessedText_InvalidHadith_Throws()
    {
        Assert.Throws<HadithNotFoundException>(() =>
            Hadith.GetPreprocessedText(HadithCollection.Bukhari, -1));
    }

    [Fact]
    public void ArabicTextNoTashkeel_DoesNotContainTashkeel()
    {
        var hadith = Hadith.GetHadith(HadithCollection.Bukhari, 1);

        // The no-tashkeel text should not contain common diacritical marks
        Assert.DoesNotContain("\u064E", hadith.ArabicTextNoTashkeel); // Fathah
        Assert.DoesNotContain("\u064F", hadith.ArabicTextNoTashkeel); // Dammah
        Assert.DoesNotContain("\u0650", hadith.ArabicTextNoTashkeel); // Kasrah
        Assert.DoesNotContain("\u0651", hadith.ArabicTextNoTashkeel); // Shaddah
    }

    [Fact]
    public void PreprocessedText_DiffersFromOriginal()
    {
        var hadith = Hadith.GetHadith(HadithCollection.Bukhari, 1);

        // Preprocessed text should be different from original (it's lemmatized)
        Assert.NotEqual(hadith.ArabicText, hadith.PreprocessedText);
    }

    #endregion
}
