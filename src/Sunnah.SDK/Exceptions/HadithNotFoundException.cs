namespace Sunnah.SDK.Exceptions;

/// <summary>
/// The exception thrown when a Hadith with the specified identifier cannot be found.
/// </summary>
[Serializable]
public class HadithNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HadithNotFoundException"/> class.
    /// </summary>
    public HadithNotFoundException() : base("The specified Hadith was not found.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="HadithNotFoundException"/> class with a specified error message.
    /// </summary>
    public HadithNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="HadithNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception.
    /// </summary>
    public HadithNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
