using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class ExternalPersonPetServiceException : BaseXeption<ExternalPerson>
{
    public ExternalPersonPetServiceException(Xeptions.Xeption exception)
        : base("External person pet service error occurred, " +
            "contact support.") { }
}

