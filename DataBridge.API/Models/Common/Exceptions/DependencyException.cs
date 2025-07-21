namespace DataBridge.API.Models.Common.Exceptions;

public class DependencyException<T> : BaseXeption<T>
{
    public DependencyException()
        : base($"A dependency error occurred in '{typeof(T).Name}'. " +
               $"Please check dependent services and try again.")
    { }

    public DependencyException(string message)
        : base(message) { }

    public DependencyException(string message, Exception innerException)
        : base(message, innerException) { }
}
