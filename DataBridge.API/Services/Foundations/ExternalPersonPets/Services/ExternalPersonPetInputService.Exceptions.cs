using DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets;

public partial class ExternalPersonPetInputService
{
    private delegate ValueTask ReturningFileFunction();

    private async ValueTask TryCatch(ReturningFileFunction returningFileFunction)
    {
        try
        {
            await returningFileFunction();
        }
        catch (NullExternalPersonPetInputFileException nullFileException)
        {
            this.loggingBroker.LogError(nullFileException);
            throw;
        }
        catch (EmptyExternalPersonPetInputFileException emptyFileException)
        {
            this.loggingBroker.LogError(emptyFileException);
            throw;
        }
        catch (InvalidExternalPersonPetInputFileTypeException invalidTypeFile)
        {
            this.loggingBroker.LogError(invalidTypeFile);
            throw;
        }
        catch (Exception exception)
        {
            this.loggingBroker.LogError(exception);
            throw;
        }
    }
}


