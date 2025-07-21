using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class FailedExternalPersonPetDependencyException : BaseXeption<ExternalPerson>
{
    public FailedExternalPersonPetDependencyException(IOException ioException)
        : base("Failed external person pet dependency error occurred, " +
            "contact support.") { }
}

