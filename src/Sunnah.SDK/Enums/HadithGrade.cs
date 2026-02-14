namespace Sunnah.SDK.Enums;

/// <summary>
/// Represents the authenticity grading of a Hadith as classified by hadith scholars.
/// </summary>
public enum HadithGrade
{
    /// <summary>Unknown or ungraded — the grade has not been determined or is not available.</summary>
    Unknown = 0,

    /// <summary>Sahih — Authentic. The highest grade of authenticity.</summary>
    Sahih = 1,

    /// <summary>Hasan — Good. Acceptable and reliable, one grade below Sahih.</summary>
    Hasan = 2,

    /// <summary>Da'if — Weak. The chain of narration has issues but is not fabricated.</summary>
    Daif = 3,

    /// <summary>Hasan Sahih — Between Hasan and Sahih grade.</summary>
    HasanSahih = 4,

    /// <summary>Sahih li-Ghayrihi — Authentic due to supporting evidence from other narrations.</summary>
    SahihLiGhayrihi = 5,

    /// <summary>Hasan li-Ghayrihi — Good due to supporting evidence from other narrations.</summary>
    HasanLiGhayrihi = 6,

    /// <summary>Mawdu' — Fabricated. Identified as a forgery.</summary>
    Mawdu = 7,
}
