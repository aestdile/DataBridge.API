using DataBridge.API.Models.Common;

namespace DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

public class FileFormatMismatchException : BaseXeption<ExternalPerson>
{
    public FileFormatMismatchException()
        : base("File format mismatch error occurred, " +
            "please ensure the file is in the correct format " +
                "and try again.") { }
}

