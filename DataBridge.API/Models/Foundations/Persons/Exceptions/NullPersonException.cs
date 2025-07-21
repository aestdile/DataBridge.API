using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class NullPersonException : NullException<Person>
{
    public NullPersonException() : base() { }
    public NullPersonException(string message) : base(message) { }
    public NullPersonException(string message, Exception innerException) : base(message, innerException) { }
}

