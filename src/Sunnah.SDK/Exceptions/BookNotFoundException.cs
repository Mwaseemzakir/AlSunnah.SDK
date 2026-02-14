namespace Sunnah.SDK.Exceptions;

/// <summary>
/// The exception thrown when a book/chapter within a Hadith collection cannot be found.
/// </summary>
[Serializable]
public class BookNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookNotFoundException"/> class.
    /// </summary>
    public BookNotFoundException() : base("The specified book was not found in the collection.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookNotFoundException"/> class with a specified error message.
    /// </summary>
    public BookNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception.
    /// </summary>
    public BookNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
