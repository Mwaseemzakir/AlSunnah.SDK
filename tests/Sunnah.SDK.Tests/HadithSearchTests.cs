using Sunnah.SDK;
using Sunnah.SDK.Enums;
using Xunit;

namespace Sunnah.SDK.Tests;

public class HadithSearchTests
{
    #region English Search

    [Fact]
    public void Search_EmptyTerm_ReturnsEmptyList()
    {
        var results = Hadith.Search("", HadithCollection.Bukhari);

        Assert.Empty(results);
    }

    [Fact]
    public void Search_WhitespaceTerm_ReturnsEmptyList()
    {
        var results = Hadith.Search("   ", HadithCollection.Bukhari);

        Assert.Empty(results);
    }

    #endregion

    #region Arabic Search

    [Fact]
    public void SearchArabic_ValidTerm_ReturnsResults()
    {
        var results = Hadith.SearchArabic("رسول الله", HadithCollection.Bukhari);

        Assert.NotEmpty(results);
        Assert.All(results, r => Assert.Equal(Language.Arabic, r.Language));
    }

    [Fact]
    public void SearchArabic_EmptyTerm_ReturnsEmptyList()
    {
        var results = Hadith.SearchArabic("", HadithCollection.Bukhari);

        Assert.Empty(results);
    }

    #endregion

    #region Preprocessed Search

    [Fact]
    public void SearchPreprocessed_ValidTerm_ReturnsResults()
    {
        // "رسول" is a common word that should appear in preprocessed text
        var results = Hadith.SearchPreprocessed("رسول", HadithCollection.Bukhari);

        Assert.NotEmpty(results);
    }

    [Fact]
    public void SearchPreprocessed_EmptyTerm_ReturnsEmptyList()
    {
        var results = Hadith.SearchPreprocessed("", HadithCollection.Bukhari);

        Assert.Empty(results);
    }

    [Fact]
    public void SearchAllPreprocessed_ValidTerm_ReturnsResults()
    {
        var results = Hadith.SearchAllPreprocessed("رسول");

        Assert.NotEmpty(results);
    }

    [Fact]
    public void SearchAllPreprocessed_EmptyTerm_ReturnsEmptyList()
    {
        var results = Hadith.SearchAllPreprocessed("");

        Assert.Empty(results);
    }

    #endregion

    #region Search All

    [Fact]
    public void SearchAllArabic_ValidTerm_ReturnsResults()
    {
        var results = Hadith.SearchAllArabic("رسول الله");

        Assert.NotEmpty(results);
    }

    [Fact]
    public void SearchAllArabic_EmptyTerm_ReturnsEmptyList()
    {
        var results = Hadith.SearchAllArabic("");

        Assert.Empty(results);
    }

    #endregion

    #region Book-Specific Search

    [Fact]
    public void Search_EmptyTermInBook_ReturnsEmptyList()
    {
        var results = Hadith.Search("", HadithCollection.Bukhari, 1);

        Assert.Empty(results);
    }

    #endregion
}
