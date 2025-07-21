using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class PersonExportException : ExportException<Person>
{
    public PersonExportException() : base() { }
    public PersonExportException(string message) : base(message) { }
    public PersonExportException(string message, Exception innerException) : base(message, innerException) { }
}

