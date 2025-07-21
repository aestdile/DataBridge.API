using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class PersonImportException : ImportException<Person>
{
    public PersonImportException() : base() { }
    public PersonImportException(string message) : base(message) { }
    public PersonImportException(string message, Exception innerException) : base(message, innerException) { }
}

