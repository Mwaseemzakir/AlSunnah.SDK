using Sunnah.SDK.Extensions;
using Xunit;

namespace Sunnah.SDK.Tests;

public class ArabicTextExtensionsTests
{
    [Fact]
    public void RemoveTashkeel_RemovesDiacritics()
    {
        var input = "بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ";
        var result = input.RemoveTashkeel();

        Assert.DoesNotContain("\u0650", result); // Kasrah
        Assert.DoesNotContain("\u064E", result); // Fathah
        Assert.DoesNotContain("\u0652", result); // Sukun
        Assert.Contains("بسم", result);
    }

    [Fact]
    public void RemoveTashkeel_EmptyString_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, string.Empty.RemoveTashkeel());
    }

    [Fact]
    public void RemoveTashkeel_NullString_ReturnsNull()
    {
        Assert.Null(((string)null!).RemoveTashkeel());
    }

    [Fact]
    public void NormalizeAlef_NormalizesAlefVariants()
    {
        var input = "إبراهيم أحمد";
        var result = input.NormalizeAlef();

        // Both إ and أ should be normalized to ا
        Assert.Contains("ابراهيم", result);
        Assert.Contains("احمد", result);
    }

    [Fact]
    public void NormalizeAlef_EmptyString_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, string.Empty.NormalizeAlef());
    }

    [Fact]
    public void NormalizeForSearch_RemovesTashkeelAndNormalizesAlef()
    {
        var input = "بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ";
        var result = input.NormalizeForSearch();

        Assert.DoesNotContain("\u0650", result);
        Assert.DoesNotContain("\u064E", result);
    }

    [Fact]
    public void NormalizeForSearch_EmptyString_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, string.Empty.NormalizeForSearch());
    }

    [Fact]
    public void IsTashkeelChar_Fathah_ReturnsTrue()
    {
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u064E'));
    }

    [Fact]
    public void IsTashkeelChar_RegularLetter_ReturnsFalse()
    {
        Assert.False(ArabicTextExtensions.IsTashkeelChar('ا'));
    }

    [Fact]
    public void IsTashkeelChar_Kasrah_ReturnsTrue()
    {
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u0650'));
    }

    [Fact]
    public void IsTashkeelChar_Shaddah_ReturnsTrue()
    {
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u0651'));
    }
}
