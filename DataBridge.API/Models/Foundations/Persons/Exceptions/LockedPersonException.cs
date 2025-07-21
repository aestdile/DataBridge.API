using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class LockedPersonException : LockedEntityException<Person>
{
    public LockedPersonException(Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException dbUpdateConcurrencyException) : base() { }
    public LockedPersonException(string message) : base(message) { }
    public LockedPersonException(string message, Exception innerException) : base(message, innerException) { }
}

