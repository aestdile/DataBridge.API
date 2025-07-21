using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class InvalidPersonException : InvalidEntityException<Person>
{
    public InvalidPersonException() : base() { }
    public InvalidPersonException(string message) : base(message) { }
    public InvalidPersonException(string message, Exception innerException) : base(message, innerException) { }
}
