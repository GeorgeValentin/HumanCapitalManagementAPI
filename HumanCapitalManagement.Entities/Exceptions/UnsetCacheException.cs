using System.Globalization;

namespace HumanCapitalManagement.Entities.Exceptions;

[Serializable]
public class UnsetCacheException : Exception
{
    /// <summary>
    /// Creates a new <see cref="UnsetCacheException"/>
    /// </summary>
    public UnsetCacheException() : base() { }

    /// <summary>
    /// Creates a new <see cref="UnsetCacheException"/> with a
    /// message param
    /// </summary>
    public UnsetCacheException(string message) : base(message) { }

    /// <summary>
    /// Creates a new <see cref="UnsetCacheException"/> with a
    /// message param and an array of other params
    /// </summary>
    public UnsetCacheException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
