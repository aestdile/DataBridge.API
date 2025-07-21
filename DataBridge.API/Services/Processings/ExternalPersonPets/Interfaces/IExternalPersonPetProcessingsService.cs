using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetProcessingService
{
    ValueTask<List<ExternalPerson>> GetFormattedExternalPersonPetsAsync();
}