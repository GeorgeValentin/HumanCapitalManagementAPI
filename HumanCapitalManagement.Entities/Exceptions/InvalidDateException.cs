namespace HumanCapitalManagement.Entities.Exceptions;

[Serializable]
public class InvalidDateException : Exception
{
    private const string DefaultMessage = "The date is invalid";

    /// <summary>
    /// Creates a new <see cref="InvalidDateException"/> with a
    /// predefined message
    /// </summary>
    public InvalidDateException()
        : base(DefaultMessage)
    { }

    /// <summary>
    /// Creates a new <see cref="InvalidDateException"/> with a
    /// message param
    /// </summary>
    public InvalidDateException(string message)
        : base(message)
    { }

    /// <summary>
    /// Creates a new <see cref="InvalidDateException"/> with a predefined
    /// message and a wrapped inner exception
    /// </summary>
    public InvalidDateException(Exception innerException)
        : base(DefaultMessage, innerException)
    { }

    /// <summary>
    /// Creates a new <see cref="InvalidDateException"/> with a 
    /// user-supplied message and a wrapped inner exception
    /// </summary>
    public InvalidDateException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
