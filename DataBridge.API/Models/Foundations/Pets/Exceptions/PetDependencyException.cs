using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class PetDependencyException : DependencyException<Pet>
{
    public PetDependencyException(Xeptions.Xeption exception) : base() { }
    public PetDependencyException(string message) : base(message) { }
    public PetDependencyException(string message, Exception innerException) : base(message, innerException) { }
}
