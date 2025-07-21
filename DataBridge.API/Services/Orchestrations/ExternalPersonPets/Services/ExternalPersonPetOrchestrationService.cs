using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Services.Orchestrations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Orchestrations.ExternalPersonPets.Services;

public class ExternalPersonPetOrchestrationService : IExternalPersonPetOrchestrationService
{
    private readonly IExternalPersonPetProcessingService externalPersonPetProcessingService;
    private readonly IExternalPersonPetEventProcessingService externalPersonPetEventProcessingService;

    public ExternalPersonPetOrchestrationService(
        IExternalPersonPetProcessingService externalPersonPetProcessingService,
        IExternalPersonPetEventProcessingService externalPersonPetEventProcessingService)
    {
        this.externalPersonPetProcessingService = externalPersonPetProcessingService;
        this.externalPersonPetEventProcessingService = externalPersonPetEventProcessingService;
    }

    public async ValueTask GetAndAddFormattedExternalPersonPetsAsync()
    {
        List<ExternalPerson> formattedExternalPersonPets =
            await this.externalPersonPetProcessingService
                .GetFormattedExternalPersonPetsAsync();

        await this.externalPersonPetEventProcessingService
            .AddExternalPersonPets(formattedExternalPersonPets);
    }
}