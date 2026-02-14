namespace Sunnah.SDK.Exceptions;

/// <summary>
/// The exception thrown when a Hadith collection with the specified identifier cannot be found.
/// </summary>
[Serializable]
public class CollectionNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionNotFoundException"/> class.
    /// </summary>
    public CollectionNotFoundException() : base("The specified Hadith collection was not found.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionNotFoundException"/> class with a specified error message.
    /// </summary>
    public CollectionNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception.
    /// </summary>
    public CollectionNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
