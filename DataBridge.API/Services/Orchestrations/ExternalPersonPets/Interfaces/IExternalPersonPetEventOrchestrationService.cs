using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Services.Orchestrations.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetEventOrchestrationService
{
    ValueTask<List<ExternalPerson>> GetExternalPersonPets();
}