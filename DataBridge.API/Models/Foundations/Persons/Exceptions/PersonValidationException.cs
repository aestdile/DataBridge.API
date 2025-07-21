using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class PersonValidationException : ValidationException<Person>
{
    public PersonValidationException(Xeptions.Xeption exception) : base() { }
    public PersonValidationException(string message) : base(message) { }
    public PersonValidationException(string message, Exception innerException) : base(message, innerException) { }
}

