using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class PetExportException : ExportException<Pet>
{
    public PetExportException() : base() { }
    public PetExportException(string message) : base(message) { }
    public PetExportException(string message, Exception innerException) : base(message, innerException) { }
}
