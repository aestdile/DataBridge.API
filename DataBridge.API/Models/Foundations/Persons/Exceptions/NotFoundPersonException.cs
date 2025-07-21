using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class NotFoundPersonException : NotFoundException<Person>
{
    public NotFoundPersonException(Guid personId) : base() { }
    public NotFoundPersonException(string message) : base(message) { }
    public NotFoundPersonException(string message, Exception innerException) : base(message, innerException) { }
}

