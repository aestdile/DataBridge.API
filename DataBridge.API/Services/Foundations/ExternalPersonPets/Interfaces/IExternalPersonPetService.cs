using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetService
{
    ValueTask<List<ExternalPerson>> GetAllExternalPersonPetsAsync();
}
