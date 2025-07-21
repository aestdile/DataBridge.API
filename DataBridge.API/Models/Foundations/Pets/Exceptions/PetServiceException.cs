using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class PetServiceException : ServiceException<Pet>
{
    public PetServiceException(Xeptions.Xeption exception) : base() { }
    public PetServiceException(string message) : base(message) { }
    public PetServiceException(string message, Exception innerException) : base(message, innerException) { }
}
