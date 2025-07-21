using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class FailedPersonServiceException : FailedServiceException<Person>
{
    public FailedPersonServiceException(Exception exception) : base() { }
    public FailedPersonServiceException(string message) : base(message) { }
    public FailedPersonServiceException(string message, Exception innerException) : base(message, innerException) { }
}

