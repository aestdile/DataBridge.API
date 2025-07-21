using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class ExternalPersonPetValidationException : BaseXeption<ExternalPerson>
{
    public ExternalPersonPetValidationException(Exception exception)
        : base("External person pet validation error occurred, " +
            "please fix the errors and try again.") { }
}
