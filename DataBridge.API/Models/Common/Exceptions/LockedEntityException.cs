namespace DataBridge.API.Models.Common.Exceptions;

public class LockedEntityException<T> : BaseXeption<T>
{
    public LockedEntityException()
        : base($"'{typeof(T).Name}' is currently locked. " +
               $"Please try again later or contact an administrator.")
    { }

    public LockedEntityException(string message)
        : base(message) { }

    public LockedEntityException(string message, Exception innerException)
        : base(message, innerException) { }
}