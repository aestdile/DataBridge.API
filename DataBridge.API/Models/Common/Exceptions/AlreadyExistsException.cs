namespace DataBridge.API.Models.Common.Exceptions;

public class AlreadyExistsException<T> : BaseXeption<T>
{
    public AlreadyExistsException()
        : base($"'{typeof(T).Name}' already exists. " +
               $"Please ensure it does not already exist and try again.")
    { }

    public AlreadyExistsException(string message)
        : base(message) { }

    public AlreadyExistsException(string message, Exception innerException)
        : base(message, innerException) { }
}

