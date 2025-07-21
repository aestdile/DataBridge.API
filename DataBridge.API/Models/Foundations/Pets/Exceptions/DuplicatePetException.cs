using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class DuplicatePetException : DuplicateException<Pet>
{
    public DuplicatePetException() : base() { }
    public DuplicatePetException(string message) : base(message) { }
    public DuplicatePetException(string message, Exception innerException) : base(message, innerException) { }
}
