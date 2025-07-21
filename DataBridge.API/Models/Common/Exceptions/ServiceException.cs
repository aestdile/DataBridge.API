namespace DataBridge.API.Models.Common.Exceptions;

public class ServiceException<T> : BaseXeption<T>
{
    public ServiceException()
        : base($"An internal service error occurred for '{typeof(T).Name}'. " +
               $"Please try again later.")
    { }

    public ServiceException(string message)
        : base(message) { }

    public ServiceException(string message, Exception innerException)
        : base(message, innerException) { }
}