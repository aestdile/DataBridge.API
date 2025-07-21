using Xeptions;

namespace DataBridge.API.Models.Common;
public class BaseXeption<T> : Xeption
{
    public BaseXeption()
        : base() { }
    public BaseXeption(string message)
        : base(message) { }

    public BaseXeption(string message, Exception innerException)
        : base(message, innerException) { }
}