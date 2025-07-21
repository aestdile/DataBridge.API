namespace DataBridge.API.Models.Common.Exceptions;

public class NotFoundException<T> : BaseXeption<T>
{
    public NotFoundException()
        : base($"'{typeof(T).Name}' was not found. " +
               $"Please ensure the correct identifier and try again.")
    { }

    public NotFoundException(string message)
        : base(message) { }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}