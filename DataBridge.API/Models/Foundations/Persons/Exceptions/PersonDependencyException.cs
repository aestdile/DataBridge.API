using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class PersonDependencyException : DependencyException<Person>
{
    public PersonDependencyException(Xeptions.Xeption exception) : base() { }
    public PersonDependencyException(string message) : base(message) { }
    public PersonDependencyException(string message, Exception innerException) : base(message, innerException) { }
}


