using DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;
using DataBridge.API.Models.Foundations.ExternalPersons;
using Xeptions;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets.Services;

public partial class ExternalPersonPetService
{
    private delegate ValueTask<List<ExternalPerson>> ReturningExternalPersonPetsFunction();

    private async ValueTask<List<ExternalPerson>> TryCatch(
        ReturningExternalPersonPetsFunction returningExternalPersonPetsFunction)
    {
        try
        {
            return await returningExternalPersonPetsFunction();
        }
        catch (IOException ioException)
        {
            var failedDependencyException =
                new FailedExternalPersonPetDependencyException(ioException);

            throw CreateAndLogDependencyException(failedDependencyException);
        }
        catch (Exception exception)
        {
            var failedServiceException =
                new FailedExternalPersonPetServiceException(exception);

            throw CreateAndLogServiceException(failedServiceException);
        }
    }

    private ExternalPersonPetDependencyException CreateAndLogDependencyException(Xeption exception)
    {
        var dependencyException =
            new ExternalPersonPetDependencyException(exception);

        this.loggingBroker.LogError(dependencyException);

        return dependencyException;
    }

    private ExternalPersonPetServiceException CreateAndLogServiceException(Xeption exception)
    {
        var serviceException =
            new ExternalPersonPetServiceException(exception);

        this.loggingBroker.LogError(serviceException);

        return serviceException;
    }
}