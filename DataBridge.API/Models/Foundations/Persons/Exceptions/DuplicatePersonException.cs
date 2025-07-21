using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class DuplicatePersonException : DuplicateException<Person>
{
    public DuplicatePersonException() : base() { }
    public DuplicatePersonException(string message) : base(message) { }
    public DuplicatePersonException(string message, Exception innerException) : base(message, innerException) { }
}

