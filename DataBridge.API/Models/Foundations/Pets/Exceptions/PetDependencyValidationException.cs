using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class PetDependencyValidationException : DependencyValidationException<Pet>
{
    public PetDependencyValidationException(Xeptions.Xeption exception) :base() { }
    public PetDependencyValidationException(string message) : base(message) { }
    public PetDependencyValidationException(string message, Exception innerException) : base(message, innerException) { }
}
