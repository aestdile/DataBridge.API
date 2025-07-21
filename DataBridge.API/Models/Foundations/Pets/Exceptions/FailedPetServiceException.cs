using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class FailedPetServiceException : FailedServiceException<Pet>
{
    public FailedPetServiceException(Exception exception) : base() { }
    public FailedPetServiceException(string message) : base(message) { }
    public FailedPetServiceException(string message, Exception innerException) : base(message, innerException) { }
}
