using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class ExternalPersonPetDependencyException : BaseXeption<ExternalPerson>
{
    public ExternalPersonPetDependencyException(Xeptions.Xeption exception)
        : base("External person pet dependency error occurred, " +
            "contact support.") 
    { }
}
