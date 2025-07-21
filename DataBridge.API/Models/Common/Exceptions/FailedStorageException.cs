namespace DataBridge.API.Models.Common.Exceptions;

public class FailedStorageException<T> : BaseXeption<T>
{
    public FailedStorageException()
        : base($"'{typeof(T).Name}' storage operation failed. " +
               $"Please ensure the storage is functioning and try again.")
    { }

    public FailedStorageException(string message)
        : base(message) { }

    public FailedStorageException(string message, Exception innerException)
        : base(message, innerException) { }
}
