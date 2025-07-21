using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class NullPetException : NullException<Pet>
{
    public NullPetException() : base() { }
    public NullPetException(string message) : base(message) { }
    public NullPetException(string message, Exception innerException) : base(message, innerException) { }
}

