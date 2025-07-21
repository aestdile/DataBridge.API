using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class NotFoundPetException : NotFoundException<Pet>
{
    public NotFoundPetException(Guid petId) : base() { }
    public NotFoundPetException(string message) : base(message) { }
    public NotFoundPetException(string message, Exception innerException) : base(message, innerException) { }
}