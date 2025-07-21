namespace DataBridge.API.Models.Common.Exceptions;

public class TransformationException<T> : BaseXeption<T>
{
    public TransformationException()
        : base($"Error transforming '{typeof(T).Name}' data. " +
               $"Please verify the format and logic.")
    { }

    public TransformationException(string message)
        : base(message) { }

    public TransformationException(string message, Exception innerException)
        : base(message, innerException) { }
}