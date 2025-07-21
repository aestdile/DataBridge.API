using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class PersonDependencyValidationException : DependencyValidationException<Person>
{
    public PersonDependencyValidationException(Xeptions.Xeption exception) : base() { }
    public PersonDependencyValidationException(string message) : base(message) { }
    public PersonDependencyValidationException(string message, Exception innerException) : base(message, innerException) { }
}

