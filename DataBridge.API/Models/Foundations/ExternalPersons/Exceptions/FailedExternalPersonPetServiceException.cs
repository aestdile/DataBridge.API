using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class FailedExternalPersonPetServiceException : BaseXeption<ExternalPerson>
{
    public FailedExternalPersonPetServiceException(Exception exception)
        : base("Failed external person pet service error occurred, " +
            "contact support.") { }
}

