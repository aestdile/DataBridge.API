using DataBridge.API.Models.Orchestrations.PersonPets;

namespace DataBridge.API.Services.Coordinators.Interfaces;

public interface IPersonPetEventCoordinationService
{
    ValueTask<List<PersonPet>> CoordinateExternalPersonPetsAsync();
}
