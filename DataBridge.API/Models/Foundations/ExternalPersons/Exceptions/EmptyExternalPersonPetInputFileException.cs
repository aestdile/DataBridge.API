using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class EmptyExternalPersonPetInputFileException : BaseXeption<ExternalPerson>
{
    public EmptyExternalPersonPetInputFileException()
        : base("Input file is empty. " +
            "Please upload a non-empty Excel file.") { }
}
