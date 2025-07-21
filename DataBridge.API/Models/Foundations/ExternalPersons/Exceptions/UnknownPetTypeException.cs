using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class UnknownPetTypeException : BaseXeption<ExternalPerson>
{
    public UnknownPetTypeException()
        : base("Unknown pet type error occurred, " +
            "please ensure the pet type is recognized " +
                "and try again.") { }
}

