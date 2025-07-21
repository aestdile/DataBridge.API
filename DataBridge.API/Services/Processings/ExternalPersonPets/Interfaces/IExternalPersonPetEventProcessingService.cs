using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetEventProcessingService
{
    ValueTask AddExternalPersonPets(List<ExternalPerson> externalPersonPets);
    ValueTask<List<ExternalPerson>> GetExternalPersonPets();
}
