namespace Sunnah.SDK.Enums;

/// <summary>
/// Identifies a major Hadith collection.
/// Each value corresponds to a well-known compilation of prophetic traditions.
/// </summary>
public enum HadithCollection
{
    /// <summary>Sahih al-Bukhari — compiled by Imam Muhammad al-Bukhari (d. 870 CE). Widely regarded as the most authentic collection.</summary>
    Bukhari = 1,

    /// <summary>Sahih Muslim — compiled by Imam Muslim ibn al-Hajjaj (d. 875 CE). The second most authentic collection.</summary>
    Muslim = 2,

    /// <summary>Sunan Abu Dawud — compiled by Imam Abu Dawud al-Sijistani (d. 889 CE). Focuses on legal hadith.</summary>
    AbuDawud = 3,

    /// <summary>Jami at-Tirmidhi — compiled by Imam al-Tirmidhi (d. 892 CE). Includes grading of each hadith.</summary>
    Tirmidhi = 4,

    /// <summary>Sunan an-Nasa'i — compiled by Imam an-Nasa'i (d. 915 CE). Known for strict authentication criteria.</summary>
    Nasai = 5,

    /// <summary>Sunan Ibn Majah — compiled by Imam Ibn Majah (d. 887 CE). Completes the six major collections (Kutub al-Sittah).</summary>
    IbnMajah = 6,

    /// <summary>Muwatta Malik — compiled by Imam Malik ibn Anas (d. 795 CE). One of the earliest hadith compilations.</summary>
    Malik = 7,

    /// <summary>Musnad Ahmad — compiled by Imam Ahmad ibn Hanbal (d. 855 CE). One of the largest hadith collections.</summary>
    Ahmad = 8,

    /// <summary>Sunan ad-Darimi — compiled by Imam ad-Darimi (d. 869 CE). An early comprehensive collection.</summary>
    Darimi = 9,

    /// <summary>Riyad as-Salihin — compiled by Imam an-Nawawi (d. 1277 CE). Curated collection of authentic hadith organized by topic.</summary>
    RiyadAsSalihin = 10,

    /// <summary>Bulugh al-Maram — compiled by Ibn Hajar al-Asqalani (d. 1449 CE). Hadith relating to jurisprudence.</summary>
    BulughAlMaram = 11,

    /// <summary>Al-Adab Al-Mufrad — compiled by Imam al-Bukhari. Collection focused on manners and etiquette.</summary>
    AdabAlMufrad = 12,

    /// <summary>Mishkat al-Masabih — compiled by al-Khatib al-Tabrizi (d. 1340 CE). Comprehensive collection arranged by topic.</summary>
    MishkatAlMasabih = 13,

    /// <summary>Shama'il Muhammadiyah — compiled by Imam al-Tirmidhi. Describes the appearance, character, and daily life of the Prophet ﷺ.</summary>
    ShamailMuhammadiyah = 14,

    /// <summary>40 Hadith Nawawi — Imam an-Nawawi's curated selection of 40+ foundational hadith.</summary>
    FortyNawawi = 15,

    /// <summary>40 Hadith Qudsi — Collection of 40 hadith in which Allah speaks in the first person through the Prophet ﷺ.</summary>
    FortyQudsi = 16,
}
