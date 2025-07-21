namespace DataBridge.API.Models.Common.Exceptions;

public class ImportException<T> : BaseXeption<T>
{
    public ImportException()
        : base($"Failed to import '{typeof(T).Name}'. " +
               $"Please ensure file format and data correctness.")
    { }

    public ImportException(string message)
        : base(message) { }

    public ImportException(string message, Exception innerException)
        : base(message, innerException) { }
}