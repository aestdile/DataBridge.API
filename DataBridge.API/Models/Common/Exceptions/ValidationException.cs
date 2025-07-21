namespace DataBridge.API.Models.Common.Exceptions;

public class ValidationException<T> : BaseXeption<T>
{
    public ValidationException()
        : base($"Validation failed for '{typeof(T).Name}'. " +
               $"Please review input data and try again.")
    { }

    public ValidationException(string message)
        : base(message) { }

    public ValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}