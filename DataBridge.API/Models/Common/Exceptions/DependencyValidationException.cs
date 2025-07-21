namespace DataBridge.API.Models.Common.Exceptions;

public class DependencyValidationException<T> : BaseXeption<T>
{
    public DependencyValidationException()
        : base($"'{typeof(T).Name}' validation failed due to dependency constraints. " +
               $"Please check related data and try again.")
    { }

    public DependencyValidationException(string message)
        : base(message) { }

    public DependencyValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}