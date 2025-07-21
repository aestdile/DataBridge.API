using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class InvalidPetException : InvalidEntityException<Pet>
{
    public InvalidPetException() : base() { }
    public InvalidPetException(string message) : base(message) { }
    public InvalidPetException(string message, Exception innerException) : base(message, innerException) { }
}
