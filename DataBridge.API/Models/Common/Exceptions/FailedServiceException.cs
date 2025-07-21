namespace DataBridge.API.Models.Common.Exceptions;

public class FailedServiceException<T> : BaseXeption<T>
{
    public FailedServiceException()
        : base($"'{typeof(T).Name}' service operation failed. " +
               $"Please contact support or try again later.")
    { }

    public FailedServiceException(string message)
        : base(message) { }

    public FailedServiceException(string message, Exception innerException)
        : base(message, innerException) { }
}
