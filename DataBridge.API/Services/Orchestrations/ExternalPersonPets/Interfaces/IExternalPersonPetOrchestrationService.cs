namespace DataBridge.API.Services.Orchestrations.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetOrchestrationService
{
    ValueTask GetAndAddFormattedExternalPersonPetsAsync();
}
