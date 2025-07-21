using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Processings.ExternalPersonPets.Services;

public class ExternalPersonPetEventProcessingService : IExternalPersonPetEventProcessingService
{
    private readonly IExternalPersonPetEventService externalPersonPetEventService;

    public ExternalPersonPetEventProcessingService(
        IExternalPersonPetEventService externalPersonPetEventService) =>
            this.externalPersonPetEventService = externalPersonPetEventService;

    public ValueTask AddExternalPersonPets(List<ExternalPerson> externalPersonPets) =>
        this.externalPersonPetEventService.AddExternalPersonPets(externalPersonPets);

    public ValueTask<List<ExternalPerson>> GetExternalPersonPets() =>
        this.externalPersonPetEventService.GetExternalPersonPets();
}