using Sunnah.SDK.Enums;

namespace Sunnah.SDK.Models;

/// <summary>
/// Represents a parsed Hadith reference such as "bukhari:1" or "muslim:1-5".
/// </summary>
public sealed class HadithReference
{
    /// <summary>
    /// The collection this reference points to.
    /// </summary>
    public HadithCollection Collection { get; }

    /// <summary>
    /// The starting hadith number.
    /// </summary>
    public int HadithNumber { get; }

    /// <summary>
    /// The ending hadith number for range references.
    /// Null for single-hadith references.
    /// </summary>
    public int? EndHadithNumber { get; }

    /// <summary>
    /// Whether this reference is a range (e.g., "bukhari:1-5") rather than a single hadith.
    /// </summary>
    public bool IsRange => EndHadithNumber.HasValue;

    internal HadithReference(HadithCollection collection, int hadithNumber, int? endHadithNumber = null)
    {
        Collection = collection;
        HadithNumber = hadithNumber;
        EndHadithNumber = endHadithNumber;
    }

    /// <summary>
    /// Parses a string hadith reference into a <see cref="HadithReference"/>.
    /// Supported formats: "bukhari:1" (single hadith), "bukhari:1-5" (hadith range).
    /// Collection name aliases: bukhari, muslim, abudawud, tirmidhi, nasai, ibnmajah, malik, ahmad, darimi, riyadussalihin, nawawi, qudsi.
    /// </summary>
    /// <param name="reference">The hadith reference string.</param>
    /// <returns>A parsed <see cref="HadithReference"/>.</returns>
    /// <exception cref="FormatException">Thrown when the reference format is invalid.</exception>
    public static HadithReference Parse(string reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
            throw new FormatException("Hadith reference cannot be null or empty.");

        var parts = reference.Trim().Split(':');
        if (parts.Length != 2)
            throw new FormatException($"Invalid hadith reference format: '{reference}'. Expected format: 'collection:number' or 'collection:start-end'.");

        var collection = ParseCollection(parts[0].Trim());

        var numberPart = parts[1].Trim();
        if (numberPart.IndexOf("-", StringComparison.Ordinal) >= 0)
        {
            var range = numberPart.Split('-');
            if (range.Length != 2)
                throw new FormatException($"Invalid hadith range: '{numberPart}'. Expected format: 'start-end'.");

            if (!int.TryParse(range[0], out int start) || start < 1)
                throw new FormatException($"Invalid start hadith number: '{range[0]}'.");

            if (!int.TryParse(range[1], out int end) || end < start)
                throw new FormatException($"Invalid end hadith number: '{range[1]}'. Must be >= start number.");

            return new HadithReference(collection, start, end);
        }

        if (!int.TryParse(numberPart, out int number) || number < 1)
            throw new FormatException($"Invalid hadith number: '{numberPart}'.");

        return new HadithReference(collection, number);
    }

    /// <summary>
    /// Attempts to parse a string hadith reference. Returns false if the format is invalid.
    /// </summary>
    /// <param name="reference">The hadith reference string.</param>
    /// <param name="result">The parsed reference, or null if parsing failed.</param>
    /// <returns>True if parsing succeeded.</returns>
    public static bool TryParse(string reference, out HadithReference? result)
    {
        result = null;
        try
        {
            result = Parse(reference);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static HadithCollection ParseCollection(string name)
    {
        switch (name.ToLowerInvariant().Replace("-", "").Replace("_", "").Replace(" ", ""))
        {
            case "bukhari":
            case "sahihbukhari":
                return HadithCollection.Bukhari;
            case "muslim":
            case "sahihmuslim":
                return HadithCollection.Muslim;
            case "abudawud":
            case "abudawood":
                return HadithCollection.AbuDawud;
            case "tirmidhi":
            case "tirmizi":
                return HadithCollection.Tirmidhi;
            case "nasai":
            case "nisai":
                return HadithCollection.Nasai;
            case "ibnmajah":
            case "ibnmaja":
                return HadithCollection.IbnMajah;
            case "malik":
            case "muwatta":
                return HadithCollection.Malik;
            case "ahmad":
            case "musnad":
                return HadithCollection.Ahmad;
            case "darimi":
                return HadithCollection.Darimi;
            case "riyadussalihin":
            case "riyadassalihin":
                return HadithCollection.RiyadAsSalihin;
            case "bulughalmaram":
                return HadithCollection.BulughAlMaram;
            case "adabalmufrad":
                return HadithCollection.AdabAlMufrad;
            case "mishkat":
            case "mishkatalmasabih":
                return HadithCollection.MishkatAlMasabih;
            case "shamail":
            case "shamailmuhammadiyah":
                return HadithCollection.ShamailMuhammadiyah;
            case "nawawi":
            case "40nawawi":
            case "fortynawawi":
                return HadithCollection.FortyNawawi;
            case "qudsi":
            case "40qudsi":
            case "fortyqudsi":
                return HadithCollection.FortyQudsi;
            default:
                throw new FormatException($"Unknown collection: '{name}'. Supported: bukhari, muslim, abudawud, tirmidhi, nasai, ibnmajah, malik, ahmad, darimi, riyadussalihin, nawawi, qudsi, etc.");
        }
    }

    /// <inheritdoc />
    public override string ToString() =>
        IsRange ? $"{Collection}:{HadithNumber}-{EndHadithNumber}" : $"{Collection}:{HadithNumber}";
}
