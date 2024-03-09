using System.Globalization;

namespace HumanCapitalManagement.Entities.Exceptions;
public class AppException : Exception
{
    /// <summary>
    /// Creates a new <see cref="AppException"/>
    /// </summary>
    public AppException() : base() { }

    /// <summary>
    /// Creates a new <see cref="AppException"/> with a
    /// message param
    /// </summary>
    public AppException(string message) : base(message) { }

    /// <summary>
    /// Creates a new <see cref="AppException"/> with a
    /// message param and an array of other params
    /// </summary>
    public AppException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
