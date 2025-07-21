using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Services.Orchestrations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Orchestrations.ExternalPersonPets.Services;

public class ExternalPersonPetEventOrchestrationService : IExternalPersonPetEventOrchestrationService
{
    private readonly IExternalPersonPetEventProcessingService externalPersonPetEventProcessingService;

    public ExternalPersonPetEventOrchestrationService(
        IExternalPersonPetEventProcessingService externalPersonPetEventProcessingService) =>
            this.externalPersonPetEventProcessingService = externalPersonPetEventProcessingService;

    public ValueTask<List<ExternalPerson>> GetExternalPersonPets() =>
        this.externalPersonPetEventProcessingService.GetExternalPersonPets();
}
