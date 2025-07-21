using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Common.Exceptions;
public class DuplicateException<T> : BaseXeption<T>
{
    public DuplicateException()
        : base($"Duplicate '{typeof(T).Name}' found. " +
               $"Please ensure uniqueness before retrying.")
    { }

    public DuplicateException(string message)
        : base(message) { }

    public DuplicateException(string message, Exception innerException)
        : base(message, innerException) { }
}