using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class PersonServiceException : ServiceException<Person>
{
    public PersonServiceException(Xeptions.Xeption exception) : base() { }
    public PersonServiceException(string message) : base(message) { }
    public PersonServiceException(string message, Exception innerException) : base(message, innerException) { }
}

