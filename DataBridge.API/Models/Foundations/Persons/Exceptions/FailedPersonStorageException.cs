using DataBridge.API.Models.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataBridge.API.Models.Foundations.Persons.Exceptions;

public class FailedPersonStorageException : FailedStorageException<Person>
{
    private DbUpdateException dbUpdateException;

    public FailedPersonStorageException(Microsoft.Data.SqlClient.SqlException sqlException) : base() { }
    public FailedPersonStorageException(string message) : base(message) { }

    public FailedPersonStorageException(DbUpdateException dbUpdateException)
    {
        this.dbUpdateException = dbUpdateException;
    }

    public FailedPersonStorageException(string message, Exception innerException) : base(message, innerException) { }
}

