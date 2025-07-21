using DataBridge.API.Models.Common.Exceptions;
using EFxceptions.Models.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class AlreadyExistsPetException : AlreadyExistsException<Pet>
{
    private DuplicateKeyException duplicateKeyException;

    public AlreadyExistsPetException(DuplicatePetException duplicateKeyException) : base() { }
    public AlreadyExistsPetException(string message) : base(message) { }

    public AlreadyExistsPetException(DuplicateKeyException duplicateKeyException)
    {
        this.duplicateKeyException = duplicateKeyException;
    }

    public AlreadyExistsPetException(string message, Exception innerException) : base(message, innerException) { }
}
