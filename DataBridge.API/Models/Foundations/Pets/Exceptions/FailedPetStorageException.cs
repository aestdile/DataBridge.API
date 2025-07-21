using DataBridge.API.Models.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataBridge.API.Models.Foundations.Pets.Exceptions;

public class FailedPetStorageException : FailedStorageException<Pet>
{
    private DbUpdateException dbUpdateException;

    public FailedPetStorageException(Microsoft.Data.SqlClient.SqlException sqlException) : base() { }
    public FailedPetStorageException(string message) : base(message) { }

    public FailedPetStorageException(DbUpdateException dbUpdateException)
    {
        this.dbUpdateException = dbUpdateException;
    }

    public FailedPetStorageException(string message, Exception innerException) : base(message, innerException) { }
}
