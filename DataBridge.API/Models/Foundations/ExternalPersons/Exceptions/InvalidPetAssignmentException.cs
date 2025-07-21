using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class InvalidPetAssignmentException : BaseXeption<ExternalPerson>
{
    public InvalidPetAssignmentException()
        : base("Invalid pet assignment error occurred, " +
            "please ensure the pet is correctly assigned " +
                "to the external person and try again.") { }
}

