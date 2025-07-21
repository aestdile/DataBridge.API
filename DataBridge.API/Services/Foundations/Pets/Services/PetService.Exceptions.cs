using DataBridge.API.Models.Foundations.Pets;
using DataBridge.API.Models.Foundations.Pets.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace DataBridge.API.Services.Foundations.Pets.Services;

public partial class PetService
{
    private delegate ValueTask<Pet> ReturningPetFunction();
    private delegate IQueryable<Pet> ReturningPetsFunction();

    private async ValueTask<Pet> TryCatch(ReturningPetFunction returningPetFunction)
    {
        try
        {
            return await returningPetFunction();
        }
        catch (NullPetException nullPetException)
        {
            throw CreateAndLogValidationException(nullPetException);
        }
        catch (InvalidPetException invalidPetException)
        {
            throw CreateAndLogValidationException(invalidPetException);
        }
        catch (SqlException sqlException)
        {
            var failedPetStorageException = new FailedPetStorageException(sqlException);

            throw CreateAndLogCriticalDependencyException(failedPetStorageException);
        }
        catch (NotFoundPetException notFoundPetException)
        {
            throw CreateAndLogValidationException(notFoundPetException);
        }
        catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
        {
            var lockedPetException = new LockedPetException(dbUpdateConcurrencyException);

            throw CreateAndLogDependencyValidationException(lockedPetException);
        }
        catch (DbUpdateException dbUpdateException)
        {
            var failedPetStorageException = new FailedPetStorageException(dbUpdateException);

            throw CreateAndLogDependencyException(failedPetStorageException);
        }
        catch (DuplicatePetException duplicateKeyException)
        {
            var alreadyExistPetException =
                new AlreadyExistsPetException(duplicateKeyException);

            throw CreateAndLogDependencyValidationException(alreadyExistPetException);
        }
        catch (Exception exception)
        {
            var failedPetServiceException =
                new FailedPetServiceException(exception);

            throw CreateAndLogServiceException(failedPetServiceException);
        }
    }

    private IQueryable<Pet> TryCatch(ReturningPetsFunction returningPetsFunction)
    {
        try
        {
            return returningPetsFunction();
        }
        catch (SqlException sqlException)
        {
            var failedPetStorageException =
                new FailedPetStorageException(sqlException);

            throw CreateAndLogCriticalDependencyException(failedPetStorageException);
        }
        catch (Exception exception)
        {
            var failedPetServiceException =
                new FailedPetServiceException(exception);

            throw CreateAndLogServiceException(failedPetServiceException);
        }
    }

    private PetValidationException CreateAndLogValidationException(Xeption exception)
    {
        var petValidationException =
            new PetValidationException(exception);

        this.loggingBroker.LogError(petValidationException);

        return petValidationException;
    }

    private PetDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
    {
        var petDependencyException = new PetDependencyException(exception);
        this.loggingBroker.LogCritical(petDependencyException);

        return petDependencyException;
    }

    private PetDependencyValidationException CreateAndLogDependencyValidationException(
        Xeption exception)
    {
        var petDependencyValidationException =
            new PetDependencyValidationException(exception);

        this.loggingBroker.LogError(petDependencyValidationException);

        return petDependencyValidationException;
    }

    private PetDependencyException CreateAndLogDependencyException(Xeption exception)
    {
        var petDependencyException = new PetDependencyException(exception);
        this.loggingBroker.LogError(petDependencyException);

        return petDependencyException;
    }

    private PetServiceException CreateAndLogServiceException(Xeption exception)
    {
        var petServiceException = new PetServiceException(exception);
        this.loggingBroker.LogError(petServiceException);

        return petServiceException;
    }
}
