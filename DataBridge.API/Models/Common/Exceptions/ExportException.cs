namespace DataBridge.API.Models.Common.Exceptions;

public class ExportException<T> : BaseXeption<T>
{
    public ExportException()
        : base($"Failed to export '{typeof(T).Name}'. " +
               $"Please ensure data is valid and try again.")
    { }

    public ExportException(string message)
        : base(message) { }

    public ExportException(string message, Exception innerException)
        : base(message, innerException) { }
}
