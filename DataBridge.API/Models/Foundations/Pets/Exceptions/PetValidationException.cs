using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class PetValidationException : ValidationException<Pet>
{
    public PetValidationException(Xeptions.Xeption exception) : base() { }
    public PetValidationException(string message) : base(message) { }
    public PetValidationException(string message, Exception innerException) : base(message, innerException) { }
}
