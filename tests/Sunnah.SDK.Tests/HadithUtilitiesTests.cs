using Sunnah.SDK;
using Sunnah.SDK.Enums;
using Sunnah.SDK.Exceptions;
using Sunnah.SDK.Models;
using Xunit;

namespace Sunnah.SDK.Tests;

public class HadithUtilitiesTests
{
    #region Reference Parsing

    [Fact]
    public void ParseReference_ValidSingleReference_ParsesCorrectly()
    {
        var reference = Hadith.ParseReference("bukhari:1");

        Assert.Equal(HadithCollection.Bukhari, reference.Collection);
        Assert.Equal(1, reference.HadithNumber);
        Assert.False(reference.IsRange);
    }

    [Fact]
    public void ParseReference_ValidRangeReference_ParsesCorrectly()
    {
        var reference = Hadith.ParseReference("muslim:1-5");

        Assert.Equal(HadithCollection.Muslim, reference.Collection);
        Assert.Equal(1, reference.HadithNumber);
        Assert.Equal(5, reference.EndHadithNumber);
        Assert.True(reference.IsRange);
    }

    [Fact]
    public void ParseReference_InvalidFormat_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => Hadith.ParseReference("invalid"));
    }

    [Fact]
    public void GetHadith_ByReference_ReturnsHadith()
    {
        var hadith = Hadith.GetHadith("bukhari:1");

        Assert.NotNull(hadith);
        Assert.Equal(1, hadith.HadithNumber);
        Assert.Equal(HadithCollection.Bukhari, hadith.Collection);
    }

    [Fact]
    public void GetHadithRange_ByReference_ReturnsHadithList()
    {
        var hadith = Hadith.GetHadithRange("bukhari:1-3");

        Assert.NotEmpty(hadith);
    }

    [Fact]
    public void GetHadithRange_SingleReference_ReturnsSingleItem()
    {
        var hadith = Hadith.GetHadithRange("bukhari:1");

        Assert.Single(hadith);
    }

    #endregion

    #region Random Hadith

    [Fact]
    public void GetRandomHadith_FromCollection_ReturnsHadith()
    {
        var hadith = Hadith.GetRandomHadith(HadithCollection.Bukhari);

        Assert.NotNull(hadith);
        Assert.Equal(HadithCollection.Bukhari, hadith.Collection);
    }

    [Fact]
    public void GetRandomHadith_AnyCollection_ReturnsHadith()
    {
        var hadith = Hadith.GetRandomHadith();

        Assert.NotNull(hadith);
    }

    [Fact]
    public void GetRandomHadith_FromBook_ReturnsHadith()
    {
        var books = Hadith.GetBooks(HadithCollection.Bukhari);
        var firstBook = books.First();

        var hadith = Hadith.GetRandomHadith(HadithCollection.Bukhari, firstBook.BookNumber);

        Assert.NotNull(hadith);
    }

    #endregion

    #region Hadith of the Day

    [Fact]
    public void GetHadithOfTheDay_ReturnsHadith()
    {
        var hadith = Hadith.GetHadithOfTheDay(HadithCollection.Bukhari);

        Assert.NotNull(hadith);
        Assert.Equal(HadithCollection.Bukhari, hadith.Collection);
    }

    [Fact]
    public void GetHadithOfTheDay_SameDate_ReturnsSameHadith()
    {
        var date = new DateTime(2025, 1, 1);

        var hadith1 = Hadith.GetHadithOfTheDay(HadithCollection.Bukhari, date);
        var hadith2 = Hadith.GetHadithOfTheDay(HadithCollection.Bukhari, date);

        Assert.Equal(hadith1.HadithNumber, hadith2.HadithNumber);
    }

    [Fact]
    public void GetHadithOfTheDay_DifferentDates_MayReturnDifferentHadith()
    {
        var date1 = new DateTime(2025, 1, 1);
        var date2 = new DateTime(2025, 6, 15);

        var hadith1 = Hadith.GetHadithOfTheDay(HadithCollection.Bukhari, date1);
        var hadith2 = Hadith.GetHadithOfTheDay(HadithCollection.Bukhari, date2);

        // Not guaranteed different, but highly likely with different seed
        Assert.NotNull(hadith1);
        Assert.NotNull(hadith2);
    }

    #endregion

    #region Statistics

    [Fact]
    public void GetArabicWordCount_Bukhari_ReturnsPositiveCount()
    {
        var count = Hadith.GetArabicWordCount(HadithCollection.Bukhari);

        Assert.True(count > 0);
    }

    [Fact]
    public void GetGradeDistribution_Bukhari_ReturnsDistribution()
    {
        var distribution = Hadith.GetGradeDistribution(HadithCollection.Bukhari);

        Assert.NotNull(distribution);
        Assert.NotEmpty(distribution);
    }

    #endregion
}
