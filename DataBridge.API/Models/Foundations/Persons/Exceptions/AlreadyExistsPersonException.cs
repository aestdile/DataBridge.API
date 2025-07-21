using DataBridge.API.Models.Common.Exceptions;
using EFxceptions.Models.Exceptions;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class AlreadyExistsPersonException : AlreadyExistsException<Person>
{
    private DuplicateKeyException duplicateKeyException;

    public AlreadyExistsPersonException(DuplicatePersonException duplicateKeyException) : base() { }
    public AlreadyExistsPersonException(string message) : base(message) { }

    public AlreadyExistsPersonException(DuplicateKeyException duplicateKeyException)
    {
        this.duplicateKeyException = duplicateKeyException;
    }

    public AlreadyExistsPersonException(string message, Exception innerException) : base(message, innerException) { }
}