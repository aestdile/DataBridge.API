using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class PetTransformationException : TransformationException<Pet>
{
    public PetTransformationException() : base() { }
    public PetTransformationException(string message) : base(message) { }
    public PetTransformationException(string message, Exception innerException) : base(message, innerException) { }
}
