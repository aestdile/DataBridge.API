using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class PetImportException : ImportException<Pet>
{
    public PetImportException() : base() { }
    public PetImportException(string message) : base(message) { }
    public PetImportException(string message, Exception innerException) : base(message, innerException) { }
}
