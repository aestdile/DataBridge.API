namespace DataBridge.API.Models.Common.Exceptions;

public class InvalidEntityException<T> : BaseXeption<T>
{
    public InvalidEntityException()
        : base($"Invalid '{typeof(T).Name}' data detected. " +
               $"Please provide correct data and try again.")
    { }

    public InvalidEntityException(string message)
        : base(message) { }

    public InvalidEntityException(string message, Exception innerException)
        : base(message, innerException) { }
}