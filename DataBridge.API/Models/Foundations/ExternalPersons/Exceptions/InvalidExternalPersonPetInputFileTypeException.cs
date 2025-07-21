using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class InvalidExternalPersonPetInputFileTypeException : BaseXeption<ExternalPerson>
{
    public InvalidExternalPersonPetInputFileTypeException(string fileName)
        : base("Invalid external person pet input file type error occurred, " +
            "please ensure the file is in the correct format and try again.") { }
}
