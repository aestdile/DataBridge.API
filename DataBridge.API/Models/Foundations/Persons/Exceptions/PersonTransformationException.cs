using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class PersonTransformationException : TransformationException<Person>
{
    public PersonTransformationException() : base() { }
    public PersonTransformationException(string message) : base(message) { }
    public PersonTransformationException(string message, Exception innerException) : base(message, innerException) { }
}

