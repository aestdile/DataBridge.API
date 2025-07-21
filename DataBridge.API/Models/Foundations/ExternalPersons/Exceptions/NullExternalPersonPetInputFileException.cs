using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class NullExternalPersonPetInputFileException : BaseXeption<ExternalPerson>
{
    public NullExternalPersonPetInputFileException()
        : base("Null external person pet input file error occurred, " +
            "please ensure the file is not null and try again.") 
    { }
}

