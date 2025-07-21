using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetEventService
{
    ValueTask AddExternalPersonPets(List<ExternalPerson> externalPersonPets);
    ValueTask<List<ExternalPerson>> GetExternalPersonPets();
}