namespace DataBridge.API.Models.Common.Exceptions;

public class NullException<T> : BaseXeption<T>
{
    public NullException()
        : base($"'{typeof(T).Name}' is null. " +
               $"Please provide a valid instance before proceeding.")
    { }

    public NullException(string message)
        : base(message) { }

    public NullException(string message, Exception innerException)
        : base(message, innerException) { }
}