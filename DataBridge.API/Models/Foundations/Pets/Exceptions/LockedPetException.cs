using DataBridge.API.Models.Common.Exceptions;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class LockedPetException : LockedEntityException<Pet>
{
    public LockedPetException(Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException dbUpdateConcurrencyException) : base () { }
    public LockedPetException(string message) : base(message) { }
    public LockedPetException(string message, Exception innerException) : base(message, innerException) { }
}
